using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour
{
	[Header("UI Stuff")]
	[SerializeField] private TextMeshProUGUI itemNumberText;
	[SerializeField] private Image itemImage;

	[Header("Items Variables")]
	public InventoryItem thisItem;
	public InventoryManager thisManager;

	public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
		thisItem = newItem;
		thisManager = newManager;
		if (thisItem != null)
        {
			itemImage.sprite = thisItem.itemImage;
			itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

	void Start()
	{
		
	}

	void Update()
	{

	}
	
	public void OnClicked()
    {
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDiscription, thisItem.usable,
				thisItem);
        }
    }
}