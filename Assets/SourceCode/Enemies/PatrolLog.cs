using UnityEngine;

public class PatrolLog : Log
{
    [Header("Patrol Settings")]
    public Transform[] path;
    public int currentPoint = 0;
    public Transform currentGoal;
    public float roundingDistance = 0.5f;

    private void Start()
    {
        if (path != null && path.Length > 0)
        {
            currentGoal = path[currentPoint];
        }
        else
        {
            Debug.LogWarning("Patrol path is not set.");
        }
    }

    public override void CheckDistance()
    {
        if (target == null || logrb == null || animator == null || path == null || path.Length == 0)
            return;

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 moveTo = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimation(moveTo - transform.position);
                logrb.MovePosition(moveTo);
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeup", true);
            }
        }
        else if (distanceToTarget > chaseRadius)
        {
            float distanceToGoal = Vector3.Distance(transform.position, currentGoal.position);

            if (distanceToGoal > roundingDistance)
            {
                Vector3 moveTo = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);
                ChangeAnimation(moveTo - transform.position);
                logrb.MovePosition(moveTo);
                ChangeState(EnemyState.walk);
            }
            else
            {
                ChangeGoal();
            }

            animator.SetBool("wakeup", false);
        }
    }

	private void ChangeGoal()
	{
		if (path == null || path.Length == 0) return;

		currentPoint = (currentPoint + 1) % path.Length;
		currentGoal = path[currentPoint];
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		for (int i = 0; i < path.Length; i++)
		{
			if (path[i] != null)
			{
				Gizmos.DrawSphere(path[i].position, 0.2f);
				if (i + 1 < path.Length && path[i + 1] != null)
				{
					Gizmos.DrawLine(path[i].position, path[i + 1].position);
				}
			}
		}
	}

}
