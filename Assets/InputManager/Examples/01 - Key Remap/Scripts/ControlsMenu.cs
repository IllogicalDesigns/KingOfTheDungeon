using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using TeamUtility.IO;

public sealed class ControlsMenu : MonoBehaviour 
{
	private int _scanIndex = -1;
	private bool _showGUI = true;

	private void Start()
	{
		//Load();
	}

	private void Update()
	{
		if(!_showGUI && InputManager.GetKeyDown(KeyCode.F1))
		{
			_scanIndex = -1;
			_showGUI = true;
		}
	}

	private bool HandleKeyScanResult(KeyCode key, params object[] args)
	{
		if(!IsValidKeyboardKey(key))
			return false;

		string axisName = (string)args[0];
		bool positive = (bool)args[1];

		if(key != KeyCode.None)
		{
			AxisConfiguration axisConfig = InputManager.GetAxisConfiguration("Keyboard", axisName);
			if(axisConfig != null)
			{
				SetKey(axisConfig, key, positive);
			}
		}
		_scanIndex = -1;

		return true;
	}

	private bool IsValidKeyboardKey(KeyCode key)
	{
		if((int)key >= (int)KeyCode.JoystickButton0)
			return false;
		if(key == KeyCode.LeftApple || key == KeyCode.RightApple)
			return false;
		if(key == KeyCode.LeftWindows || key == KeyCode.RightWindows)
			return false;
		
		return true;
	}

	private void SetKey(AxisConfiguration axisConfig, KeyCode key, bool positive)
	{
		if(positive)
		{
			axisConfig.positive = (key == KeyCode.Backspace) ? KeyCode.None : key;
		}
		else
		{
			axisConfig.negative = (key == KeyCode.Backspace) ? KeyCode.None : key;
		}
	}

	private string GetKeyName(string axisName, bool positive)
	{
		AxisConfiguration axisConfig = InputManager.GetAxisConfiguration("Keyboard", axisName);
		if(axisConfig != null)
		{
			if(positive)
			{
				return (axisConfig.positive != KeyCode.None) ? axisConfig.positive.ToString() : string.Empty;
			}
			else
			{
				return (axisConfig.negative != KeyCode.None) ? axisConfig.negative.ToString() : string.Empty;
			}
		}

		return string.Empty;
	}

	private void Load()
	{
		if(PlayerPrefs.HasKey("ControlsMenu.InputConfig"))
		{
			string xml = PlayerPrefs.GetString("ControlsMenu.InputConfig");
			using(TextReader reader = new StringReader(xml))
			{
				InputLoaderXML loader = new InputLoaderXML(reader);
				InputManager.Load(loader);
			}
		}
	}

	private void Save()
	{
		StringBuilder output = new StringBuilder();
		InputSaverXML saver = new InputSaverXML(output);
		InputManager.Save(saver);

		PlayerPrefs.SetString("ControlsMenu.InputConfig", output.ToString());
	}
}
