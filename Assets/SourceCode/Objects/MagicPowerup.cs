using UnityEngine;

public class MagicPowerup : PowerUp
{
    [Header("Power-Up Settings")]
    public Inventory playerInventory;
    public float cost = 1f;

    void Start()
    {
        // Optional initialization
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory.currentMagic += cost;
            powerUpSignal?.Raise();
            Destroy(gameObject);
        }
    }
}
