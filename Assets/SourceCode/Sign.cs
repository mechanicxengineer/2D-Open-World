using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
	public GameObject dialogBox;
	public TMP_Text dialogText;
	public string dialog;
	public bool playerInRange;

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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Player is near sign.");
			playerInRange = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Player is leaving sign.");
			playerInRange = false;
			dialogBox.SetActive(false);
		}
	}
}