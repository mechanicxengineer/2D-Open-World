using UnityEngine;

public class BoundedNPC : Sign
{
	private Vector3 directionVector;
	private Transform thisTransform;
	private Rigidbody2D thisRigidbody2D;
	private Animator thisAnimator;
	public Collider2D bounds;
	public float speed = 0.1f;
	public float minMoveTime;
	public float maxMoveTime;
	private float moveTimeSeconds;
	public float minWaitTime;
	public float maxWaitTime;
	private float waitTimeSeconds;
	private bool isMoving;

	void Start()
	{
		moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
		waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
		thisTransform = GetComponent<Transform>();
		thisRigidbody2D = GetComponent<Rigidbody2D>();
		thisAnimator = GetComponent<Animator>();
		ChangeDirection();
	}

	public override void Update()
	{
		base.Update();
		if (isMoving)
		{
			moveTimeSeconds -= Time.deltaTime;
			if (moveTimeSeconds <= 0)
			{
				isMoving = false;
				moveTimeSeconds = moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
			}

			if (!playerInRange)
			{
				Move();
			}
		}
		else
		{
			waitTimeSeconds -= Time.deltaTime;
			if (waitTimeSeconds <= 0)
			{
				isMoving = true;
				waitTimeSeconds = waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
				ChooseDifferentDirection();
			}
		}
		
	}

	private void ChooseDifferentDirection()
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
		ChooseDifferentDirection();
    }
}