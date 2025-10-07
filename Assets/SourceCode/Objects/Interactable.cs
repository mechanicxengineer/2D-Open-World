using UnityEngine;

public class Interactable : MonoBehaviour
{
	public SignalObject Context;
	public bool playerInRange;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			Context.Raise();
			playerInRange = true;
		}
	}

	public virtual void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			Context.Raise();
			playerInRange = false;
		}
	}
}