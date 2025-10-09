using UnityEngine;

public enum DoorType
{
	key,
	enemy,
	button
}

public class Door : Interactable
{
	[Header("Door Settings")]
	public DoorType doorType;
	public bool open = false;
	public Inventory playerInventory;
	public SpriteRenderer doorSprite;
	public BoxCollider2D doorCollider;

	private void Start()
	{
		doorSprite = GetComponentInParent<SpriteRenderer>();
		doorCollider = doorSprite.GetComponent<BoxCollider2D>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("AllRounder"))
		{
			if (playerInRange && doorType == DoorType.key)
			{
				if (playerInventory.numberOfKeys > 0)
				{
					playerInventory.numberOfKeys--;
					Open();
				}
			}
		}
	}

	public void Open()
	{
		doorSprite.enabled = false;
		open = true;
		doorCollider.enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}

	public void Close()
	{
	    
	}

}