using UnityEngine;
using System.Collections;

public class ServerClassScript : MonoBehaviour {
	public class ServerInfo {
		public string serverName;
		public string gameMode;
		public int currentPlayers; //Can this be here
		public int maxPlayers;
		public int pingToServer;  //Can tis be here
		public string serverMap;
		public bool isPassworded;
		public string password;
		public string ipAddress;

		public ServerInfo (string currServerName, string currGameMode, int currMaxPlayers, string currServerMap, bool currPassword, string currPasswordString){	//The Setup Constructor
			serverName = currServerName;
			gameMode = currGameMode;
			maxPlayers = currMaxPlayers;
			serverMap = currServerMap;
			isPassworded = currPassword;
			password = currPasswordString;
		}

		public ServerInfo (int currPing){
			pingToServer = currPing;
		}

		public ServerInfo (int currPlayers, int currMaxPlayers){
			maxPlayers = currMaxPlayers;
			currentPlayers = currPlayers;
		}
	}
}
