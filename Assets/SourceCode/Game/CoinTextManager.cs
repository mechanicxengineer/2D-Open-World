using TMPro;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
	public Inventory playerInventory;
	public TextMeshProUGUI coinText;

	public void UpdateCoinCount()
	{
		coinText.text = playerInventory.Coins.ToString();
	}
}