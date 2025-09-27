using System.Collections;
using UnityEngine;

public enum PlayerState
{
	walk,
	attack,
	interact
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D rigidbody2d;
	private Vector3 change;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");
		if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack)
		{
			StartCoroutine(AttackCo());
		}
		else if (currentState == PlayerState.walk)
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
		currentState = PlayerState.walk;
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
		rigidbody2d.MovePosition(transform.position + change * speed * Time.deltaTime);
	}
}