using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	void Start()
	{
		
	}

	void Update()
	{

	}

	public void SeekTheTruth()
	{
		SceneManager.LoadScene("OpenWorld");
	}

	public void QuitToDesktop()
	{
		Application.Quit();
	}	
	
}