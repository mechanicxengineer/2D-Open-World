using UnityEngine;

public class AreaLog : Log
{
    [Header("Area Restriction")]
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if (target == null || boundary == null || logrb == null || animator == null)
        {
            Debug.LogWarning("Missing references in AreaLog.");
            return;
        }

        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(targetPos, transform.position);

        // Convert to 2D for boundary check
        Vector2 target2D = new Vector2(targetPos.x, targetPos.y);

        if (distance <= chaseRadius && distance > attackRadius && boundary.bounds.Contains(target2D))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 moveTo = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                Vector2 direction = (moveTo - transform.position).normalized;

                ChangeAnimation(direction);
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

        if (boundary != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boundary.bounds.center, boundary.bounds.size);
        }
    }
}
