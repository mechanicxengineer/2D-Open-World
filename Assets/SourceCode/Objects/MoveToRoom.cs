using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveToRoom : MonoBehaviour
{
	public Vector2 cameraChange;
	public Vector2 playerChange;
	private CameraMovement cameraMovement;
	public bool needText;
	public string placeName;
	public GameObject text;
	public TMP_Text placeText;

	// Start is called before the first frame update
	void Start()
	{
		cameraMovement = Camera.main.GetComponent<CameraMovement>();
		text.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			cameraMovement.minPosition += cameraChange;
			cameraMovement.maxPosition += cameraChange;
			other.transform.position += (Vector3)playerChange;
			if (needText)
			{
				StartCoroutine(PlaceNameCo());
			}
		}
	}

	private IEnumerator PlaceNameCo()
	{
		text.SetActive(true);
		placeText.text = placeName;
		yield return new WaitForSeconds(4f);
		text.SetActive(false);
	}
}