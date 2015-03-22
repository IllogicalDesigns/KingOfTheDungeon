using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using TeamUtility.IO;
using UnityEngine.UI;

public sealed class ControlScript : MonoBehaviour
{
		public Text fireButtonText;
		public Text fireButtonText1;
		public Text fireButtonText2;
		public Text fireButtonText3;
		public GameObject Keyboard;
		public GameObject Gamepad;
		public Color normalColor;
		public Color testColor;
		private int scanIndex = -1;
		private bool justScaned = true;
		public bool keyboardP1 = false;
	
		private void Start ()
		{
				//Load ();
				keyboardP1 = false;
				player1UsingKeyboard ();
				Gamepad.SetActive (false);
				Keyboard.SetActive (true);
		}

		public void player1UsingKeyboard ()
		{
				keyboardP1 = !keyboardP1;
				AxisConfiguration axisConfigMoveHorizontal1 = InputManager.GetAxisConfiguration ("MoveHorizontal");			//1st Player Start
				AxisConfiguration axisConfigMoveVertical1 = InputManager.GetAxisConfiguration ("MoveVertical");
				AxisConfiguration axisConfigRotateVertical1 = InputManager.GetAxisConfiguration ("RotateVertical");
				AxisConfiguration axisConfigRotateHorizontal1 = InputManager.GetAxisConfiguration ("RotateHorizontal");
				AxisConfiguration axisConfigFire1 = InputManager.GetAxisConfiguration ("Fire");								//1st Player end
				AxisConfiguration axisConfigMoveVertical2 = InputManager.GetAxisConfiguration ("MoveVertical2");				//2nd Player Start
				AxisConfiguration axisConfigMoveHorizontal2 = InputManager.GetAxisConfiguration ("MoveHorizontal2");
				AxisConfiguration axisConfigRotateVertical2 = InputManager.GetAxisConfiguration ("RotateVertical2");
				AxisConfiguration axisConfigRotateHorizontal2 = InputManager.GetAxisConfiguration ("RotateHorizontal2");	
				AxisConfiguration axisConfigFire2 = InputManager.GetAxisConfiguration ("Fire2");								//2nd Player end
				AxisConfiguration axisConfigMoveVertical3 = InputManager.GetAxisConfiguration ("MoveVertical3");				//3rd Player Start
				AxisConfiguration axisConfigMoveHorizontal3 = InputManager.GetAxisConfiguration ("MoveHorizontal3");
				AxisConfiguration axisConfigRotateVertical3 = InputManager.GetAxisConfiguration ("RotateVertical3");
				AxisConfiguration axisConfigRotateHorizontal3 = InputManager.GetAxisConfiguration ("RotateHorizontal3");
				AxisConfiguration axisConfigFire3 = InputManager.GetAxisConfiguration ("Fire3");								//3rd Player end
				AxisConfiguration axisConfigMoveVertical4 = InputManager.GetAxisConfiguration ("MoveVertical4");				//4th Player Start
				AxisConfiguration axisConfigMoveHorizontal4 = InputManager.GetAxisConfiguration ("MoveHorizontal4");
				AxisConfiguration axisConfigRotateVertical4 = InputManager.GetAxisConfiguration ("RotateVertical4");
				AxisConfiguration axisConfigRotateHorizontal4 = InputManager.GetAxisConfiguration ("RotateHorizontal4");
				AxisConfiguration axisConfigFire4 = InputManager.GetAxisConfiguration ("Fire4");								//4th Player end

				if (keyboardP1) {
						Debug.Log ("Player1PlayingOnTheKeyBoard");
						Gamepad.SetActive (false);
						Keyboard.SetActive (true);
						SetKey (axisConfigFire1, KeyCode.Mouse0, true);
						axisConfigMoveVertical2.joystick = 0;
						axisConfigMoveHorizontal2.joystick = 0;
						axisConfigRotateVertical2.joystick = 0;
						axisConfigRotateHorizontal2.joystick = 0;
						axisConfigFire2.joystick = 0;
						SetKey (axisConfigFire2, KeyCode.Joystick1Button5, true);
			

						axisConfigMoveVertical3.joystick = 1;
						axisConfigMoveHorizontal3.joystick = 1;
						axisConfigRotateVertical3.joystick = 1;
						axisConfigRotateHorizontal3.joystick = 1;
						axisConfigFire3.joystick = 1;
						SetKey (axisConfigFire3, KeyCode.Joystick2Button5, true);
			
						axisConfigMoveVertical4.joystick = 2;
						axisConfigMoveHorizontal4.joystick = 2;
						axisConfigRotateVertical4.joystick = 2;
						axisConfigRotateHorizontal4.joystick = 2;
						axisConfigFire4.joystick = 2;
						SetKey (axisConfigFire4, KeyCode.Joystick3Button5, true);
						fireButtonText.text = "fire = " + GetKeyName ("Fire", true);
						fireButtonText1.text = "fire = " + GetKeyName ("Fire2", true);
						fireButtonText2.text = "fire = " + GetKeyName ("Fire3", true);
						fireButtonText3.text = "fire = " + GetKeyName ("Fire4", true);
				}
				if (!keyboardP1) {
						Debug.Log ("Player1PlayingOnTheGamePad");
						Gamepad.SetActive (true);
						Keyboard.SetActive (false);
						SetKey (axisConfigFire1, KeyCode.Joystick1Button5, true);
						axisConfigMoveVertical2.joystick = 1;
						axisConfigMoveHorizontal2.joystick = 1;
						axisConfigRotateVertical2.joystick = 1;
						axisConfigRotateHorizontal2.joystick = 1;
						axisConfigFire2.joystick = 1;
						SetKey (axisConfigFire2, KeyCode.Joystick2Button5, true);
				
						axisConfigMoveVertical3.joystick = 2;
						axisConfigMoveHorizontal3.joystick = 2;
						axisConfigRotateVertical3.joystick = 2;
						axisConfigRotateHorizontal3.joystick = 2;
						axisConfigFire3.joystick = 2;
						SetKey (axisConfigFire3, KeyCode.Joystick3Button5, true);
			
						axisConfigMoveVertical4.joystick = 3;
						axisConfigMoveHorizontal4.joystick = 3;
						axisConfigRotateVertical4.joystick = 3;
						axisConfigRotateHorizontal4.joystick = 3;
						axisConfigFire4.joystick = 3;
						SetKey (axisConfigFire4, KeyCode.Joystick4Button5, true);
						fireButtonText.text = "fire = " + GetKeyName ("Fire", true);
						fireButtonText1.text = "fire = " + GetKeyName ("Fire2", true);
						fireButtonText2.text = "fire = " + GetKeyName ("Fire3", true);
						fireButtonText3.text = "fire = " + GetKeyName ("Fire4", true);
				}
		}
	
