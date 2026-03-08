using UnityEngine;

public class TurretLog : Log
{
    [Header("Turret Settings")]
    public GameObject projectile;
    public float fireDelay = 2f;
    private float fireDelaySecond;
    public bool canFire = true;

    private void Start()
    {
        fireDelaySecond = fireDelay;
    }

    private void Update()
    {
        if (canFire == false)
        {
            fireDelaySecond -= Time.deltaTime;
            if (fireDelaySecond <= 0f)
            {
                canFire = true;
                fireDelaySecond = fireDelay;
            }
        }
    }

    public override void CheckDistance()
    {
        if (target == null || projectile == null || animator == null)
        {
            Debug.LogWarning("Missing references in TurretLog.");
            return;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && canFire)
            {
                Vector3 direction = (target.position - transform.position).normalized;

                GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                Projectile proj = currentProjectile.GetComponent<Projectile>();
                if (proj != null)
                {
                    proj.Launch(direction);
                }
                else
                {
                    Debug.LogWarning("Projectile prefab is missing the Projectile component.");
                }

                canFire = false;
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeup", true);
            }
        }
        else if (distance > chaseRadius)
        {
            animator.SetBool("wakeup", false);
        }
    }
}
