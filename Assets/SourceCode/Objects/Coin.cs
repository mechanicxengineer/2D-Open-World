using UnityEngine;

public class Coin : PowerUp
{
	public Inventory playerInventory;

	// Start is called before the first frame update
	void Start()
	{
		powerUpSignal.Raise();
	}

	// Update is called once per frame
	void Update()
	{

	}

		private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			playerInventory.Coins += 1;
			powerUpSignal.Raise();
			Destroy(this.gameObject);
		}
	}
}