		private void Update ()
		{
				if (scanIndex == 1) {
						fireButtonText.color = testColor;
						fireButtonText.text = "fire = ";
				} else {
						fireButtonText.color = normalColor;
				}
				if (scanIndex == 11) {
						fireButtonText1.color = testColor;
						fireButtonText1.text = "fire = ";
				} else {
						fireButtonText1.color = normalColor;
				}
				if (scanIndex == 21) {
						fireButtonText2.color = testColor;
						fireButtonText2.text = "fire = ";
				} else {
						fireButtonText2.color = normalColor;
				}
				if (scanIndex == 31) {
						fireButtonText3.color = testColor;
						fireButtonText3.text = "fire = ";
				} else {
						fireButtonText3.color = normalColor;
				}
				if (scanIndex == -1 && justScaned) {
						fireButtonText.text = "fire = " + GetKeyName ("Fire", true);
						fireButtonText1.text = "fire = " + GetKeyName ("Fire2", true);
						fireButtonText2.text = "fire = " + GetKeyName ("Fire3", true);
						fireButtonText3.text = "fire = " + GetKeyName ("Fire4", true);
						justScaned = false;
				}
				if (InputManager.GetButton ("Fire2")) {
						fireButtonText1.color = testColor;
				} else {
						fireButtonText1.color = normalColor;
				}
				if (InputManager.GetButton ("Fire")) {
						fireButtonText.color = testColor;
				} else {
						fireButtonText.color = normalColor;
				}
				if (InputManager.GetButton ("Fire3")) {
						fireButtonText2.color = testColor;
				} else {
						fireButtonText2.color = normalColor;
				}
				if (InputManager.GetButton ("Fire4")) {
						fireButtonText3.color = testColor;
				} else {
						fireButtonText3.color = normalColor;
				}
		}

