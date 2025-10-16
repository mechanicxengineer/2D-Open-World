using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector2 directionToMove;

    [Header("Lifetime")]
    public float lifeTime = 2f;
    private float lifetimeSeconds;

    private Rigidbody2D projectileRigidbody;

    void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
        if (projectileRigidbody == null)
        {
            Debug.LogError("Projectile is missing Rigidbody2D.");
        }
    }

    void Start()
    {
        lifetimeSeconds = lifeTime;

        if (directionToMove != Vector2.zero)
        {
            Launch(directionToMove);
        }
    }

    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction)
    {
        directionToMove = direction;

        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = direction * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Optional: Filter by tag
        // if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        // {
        //     Destroy(gameObject);
        // }

        Destroy(gameObject);
    }
}
