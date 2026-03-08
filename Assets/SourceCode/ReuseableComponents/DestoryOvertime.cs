using UnityEngine;

public class DestoryOvertime : MonoBehaviour
{
	[Header("Lifetime")]
    [SerializeField] private float lifetime = 2f;

	void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}