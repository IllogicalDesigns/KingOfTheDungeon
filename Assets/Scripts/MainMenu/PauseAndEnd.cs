using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseAndEnd : MonoBehaviour {
	public GameObject pausePanel;
	public GameObject WinPanel;
	public Text playerWinsText;
	bool alreadyPaused = false;

	// Use this for initialization
	void Start () {
		pausePanel.SetActive (false);
		WinPanel.SetActive (false);
		playerWinsText.text = "player Lame Wins";
	}

	public void PauseGame (){
		alreadyPaused = !alreadyPaused;
		if(alreadyPaused){
			pausePanel.SetActive (false);
			Time.timeScale = 0;
		}else{
		pausePanel.SetActive (true);
		Time.timeScale = 0;
		}
		}

	public void TheWinnerIs (string whoWon){
		if(whoWon!=null) playerWinsText.text = whoWon;
		WinPanel.SetActive (true);
		Application.LoadLevel("MainMenu");
	}

	public void ExitToMain (){
		Time.timeScale = 1;
		Debug.Log ("heading to the main menu");
		Application.LoadLevel ("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			PauseGame();
		}
	
	}
}
