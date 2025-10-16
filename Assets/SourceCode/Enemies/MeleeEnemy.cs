using System.Collections;
using UnityEngine;

public class MeleeEnemy : Log
{
	void Start()
	{
		
	}

	void Update()
	{

	}

	public override void CheckDistance()
	{
		if (target == null || logrb == null || animator == null) return;
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= chaseRadius && distance > attackRadius)
		{
			if (currentState == EnemyState.idle || currentState == EnemyState.walk)
			{
				Vector3 moveTo = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
				Vector2 direction = (moveTo - transform.position).normalized;

				ChangeAnimation(direction);
				logrb.MovePosition(moveTo);
				ChangeState(EnemyState.walk);

			}
		}
		else if (distance <= chaseRadius && distance <= attackRadius)
		{
			if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
				StartCoroutine(AttackCo());
            }
		}
	}
	
	public IEnumerator AttackCo()
    {
		currentState = EnemyState.attack;
		animator.SetBool("attack", true);
		yield return new WaitForSeconds(.5f);
		currentState = EnemyState.walk;
		animator.SetBool("attack", false);
    }
}