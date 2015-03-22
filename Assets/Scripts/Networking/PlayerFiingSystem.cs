using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerFiingSystem : MonoBehaviour
{
		public Text unOrganizedPlayers;
		public List<string> currUnOrganizedPlayers = new List<string> ();
		public PlayerInfo[] playerInfos;
		public string newestPlayer;

		// Use this for initialization
		void Start ()
		{
				Debug.Log (Network.player.ipAddress + " connected to server");
				int i = 0;
				playerInfos = FindObjectsOfType (typeof(PlayerInfo)) as PlayerInfo[];
				if (currUnOrganizedPlayers.Count != 0) {
						foreach (PlayerInfo pInfo in playerInfos) {
								if (!currUnOrganizedPlayers.Contains (pInfo.playerName)) {
										newestPlayer = pInfo.playerName;
										currUnOrganizedPlayers.Add (playerInfos [i].playerName);
										i++;
								}
						}
				} else {
						currUnOrganizedPlayers.Add (playerInfos [i].playerName);
				}
				updateTextUnOrgaizedPlayers ();
		}

		void updateTextUnOrgaizedPlayers ()
		{ 
				int i = 0;
				if (currUnOrganizedPlayers.Count == 0)
						unOrganizedPlayers.text = "none";
				if (currUnOrganizedPlayers.Count == 1)
						unOrganizedPlayers.text = currUnOrganizedPlayers [i];
				if (currUnOrganizedPlayers.Count == 2)
						unOrganizedPlayers.text = currUnOrganizedPlayers [i] + " " + currUnOrganizedPlayers [i + 1];
				if (currUnOrganizedPlayers.Count == 3)
						unOrganizedPlayers.text = currUnOrganizedPlayers [i] + " " + currUnOrganizedPlayers [i + 1] + " " +
								currUnOrganizedPlayers [i + 2];
				if (currUnOrganizedPlayers.Count == 4)
						unOrganizedPlayers.text = currUnOrganizedPlayers [i] + " " + currUnOrganizedPlayers [i + 1] + " " +
								currUnOrganizedPlayers [i + 2] + " " + currUnOrganizedPlayers [i + 3];
		}

		void OnPlayerConnected (NetworkPlayer player)
		{
				Debug.Log (player.ipAddress + " connected to server");
				int i = 0;
				playerInfos = FindObjectsOfType (typeof(PlayerInfo)) as PlayerInfo[];
				if (currUnOrganizedPlayers.Count != 0) {
						foreach (PlayerInfo pInfo in playerInfos) {
								if (!currUnOrganizedPlayers.Contains (pInfo.playerName)) {
										newestPlayer = pInfo.playerName;
										currUnOrganizedPlayers.Add (playerInfos [i].playerName);
										i++;
								}
						}
				} else {
						currUnOrganizedPlayers.Add (playerInfos [i].playerName);
				}
				updateTextUnOrgaizedPlayers ();
		}

	
		// Update is called once per frame
		void Update ()
		{
		}
}
