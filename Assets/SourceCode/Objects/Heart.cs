using UnityEngine;

public class Heart : PowerUp
{
	public FloatValue playerHealth;
	public FloatValue heathContainers;
	public float amountToIncrease;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			playerHealth.runtimeValue += amountToIncrease;
			if (playerHealth.initialValue > heathContainers.runtimeValue * 2f)
			{
				playerHealth.initialValue = heathContainers.runtimeValue * 2f;
			}
			powerUpSignal.Raise();
			Destroy(this.gameObject);
		}
	}
}