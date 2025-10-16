using System.Collections;
using UnityEngine;

public enum PlayerState
{
	idle,
	walk,
	attack,
	interact,
	stagger
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState currentState;
	public FloatValue currentHealth;
	public SignalObject playerHealthSignal;
	public SignalObject CameraShakeSignal;
	public SignalObject reduceMagicSignal;
	public VectorValue startingPosition;
	public Inventory playerInventory;
	public SpriteRenderer receiveItemSprite;
	public float speed;
	private Rigidbody2D rigidbody2d;
	private Vector3 change;
	private Animator animator;
	[Header("Projectiles")]
	public GameObject arrowProjectile;
	public Item bow;

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
		if (Input.GetButtonDown("Fire1") && currentState != PlayerState.attack &&
			currentState != PlayerState.stagger)
		{
			StartCoroutine(AttackCo());
		}
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
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		}
		else { animator.SetBool("moving", false); }
	}

	void MoveCharacter()
	{
		change.Normalize();
		rigidbody2d.MovePosition(transform.position + change * speed * Time.deltaTime);
	}

	public void KnockPlayer(float knockTime, float damage)
	{
		currentHealth.runtimeValue = Mathf.Max(currentHealth.runtimeValue - damage, 0f);
		playerHealthSignal?.Raise();
		if (currentHealth.runtimeValue > 0)
		{
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(KnockCo(knockTime));
			}
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private IEnumerator KnockCo(float knockTime)
	{
		CameraShakeSignal.Raise();
		if (rigidbody2d != null)
		{
			yield return new WaitForSeconds(knockTime);
			rigidbody2d.velocity = Vector2.zero;
			currentState = PlayerState.idle;
			rigidbody2d.velocity = Vector2.zero;
		}
	}
}