using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public static int howManyPlayers = 4;
	public static int volume = 1;
	public int manyPlayers = 2;
	public TextMesh textPlayerNum;
	public string lvl2Load;

	// Use this for initialization
	void Awake () {
		howManyPlayers = manyPlayers;
		textPlayerNum.text = manyPlayers.ToString ();
	}

	void Start () {
		howManyPlayers = manyPlayers;
		textPlayerNum.text = manyPlayers.ToString ();
	}

	void AdjustPlayerCount (string direction) {
		if (howManyPlayers >= 0 && howManyPlayers <= 5) {
			if (direction == "up" && (howManyPlayers + 1) < 5) {
				manyPlayers ++;
				howManyPlayers = manyPlayers;
				textPlayerNum.text = manyPlayers.ToString ();
			}
			if (direction == "down" && (howManyPlayers - 1) > 0) {
				manyPlayers --;
				howManyPlayers = manyPlayers;
				textPlayerNum.text = manyPlayers.ToString ();
			}
		}
	}

	void StartGame () {
		howManyPlayers = manyPlayers;
		Application.LoadLevel (lvl2Load);

	}
	
	// Update is called once per frame
}
