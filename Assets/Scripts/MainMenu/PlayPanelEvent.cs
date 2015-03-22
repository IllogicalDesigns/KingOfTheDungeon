using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayPanelEvent : MonoBehaviour
{
		public Slider playerCountSliderUI;
		public Text playerCountText;
		public Button StartButton;
		public GameObject[] indivdualPlayerOptions;
		public GameObject[] readyBanners;
		public GameObject[] ControlPanels;
		bool keyboard = true;
		public static string whoHasKeyboard = "null";
		private bool everyoneReady = false;
		private bool player1Ready = false;
		private bool player2Ready = false;
		private bool player3Ready = false;
		private bool player4Ready = false;
		private bool player1Controls = false;
		private bool player2Controls = false;
		private bool player3Controls = false;
		private bool player4Controls = false;
		private bool playerInControlls = false;
		int lastPlayerCount;

		void Awake ()
		{
				StartButton.gameObject.SetActive (false);
				foreach (GameObject banner in readyBanners) {
						banner.gameObject.SetActive (false);
				}
				UpdatePlayerOptions ();
		}

		void UpdatePlayerOptions ()
		{
				if (MainMenu.howManyPlayers == 1) {
						indivdualPlayerOptions [0].gameObject.SetActive (true);
						indivdualPlayerOptions [1].gameObject.SetActive (false);
						indivdualPlayerOptions [2].gameObject.SetActive (false);
						indivdualPlayerOptions [3].gameObject.SetActive (false);
				}

				if (MainMenu.howManyPlayers == 2) {
						indivdualPlayerOptions [0].gameObject.SetActive (true);
						indivdualPlayerOptions [1].gameObject.SetActive (true);
						indivdualPlayerOptions [2].gameObject.SetActive (false);
						indivdualPlayerOptions [3].gameObject.SetActive (false);
				}

				if (MainMenu.howManyPlayers == 3) {
						indivdualPlayerOptions [0].gameObject.SetActive (true);
						indivdualPlayerOptions [1].gameObject.SetActive (true);
						indivdualPlayerOptions [2].gameObject.SetActive (true);
						indivdualPlayerOptions [3].gameObject.SetActive (false);
				}

				if (MainMenu.howManyPlayers == 4) {
						indivdualPlayerOptions [0].gameObject.SetActive (true);
						indivdualPlayerOptions [1].gameObject.SetActive (true);
						indivdualPlayerOptions [2].gameObject.SetActive (true);
						indivdualPlayerOptions [3].gameObject.SetActive (true);
				}

		}
		
		public void RebindControlMenu (int whichOne)
		{
				if (whichOne == 1)
						player1Controls = !player1Controls;
				if (whichOne == 2)
						player2Controls = !player2Controls;
				if (whichOne == 3)
						player3Controls = !player3Controls;
				if (whichOne == 4)
						player4Controls = !player4Controls;
				if (player1Controls) {
						ControlPanels [0].gameObject.SetActive (true);
						player1Ready = false;
				} else {
						ControlPanels [0].gameObject.SetActive (false);
				}
				if (player2Controls) {
						ControlPanels [1].gameObject.SetActive (true);
						player2Ready = false;
				} else {
						ControlPanels [1].gameObject.SetActive (false);
				}
				if (player3Controls) {
						ControlPanels [2].gameObject.SetActive (true);
						player3Ready = false;
				} else {
						ControlPanels [2].gameObject.SetActive (false);
				}
				if (player4Controls) {
						ControlPanels [3].gameObject.SetActive (true);
						player4Ready = false;
				} else {
						ControlPanels [3].gameObject.SetActive (false);
				}
				
		}

		public void keyboardOrGamePad (int whichPlayer)
		{
				keyboard = !keyboard;
				if (keyboard) {
						whoHasKeyboard = whichPlayer.ToString ();
				} else {
						whoHasKeyboard = "null";
				}
		}
		
		public void ReadyAPlayer (int whichOne)
		{
				if (whichOne == 1)
						player1Ready = !player1Ready;
				if (whichOne == 2)
						player2Ready = !player2Ready;
				if (whichOne == 3)
						player3Ready = !player3Ready;
				if (whichOne == 4)
						player4Ready = !player4Ready;
				if (player1Ready) {
						readyBanners [0].gameObject.SetActive (true);
				} else {
						readyBanners [0].gameObject.SetActive (false);
				}
				if (player2Ready) {
						readyBanners [1].gameObject.SetActive (true);
				} else {
						readyBanners [1].gameObject.SetActive (false);
				}
				if (player3Ready) {
						readyBanners [2].gameObject.SetActive (true);
				} else {
						readyBanners [2].gameObject.SetActive (false);
				}
				if (player4Ready) {
						readyBanners [3].gameObject.SetActive (true);
				} else {
						readyBanners [3].gameObject.SetActive (false);
				}
		}

		public void UpdatePlayerNumbers ()
		{
				playerCountText.text = playerCountSliderUI.value.ToString ();
				MainMenu.howManyPlayers = Mathf.RoundToInt (playerCountSliderUI.value);
				if (MainMenu.howManyPlayers == 3) {
						ControlPanels [3].gameObject.SetActive (false);
						player4Controls = false;
				}
				if (MainMenu.howManyPlayers == 2) {
						ControlPanels [2].gameObject.SetActive (false);
						ControlPanels [3].gameObject.SetActive (false);
						player4Controls = false;
						player3Controls = false;
				}
				if (MainMenu.howManyPlayers == 1) {
						ControlPanels [1].gameObject.SetActive (false);
						ControlPanels [2].gameObject.SetActive (false);
						ControlPanels [3].gameObject.SetActive (false);
						player4Controls = false;
						player3Controls = false;
						player2Controls = false;
				}
		}

		public void LoadLevel (string lvlName)
		{
				Application.LoadLevel (lvlName);
		}
		
		private void readyBanner ()
		{
				if (everyoneReady) {
						StartButton.gameObject.SetActive (true);
				} else {
						StartButton.gameObject.SetActive (false);
				}

				if (player1Ready && !playerInControlls && MainMenu.howManyPlayers == 1) {
						everyoneReady = true;
				} else if (MainMenu.howManyPlayers == 1) {
						everyoneReady = false;
				}
				if (player1Ready && !playerInControlls && player2Ready && MainMenu.howManyPlayers == 2) {
						everyoneReady = true;
				} else if (MainMenu.howManyPlayers == 2) {
						everyoneReady = false;
				}
				if (player1Ready && !playerInControlls && player2Ready && player3Ready && MainMenu.howManyPlayers == 3) {
						everyoneReady = true;
				} else if (MainMenu.howManyPlayers == 3) {
						everyoneReady = false;
				}
				if (player1Ready && !playerInControlls && player2Ready && player3Ready && player4Ready && MainMenu.howManyPlayers == 4) {
						everyoneReady = true;
				} else if (MainMenu.howManyPlayers == 4) {
						everyoneReady = false;
				}

		}

		void Update ()
		{
				readyBanner ();
				if (MainMenu.howManyPlayers != lastPlayerCount) {
						lastPlayerCount = MainMenu.howManyPlayers;
						UpdatePlayerOptions ();
				}
		}
}
