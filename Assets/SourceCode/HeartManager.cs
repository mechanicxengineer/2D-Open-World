using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainer;
    public FloatValue playerCurrentHealth;

    void Start()
    {
        InitHearts();
        UpdateHearts(); // Optional: sync visuals at start
    }

    public void InitHearts()
    {
        int maxHearts = Mathf.Clamp((int)heartContainer.initialValue, 0, hearts.Length);

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < maxHearts);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float currentHealth = playerCurrentHealth.runtimeValue;
        int fullHearts = Mathf.FloorToInt(currentHealth / 2f);
        bool hasHalfHeart = (currentHealth % 2f) != 0;

        for (int i = 0; i < hearts.Length; i++)
        {
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
