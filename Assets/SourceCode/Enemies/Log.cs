using UnityEngine;

public class Log : Enemy
{
    [Header("Components")]
    public Rigidbody2D logrb;
    public Animator animator;

    [Header("Target Variables")]
    public Transform target;
    public float chaseRadius = 5f;
    public float attackRadius = 1f;

    private void Awake()
    {
        logrb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the Player has the correct tag.");
        }
    }

    private void Start()
    {
        currentState = EnemyState.idle;
        if (animator != null)
        {
            animator.SetBool("wakeup", true);
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            CheckDistance();
        }
    }

    public virtual void CheckDistance()
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

    public void ChangeAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetAnimationFloat(direction.x > 0 ? Vector2.right : Vector2.left);
        }
        else
        {
            SetAnimationFloat(direction.y > 0 ? Vector2.up : Vector2.down);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            // Optional: trigger animation or effects
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
