using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Settings")]
    public float speed = 5f;
    public float cost = 1f;
    public float lifetime = 3f;

    [Header("References")]
    public Rigidbody2D arrowRigidbody;

    private float lifetimeTimer;

    void Start()
    {
		lifetimeTimer = lifetime;
		arrowRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(Vector2 velocity, Vector3 direction)
	{
		arrowRigidbody.velocity = velocity.normalized * speed;
		transform.rotation = Quaternion.Euler(direction); 
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
