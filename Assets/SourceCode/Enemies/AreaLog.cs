using UnityEngine;

public class AreaLog : Log
{
	public Collider2D boundary;

	public override void CheckDistance()
	{
		if (target == null || boundary == null || animator == null)
		{
			Debug.LogWarning("Missing references in CheckDistance.");
			return;
		}

		Vector3 targetPos = target.position;
		float distance = Vector3.Distance(targetPos, transform.position);

		if (distance <= chaseRadius && distance > attackRadius && boundary.bounds.Contains(targetPos))
		{
			if (currentState == EnemyState.idle || currentState == EnemyState.walk)
			{
				Vector3 moveTo = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
				ChangeAnimation(moveTo - transform.position);
				logrb.MovePosition(moveTo);
				ChangeState(EnemyState.walk);
				animator.SetBool("wakeup", true);
			}
		}
		else
		{
			animator.SetBool("wakeup", false);
		}
	}


	private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);     
    }
}