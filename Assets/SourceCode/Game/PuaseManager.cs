using UnityEngine;
using UnityEngine.SceneManagement;

public class PuaseManager : MonoBehaviour
{
	private bool isPaused;
	public bool usingPausePanel;
	public GameObject pausePanel;
	public GameObject inventoryPanel;
	public string mainMenu;

	void Start()
	{
		isPaused = false;
		pausePanel.SetActive(false);
		inventoryPanel.SetActive(false);
		usingPausePanel = false;
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
			usingPausePanel = true;
		}
		else
		{
			inventoryPanel.SetActive(false);
			pausePanel.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void QuitToMainMenu()
	{
		SceneManager.LoadScene(mainMenu);
		Time.timeScale = 1;
	}
	
	public void SwitchPanels()
    {
		usingPausePanel = !usingPausePanel;
		if (usingPausePanel)
		{
			pausePanel.SetActive(true);
			inventoryPanel.SetActive(false);
		}
		else
		{
			pausePanel.SetActive(false);
			inventoryPanel.SetActive(true);
		}
    }
}