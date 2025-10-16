using UnityEngine;

public class HeartContainer : PowerUp
{
    [Header("Heart Upgrade Values")]
    public FloatValue heartContainer;     // Total number of hearts
    public FloatValue playerHealth;       // Current health value

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (heartContainer != null && playerHealth != null)
            {
                heartContainer.runtimeValue += 1;
                playerHealth.runtimeValue = heartContainer.runtimeValue * 2f;
            }

            if (powerUpSignal != null)
            {
                powerUpSignal.Raise();
            }

            Destroy(gameObject);
        }
    }
}
