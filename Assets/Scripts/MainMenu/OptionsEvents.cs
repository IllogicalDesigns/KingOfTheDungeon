using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsEvents : MonoBehaviour
{
		//Screen Stuff
		Resolution[] myResez;
		int currentRezInt;
		public Text screenResolutionText;
		public static bool fullScreenBool = true;
		//Graphics Stuff
		public Text graphicsQualityText;
		public int currentGfxInt;
		public string[] qualitySettingNames;

		void Start ()
		{
				//screen Stuff
				currentRezInt = PlayerPrefs.GetInt ("screenRez");
				myResez = Screen.resolutions;
				Debug.Log ("current Resolution is " + currentRezInt + " of " + myResez.Length);
					screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
				Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
				//GraphicsStuff
				currentGfxInt = PlayerPrefs.GetInt ("GfxQuality");
				qualitySettingNames = QualitySettings.names;
				Debug.Log ("current Gfx lvl is " + currentGfxInt + " of " + qualitySettingNames.Length);
				if(!(currentGfxInt > qualitySettingNames.Length))
				graphicsQualityText.text = qualitySettingNames [currentGfxInt];
				QualitySettings.SetQualityLevel (currentGfxInt, true);
		}

		public void ChangeFullScreen (){
		fullScreenBool = !fullScreenBool;
		Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
		}

		public void ChangeResolution (bool increasing)
		{
				if (increasing && (currentRezInt + 1) != (myResez.Length)) {
						currentRezInt ++;
						screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
					Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
					Debug.Log ("increase Resolution");
				} else if (!increasing && (currentRezInt - 1) != - 1) {
						currentRezInt --;
						screenResolutionText.text = myResez [currentRezInt].width + " x " + myResez [currentRezInt].height;
				Screen.SetResolution (myResez [currentRezInt].width, myResez [currentRezInt].height, fullScreenBool);
				Debug.Log ("decrease Resolution");
				}
		}

		public void QualitySetting (bool increasing)
		{
		if (qualitySettingNames.Length > 0) {
						if (increasing && (currentGfxInt + 1) != qualitySettingNames.Length) {
								currentGfxInt ++;
								graphicsQualityText.text = qualitySettingNames [currentGfxInt];
								QualitySettings.SetQualityLevel (currentGfxInt, true);
								Debug.Log ("increase Quality");
						} else if (!increasing && (currentGfxInt - 1) != - 1) {
								currentGfxInt --;
								graphicsQualityText.text = qualitySettingNames [currentGfxInt];
								QualitySettings.SetQualityLevel (currentGfxInt, true);
								Debug.Log ("decrease Quality");
						}
				}
		}

		public void SaveSettings ()
		{
				PlayerPrefs.SetInt ("screenRez", currentRezInt);
				PlayerPrefs.SetInt ("GfxQuality", currentGfxInt);
		}
}
