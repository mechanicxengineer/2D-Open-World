using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Vector2 directionToMove;
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
        if (directionToMove != Vector2.zero)
        {
            Launch(directionToMove);
        }
    }

    public void Launch(Vector2 direction)
    {
        directionToMove = direction;
        if (projectileRigidbody != null)
        {
            projectileRigidbody.linearVelocity = direction * moveSpeed;
        }
    }

}
