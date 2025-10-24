using UnityEngine;
using UnityEngine.SceneManagement;

public class PuaseManager : MonoBehaviour
{
	private bool isPaused;
	public GameObject pausePanel;
	public string mainMenu;

	void Start()
	{
		isPaused = false;
	}

	void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			ChangePauseToResume_ResumeToPause();
		}
	}

	public void ChangePauseToResume_ResumeToPause()
	{
		isPaused = !isPaused;
		if (isPaused)
		{
			pausePanel.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			pausePanel.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void QuitToMainMenu()
    {
		SceneManager.LoadScene(mainMenu);
		Time.timeScale = 1;
    }
}