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

    void Update()
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
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            logrb.MovePosition(temp);
        }
    }
}
