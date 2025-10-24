using UnityEngine;

public class BoundedNPC : Interactable
{
	private Vector3 directionVector;
	private Transform thisTransform;
	private Rigidbody2D thisRigidbody2D;
	private Animator thisAnimator;
	public Collider2D bounds;
	public float speed = 0.1f;

	void Start()
	{
		thisTransform = GetComponent<Transform>();
		thisRigidbody2D = GetComponent<Rigidbody2D>();
		thisAnimator = GetComponent<Animator>();
		ChangeDirection();
	}

	void Update()
	{
		if (!playerInRange)
        {
			Move();
        }
	}
	
	private void Move()
	{
		var temp = thisTransform.position + directionVector * speed * Time.deltaTime;
		if (bounds.bounds.Contains(temp))
		{
			thisRigidbody2D.MovePosition(temp);
		}
		else { ChangeDirection(); }
	}

	void ChangeDirection()
	{
		int direction = Random.Range(0, 4);
		switch (direction)
		{
			case 0: // walking to the right
				directionVector = Vector3.right;
				break;
			case 1: // walking to the up
				directionVector = Vector3.up;
				break;
			case 2: // walking to the left
				directionVector = Vector3.left;
				break;
			case 3: // walking to the down
				directionVector = Vector3.down;
				break;
			default:
				break;
		}
		UpdateAnimation();
	}

	void UpdateAnimation()
	{
		thisAnimator.SetFloat("moveX", directionVector.x);
		thisAnimator.SetFloat("moveY", directionVector.y);
	}

    private void OnCollisionEnter2D(Collision2D other)
	{
		var temp = directionVector;
		ChangeDirection();
		int loops = 0;
		while (temp == directionVector && loops < 100)
		{
			loops++;
			ChangeDirection();
		}
    }
}