		public void RebindButton (string whichOne)
		{
				if (whichOne == "fire" && scanIndex == -1) {
						InputManager.StartKeyScan (HandleKeyScanResult, 10.0f, null, "Fire", true);
						scanIndex = 1;
				}
				if (whichOne == "fire2" && scanIndex == -1) {
						InputManager.StartKeyScan (HandleKeyScanResult, 10.0f, null, "Fire2", true);
						scanIndex = 11;
				}
				if (whichOne == "fire3" && scanIndex == -1) {
						InputManager.StartKeyScan (HandleKeyScanResult, 10.0f, null, "Fire3", true);
						scanIndex = 21;
				}
				if (whichOne == "fire4" && scanIndex == -1) {
						InputManager.StartKeyScan (HandleKeyScanResult, 10.0f, null, "Fire4", true);
						scanIndex = 31;
				}
		}
	
		private bool HandleKeyScanResult (KeyCode key, params object[] args)
		{
				//if (!IsValidKeyboardKey (key))
				//return false;
		
				string axisName = (string)args [0];
				bool positive = (bool)args [1];
		
				if (key != KeyCode.None) {
						AxisConfiguration axisConfig = InputManager.GetAxisConfiguration ("KeyboardAndMouse", axisName);
						if (axisConfig != null) {
								SetKey (axisConfig, key, positive);
						}
				}
				scanIndex = -1;
				justScaned = true;
		
				return true;
		}
	
		private bool IsValidKeyboardKey (KeyCode key)
		{
				if ((int)key >= (int)KeyCode.JoystickButton0)
						return false;
				if (key == KeyCode.LeftApple || key == KeyCode.RightApple)
						return false;
				if (key == KeyCode.LeftWindows || key == KeyCode.RightWindows)
						return false;
		
				return true;
		}
	
		private void SetKey (AxisConfiguration axisConfig, KeyCode key, bool positive)
		{
				if (positive) {
						axisConfig.positive = (key == KeyCode.Backspace) ? KeyCode.None : key;
				} else {
						axisConfig.negative = (key == KeyCode.Backspace) ? KeyCode.None : key;
				}
		}
	
		private string GetKeyName (string axisName, bool positive)
		{
				AxisConfiguration axisConfig = InputManager.GetAxisConfiguration ("KeyboardAndMouse", axisName);
				if (axisConfig != null) {
						if (positive) {
								return (axisConfig.positive != KeyCode.None) ? axisConfig.positive.ToString () : string.Empty;
						} else {
								return (axisConfig.negative != KeyCode.None) ? axisConfig.negative.ToString () : string.Empty;
						}
				}
				return string.Empty;
		}
	
		public void Load ()
		{
				if (PlayerPrefs.HasKey ("ControlsMenu.InputConfig")) {
						string xml = PlayerPrefs.GetString ("ControlsMenu.InputConfig");
						using (TextReader reader = new StringReader(xml)) {
								InputLoaderXML loader = new InputLoaderXML (reader);
								InputManager.Load (loader);
						}
				}
		}
	
		public void Save ()
		{
				StringBuilder output = new StringBuilder ();
				InputSaverXML saver = new InputSaverXML (output);
				InputManager.Save (saver);
		
				PlayerPrefs.SetString ("ControlsMenu.InputConfig", output.ToString ());
		}
}