using UnityEngine;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
		public GameObject mainGroup;
		public GameObject optionsGroup;
		public GameObject playGroup;
		public GameObject kingHat;
		public GameObject onlineGroup;
		public bool onlineGroupActive;

		void Start ()
		{
				int currentRezInt = PlayerPrefs.GetInt ("screenRez");
				Resolution[] myResez = Screen.resolutions;
				if (!(currentRezInt > myResez.Length && currentRezInt < 0))
						Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, true);
				int currentGfxInt = PlayerPrefs.GetInt ("GfxQuality");
				string[] qualitySettingNames = QualitySettings.names;
				if (!(currentGfxInt > qualitySettingNames.Length))
						QualitySettings.SetQualityLevel (currentGfxInt, true);
				mainGroup.SetActive (true);
				kingHat.SetActive (true);
				optionsGroup.SetActive (false);
				playGroup.SetActive (false);
				onlineGroup.SetActive (false);
				Screen.lockCursor = false;
		}
		
		public void quitGame ()
		{
				Debug.Log ("the user has tried to quit from the main menu");
				Application.Quit ();
		}

		public void SwitchToMainGroup ()
		{
				mainGroup.SetActive (true);
				kingHat.SetActive (true);
				optionsGroup.SetActive (false);
				playGroup.SetActive (false);
				onlineGroup.SetActive (false);
		}

		public void SwitchToOptionsGroup ()
		{
				mainGroup.SetActive (false);
				kingHat.SetActive (false);
				optionsGroup.SetActive (true);
				playGroup.SetActive (false);
				onlineGroup.SetActive (false);
		}

		public void SwithcToOnlineLobbyFinder ()
		{	
				mainGroup.SetActive (false);
				kingHat.SetActive (false);
				optionsGroup.SetActive (false);
				playGroup.SetActive (false);
				//onlineGroup.SetActive (true);
				onlineGroupActive = true;
		}
		

		public void SwitchToPlayGroup ()
		{
				mainGroup.SetActive (false);
				kingHat.SetActive (true);
				optionsGroup.SetActive (false);
				playGroup.SetActive (true);
				onlineGroup.SetActive (false);
		}
	
}
