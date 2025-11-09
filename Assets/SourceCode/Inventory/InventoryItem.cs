using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
[System.Serializable]
public class InventoryItem : ScriptableObject
{
	public string itemName;
	public string itemDiscription;
	public Sprite itemImage;
	public int numberHeld;
	public bool usable;
	public bool unique;
	public UnityEvent thisEvent;

	public void Use()
	{
		thisEvent.Invoke();
	}

	public void DecreaseAmount(int amountToDecrease)
	{
		numberHeld -= amountToDecrease;
		if (numberHeld < 0) numberHeld = 0;
	}
}