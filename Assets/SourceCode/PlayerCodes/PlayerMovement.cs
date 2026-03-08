using System.Collections;
using UnityEngine;

public enum PlayerState
{
	idle,
	walk,
	attack,
	interact,
	stagger,
	ability
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D rigidbody2d;
	public Vector3 change;
	private Animator animator;

	private Vector2 facingDirection = Vector2.down;

	//	todo - health - Break off the health system into a own component
	/*
	public FloatValue currentHealth;
	public SignalObject playerHealthSignal;
	*/

	public VectorValue startingPosition;

	// 	todo - health - camera shake signal be part of the health system
	public SignalObject CameraShakeSignal;
	//	todo - magic - player magic signal be part of the magic system
	public SignalObject reduceMagicSignal;


	//	todo - inventory - break off the player inventory into a own component
	public Inventory playerInventory;

	public SpriteRenderer receiveItemSprite;

	//	todo - iframes - break off the iframe stuff into its own script
	[Header("IFrame Stuff")]
	public Color flashColor;
	public Color regularColor;
	public float flashDuration;
	public int numberOfFlashes;
	public Collider2D triggerCollider;
	public SpriteRenderer sprite;

	//	todo - ability - break this off with the player ability system
	[Header("Projectiles")]
	public GameObject arrowProjectile;
	public Item bow;

	[SerializeField] private GenericAbility currentAbility;

	// Start is called before the first frame update
	void Start()
	{
		currentState = PlayerState.walk;
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		animator.SetFloat("moveX", 0);
		animator.SetFloat("moveY", -1);
		transform.position = startingPosition.initialValue;
	}

	// Update is called once per frame
	void Update()
	{
		if (currentState == PlayerState.interact) return;
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		if (!IsRestrictedState(currentState))
        {
			if (Input.GetButtonDown("Fire1") && currentState != PlayerState.attack &&
				currentState != PlayerState.stagger)
			{
				StartCoroutine(AttackCo());
			}
			//	todo - ability
			else if (Input.GetButtonDown("Fire2") && currentState != PlayerState.attack &&
				currentState != PlayerState.stagger)
			{
				if (playerInventory.CheckForItem(bow))
				{
					StartCoroutine(SecondAttackCo());
				}
			}
			else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
			{
				UpdateAnimationAndMove();
			}
			else if (Input.GetButtonDown("Ability"))
			{
				if (currentAbility)
				{
					StartCoroutine(AbilityCo(currentAbility.duration));
				}
			}
        }
	}
	
	bool IsRestrictedState(PlayerState currentState)
    {
		if (currentState == PlayerState.attack || currentState == PlayerState.ability)
		{
			return true;
		}
		return false;
    }


	private IEnumerator AttackCo()
	{
		animator.SetBool("attacking", true);
		currentState = PlayerState.attack;
		yield return null;
		animator.SetBool("attacking", false);
		yield return new WaitForSeconds(.3f);
		if (currentState != PlayerState.interact)
		{
			currentState = PlayerState.walk;
		}
	}

	//	todo - ability
	private IEnumerator SecondAttackCo()
	{
		//animator.SetBool("attacking", true);
		currentState = PlayerState.attack;
		yield return null;
		MakeArrow();
		//animator.SetBool("attacking", false);
		yield return new WaitForSeconds(.3f);
		if (currentState != PlayerState.interact)
		{
			currentState = PlayerState.walk;
		}
	}

	// 	todo - ability - this should be part of the ability system
	private void MakeArrow()
	{
		if (playerInventory.currentMagic > 0)
		{
			Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
			Arrow arrow = Instantiate(arrowProjectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
			arrow.Setup(temp, ChooseArrowDirection());
			playerInventory.UseMagic(arrow.cost);
			reduceMagicSignal.Raise();
		}
	}

	// 	todo - ability - this should be part of the ability system
	Vector3 ChooseArrowDirection()
    {
		float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
		return new Vector3(0, 0, temp);
    }

	public void RaiseItem()
	{
		if (playerInventory.currentItem != null)
		{
			if (currentState != PlayerState.interact)
			{
				animator.SetBool("receiveItem", true);
				currentState = PlayerState.interact;
				receiveItemSprite.sprite = playerInventory.currentItem.itemSprite;
			}
			else
			{
				animator.SetBool("receiveItem", false);
				currentState = PlayerState.idle;
				receiveItemSprite.sprite = null;
				playerInventory.currentItem = null;
			}
		}
	}

	void UpdateAnimationAndMove()
	{
		if (change != Vector3.zero)
		{
			MoveCharacter();
			change.x = Mathf.Round(change.x);
			change.y = Mathf.Round(change.y);
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
			facingDirection = change;
		}
		else { animator.SetBool("moving", false); }
	}

	void MoveCharacter()
	{
		change.Normalize();
		rigidbody2d.MovePosition(transform.position + change * speed * Time.deltaTime);
	}

	//	todo - knockback - move the knockback stuff into its own script
	public void KnockPlayer(float knockTime)
	{
		StartCoroutine(KnockCo(knockTime));
		/*
		//	todo - health
		currentHealth.runtimeValue = Mathf.Max(currentHealth.runtimeValue - damage, 0f);
		playerHealthSignal?.Raise();
		if (currentHealth.runtimeValue > 0)
		{
			if (gameObject.activeInHierarchy)
			{
				//	todo - health
				CameraShakeSignal.Raise();

			}
		}
		else
		{
			gameObject.SetActive(false);
		}
		*/
	}

	private IEnumerator KnockCo(float knockTime)
	{
		CameraShakeSignal.Raise();
		if (rigidbody2d != null)
		{
			StartCoroutine(FlashCo());
			yield return new WaitForSeconds(knockTime);
			rigidbody2d.linearVelocity = Vector2.zero;
			currentState = PlayerState.idle;
			rigidbody2d.linearVelocity = Vector2.zero;
		}
	}

	//	todo - iframes - move the player flashing to its own script
	private IEnumerator FlashCo()
	{
		var temp = 0;
		triggerCollider.enabled = false;
		while (temp < numberOfFlashes)
		{
			sprite.color = flashColor;
			yield return new WaitForSeconds(flashDuration);
			sprite.color = regularColor;
			yield return new WaitForSeconds(flashDuration);
			temp++;
		}
		triggerCollider.enabled = true;
	}

	public IEnumerator AbilityCo(float abilityDuration)
	{
		currentState = PlayerState.ability;
		yield return new WaitForSeconds(abilityDuration);
		currentAbility.Ability(transform.position, facingDirection,
			animator, rigidbody2d);
		currentState = PlayerState.idle;
    }
}