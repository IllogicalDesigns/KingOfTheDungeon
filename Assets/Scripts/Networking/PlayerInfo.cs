using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	public string playerName;

	// Use this for initialization
	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log ("I have connected as " + playerName + " from " + player.ipAddress);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
