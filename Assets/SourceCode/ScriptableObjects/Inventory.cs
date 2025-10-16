using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
	public Item currentItem;
	public List<Item> items = new List<Item>();
	public int numberOfKeys;
	public int Coins;
	public float maxMagic = 10;
	public float currentMagic;

	public void OnEnable()
	{
		currentMagic = maxMagic;
	}
	
	public void UseMagic(float cost)
	{
		currentMagic -= cost;
	}

	public bool CheckForItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
		return false;
    }

	public void AddItem(Item itemToAdd)
	{
		if (itemToAdd.isKey)
		{
			numberOfKeys++;
		}
		else
		{
			if (!items.Contains(itemToAdd))
			{
				items.Add(itemToAdd);
			}
		}
	}
}