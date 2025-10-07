using UnityEngine;

public class PatrolLog : Log
{
	public Transform[] path;
	public int currentPoint;
	public Transform currentGoal;
	public float roundingDistance;

	public override void CheckDistance()
	{
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= chaseRadius && distance > attackRadius)
		{
			if (currentState == EnemyState.idle || currentState == EnemyState.walk)
			{
				Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
				ChangeAnimation(temp - transform.position);
				logrb.MovePosition(temp);
				//ChangeState(EnemyState.walk);
				animator.SetBool("wakeup", true);
			}
		}
		else if (distance > chaseRadius)
		{
			if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
			{
				Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
				ChangeAnimation(temp - transform.position);
				logrb.MovePosition(temp);
			}
			else
			{
				ChangeGoal();
			}
		}
	}

	private void ChangeGoal()
	{
		if (currentPoint == path.Length - 1)
		{
			currentPoint = 0;
			currentGoal = path[0];
		}
		else
		{
			currentPoint++;
			currentGoal = path[currentPoint];
		}
	}
}