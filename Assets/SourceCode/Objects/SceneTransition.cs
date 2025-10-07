using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	public string sceneToLoad;
	public Vector2 playerPosition;
	public VectorValue playerStorage;
	public GameObject fadeInPanel;
	public GameObject fadeOutPanel;
	public float fadeWait = 1f;

	private void Awake()
	{
		if (fadeInPanel != null)
		{
			GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
			Destroy(panel, 1f);
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			playerStorage.initialValue = playerPosition;
			//SceneManager.LoadScene(sceneToLoad);
			StartCoroutine(FadeToSceneCo());
		}
	}

	public IEnumerator FadeToSceneCo()
	{
		if (fadeOutPanel != null)
		{
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(fadeWait);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		while (!asyncOperation.isDone)
		{
			yield return null;
		}
	}
}