using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[Header("Inventory Information")]
	public PlayerInventory playerInventory;
	[SerializeField] private GameObject blankInventorySlot;
	[SerializeField] private GameObject Content;
	[SerializeField] TextMeshProUGUI itemDescriptionText;
	[SerializeField] private GameObject useButton;
	[SerializeField] private InventoryItem currentItem;

	public void SetTextAndButton(string description, bool active)
    {
		itemDescriptionText.text = description;
		if (active)
		{
			useButton.SetActive(true);
			
		}
		else
		{
			useButton.SetActive(false);

		}
    }

	void MakeInventorySlots()
    {
        if (playerInventory)
		{
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
			{
				if (playerInventory.myInventory[i].numberHeld > 0 ||
					playerInventory.myInventory[i].itemName == "Bottle")
                {
					GameObject temp = Instantiate(blankInventorySlot, Content.transform.position,
						quaternion.identity);
					temp.transform.SetParent(Content.transform);
					temp.transform.localScale = new Vector3(1, 1, 1);
					InventorySlot newSlot = temp.GetComponent<InventorySlot>();
					if (newSlot != null)
					{
						newSlot.Setup(playerInventory.myInventory[i], this);
					}
                }
            }
        }
    }

	void OnEnable()
	{
		ClearInventorySlots();
		MakeInventorySlots();
		Debug.Log("Inventory Manager Enabled");
		SetTextAndButton("", false);
	}

	public void SetupDescriptionAndButton(string newDescription, bool isActiveButton,
		InventoryItem newItem)
	{
		currentItem = newItem;
		itemDescriptionText.text = newDescription;
		useButton.SetActive(isActiveButton);

	}

	public void ClearInventorySlots()
    {
		for(int i = 0; i < Content.transform.childCount; i++)
        {
			Destroy(Content.transform.GetChild(i).gameObject);
		}
    }

	public void UseItem()
	{
		if (currentItem != null) currentItem.Use();
		ClearInventorySlots();
		MakeInventorySlots();
		if (currentItem.numberHeld == 0)
		{
			SetTextAndButton("", false);
		}
	}

}