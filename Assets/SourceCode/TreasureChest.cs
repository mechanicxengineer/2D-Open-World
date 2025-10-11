using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : Interactable
{
	[Header("Contents")]
	public Item contents;
	public Inventory playerInventory;
	public bool isOpen;

	public BoolValue storedOpen;
	[Header("Signal and Dialog")]
	public SignalObject raiseItem;
	public GameObject dialogBox;
	public TMP_Text dialogText;

	[Header("Animation")]
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		isOpen = storedOpen.runtimeValue;
		if (isOpen)
        {
			animator.SetBool("opened", true);
        }
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("AllRounder") && playerInRange)
		{
			if (!isOpen)
			{
				//	open chest
				OpenChest();
			}
			else
			{
				//	chest is already open
				ChestAlreadyOpen();
			}
		}
	}

	public void OpenChest()
	{
		dialogBox.SetActive(true);
		dialogText.text = contents.itemDescription;
		playerInventory.AddItem(contents);
		playerInventory.currentItem = contents;
		raiseItem.Raise();
		Context.Raise();
		isOpen = true;
		animator.SetBool("opened", true);
		storedOpen.runtimeValue = isOpen;
	}

	public void ChestAlreadyOpen()
	{
		dialogBox.SetActive(false);
		raiseItem.Raise();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
		{
			Context.Raise();
			playerInRange = true;
		}
	}

	public override void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
		{
			Context.Raise();
			playerInRange = false;
		}
	}
}