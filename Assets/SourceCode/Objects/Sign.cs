using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
	public GameObject dialogBox;
	public TMP_Text dialogText;
	public string dialog;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Sign") && playerInRange)
		{
			if (dialogBox.activeInHierarchy)
			{
				dialogBox.SetActive(false);
				dialogText.text = "";
			}
			else
			{
				dialogBox.SetActive(true);
				dialogText.text = dialog;
			}
		}
	}

	public override void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			Context.Raise();
			playerInRange = false;
			dialogBox.SetActive(false);
		}
	}
}