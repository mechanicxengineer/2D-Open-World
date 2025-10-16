using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;

    [Header("Health Values")]
    public FloatValue heartContainer;         // Total number of hearts (e.g., 5 = 10 HP)
    public FloatValue playerCurrentHealth;    // Current health (e.g., 7 = 3 full + 1 half)

    void Start()
    {
        UpdateHearts(); // Sync visuals at start
    }

    public void UpdateHearts()
    {
        int maxHearts = Mathf.Clamp((int)heartContainer.runtimeValue, 0, hearts.Length);
        float currentHealth = Mathf.Clamp(playerCurrentHealth.runtimeValue, 0, heartContainer.runtimeValue * 2f);

        int fullHearts = Mathf.FloorToInt(currentHealth / 2f);
        bool hasHalfHeart = (currentHealth % 2f) != 0;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < maxHearts);

            if (i < fullHearts)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i == fullHearts && hasHalfHeart)
            {
                hearts[i].sprite = halfFullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
