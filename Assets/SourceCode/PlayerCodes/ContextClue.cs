using UnityEngine;

public class ContextClue : MonoBehaviour
{
	public GameObject contextClue;
	public bool contextActive;

	public void ChangeContext()
	{
		contextActive = !contextActive;
		contextClue.SetActive(contextActive);
	}
}