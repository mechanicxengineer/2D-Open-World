using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D logrb;
    public Transform target;
    public Transform homePosition;
    public Animator animator;
    public float chaseRadius;
    public float attackRadius;

    void Start()
    {
        currentState = EnemyState.idle;
        logrb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in scene. Make sure the Player has the correct tag.");
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            CheckDistance();
        }
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnimation(temp - transform.position);
                logrb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeup", true);
            }
        }
        else if (distance > chaseRadius)
        {
            animator.SetBool("wakeup", false);
        }
    }

    private void SetAnimationFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

    private void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimationFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimationFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimationFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimationFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            // Perform any additional actions needed when changing states
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
