using UnityEngine;
using System.Collections;

public class OnlineGroupOLDGUI : MonoBehaviour
{
		private int lastLevelPrefix = 0;
		public Rpc RpcLink;
		float count = 0;
		public string levelToLoad = "1";
		public GUISkin mySkin;
		private bool onFinder = true;
		private bool onSettings = false;
		private Rect windowRect1 = new Rect (Screen.height / 16, Screen.width / 16, Screen.width / 1.25f, Screen.height / 1.25f);
		private Rect windowRect2 = new Rect (Screen.height / 16, Screen.width / 16, Screen.width / 1.25f, Screen.height / 1.25f);
		public string disconnectedLevel = "MainMenu";
		private string gameName = "KoTD Game";
		private bool refreshing = false;
		private HostData[] hostData;
		private string password = "nimda";
		private string level = "1";
		private string username = "Your Player Name";
		private string ip = "if you see this then there is a problem";
		private string port = "27050";
		private string playerCount = "4";
		private int players;
		private int portint;
		private bool onMainMenu = true;
		private bool onServer = false;
		public MainMenuEvents mainMenuLink;


		// Use this for initialization
		void Start ()
		{
				onServer = false;
				onFinder = true;
				onSettings = false;
				ip = Network.player.ipAddress;
				HostData[] hostdata = MasterServer.PollHostList ();
				DontDestroyOnLoad (this);
				GetComponent<NetworkView>().group = 1;
				Network.SetLevelPrefix (lastLevelPrefix);
				if (Network.HavePublicAddress ()) {
						Debug.Log ("This machine has a public IP address");
				} else {
						Debug.Log ("This machine has a private IP address");
				}
		}

		string ScanForBadWords (string testString)
		{
				badWordFilter badWordLink = gameObject.GetComponent<badWordFilter> ();
				testString = testString.ToLower ();
				foreach (string badWord in badWordLink.profanity) {
						if (testString.Contains (badWord)) {
								testString = testString.Replace (badWord, "Love");
								return testString;
						}
				}
				return testString;
		}
	
		void  Connect (string ip, int port)
		{
				Network.Connect (ip, port, password);
		
		}

		void OnConnectedToServer ()
		{
				Network.RemoveRPCsInGroup (0);
				Network.RemoveRPCsInGroup (1);
				RpcLink.GetComponent<NetworkView>().RPC ("LoadLevel", RPCMode.AllBuffered, level, lastLevelPrefix + 1);	
				Application.LoadLevel (levelToLoad);
				onMainMenu = false;
				PlayerInfo playerInfoLink = gameObject.GetComponent<PlayerInfo> ();
				username = ScanForBadWords (username);
				playerInfoLink.playerName = username;
				Debug.Log ("Connected to server");
				onServer = true;
		}

		void OnFailedToConnect (NetworkConnectionError error)
		{
				mainMenuLink.SwitchToMainGroup ();
				mainMenuLink.onlineGroupActive = false;
		}

		void OnDisconnectedFromServer (NetworkDisconnection info)
		{
				if (Network.isServer)
						Debug.Log ("Local server connection disconnected");
				else
			if (info == NetworkDisconnection.LostConnection)
						Debug.Log ("Lost connection to the server");
				else
						Debug.Log ("Successfully diconnected from the server");
				Application.LoadLevel ("MainMenu");
				Destroy (gameObject, 0.5f);
		}

		void startServer ()
		{
				int.TryParse (playerCount, out players);
				int.TryParse (port, out portint);
				players --;
				Network.incomingPassword = password;
				bool useNat = !Network.HavePublicAddress ();
				Network.InitializeServer (players, portint, useNat);
				gameName = ScanForBadWords (gameName);
				MasterServer.RegisterHost ("KingOfTheDungeon", gameName, level);
				PlayerInfo playerInfoLink = gameObject.GetComponent<PlayerInfo> ();
				username = ScanForBadWords (username);
				playerInfoLink.playerName = username;
				Debug.Log ("started server : " + gameName);
				onServer = true;
		}

		void refreshHostList ()
		{
				MasterServer.RequestHostList ("KingOfTheDungeon");
				//hostData = MasterServer.PollHostList ();
				refreshing = true;
				refreshListNewWay ();
		}

		void DoMyWindow1 (int windowID)
		{
				GUILayout.BeginVertical ();
				GUILayout.Label ("Multiplayer Setup");
				GUILayout.Label ("Username");
				username = GUILayout.TextField (username, 32);
				if (GUILayout.Button ("start a server")) {
						onFinder = false;
						onSettings = true;
				}
				if (GUILayout.Button ("Refresh Server list")) {
						Debug.Log ("Refresh Hosts");
						refreshHostList ();
				}
				if (GUILayout.Button ("Back To Main")) {
						mainMenuLink.SwitchToMainGroup ();
						mainMenuLink.onlineGroupActive = false;
				}
				GUILayout.Label ("password (defult is nimda)");
				password = GUILayout.PasswordField (password, "*" [0], 32);
				GUILayout.Label ("Multiplayer Servers (may need refresh)");
				int i = 0;
				if (MasterServer.PollHostList ().Length > i && hostData == null) {
					Debug.Log("can't seem to find the host data it just returned null");
				}
				if (MasterServer.PollHostList ().Length > i && hostData != null) { 
						foreach (HostData element in hostData) {
								if (GUILayout.Button (element.gameName + " " + element.connectedPlayers + " of " + element.playerLimit + " on level " + element.comment + "[" + element.ip [0] + "]")) { 
										string tmpIp = "";
										tmpIp = element.ip [i] + "";
										Connect (tmpIp, element.port);
								}
								i++;
						}
				}
				GUILayout.EndVertical ();
		}

		void DoMyWindow2 (int windowID)
		{
				GUILayout.BeginVertical ();
				GUILayout.Label ("Server Setup");
				GUILayout.Label ("Your ip address (should be prefilled out)");
				ip = GUILayout.TextField (ip, 32);
		
				GUILayout.Label ("port");
				port = GUILayout.TextField (port, 32);
		
				//GUILayout.Label ("level (1) or level (1)");
				//level = GUILayout.TextField (level, 1);
		
				GUILayout.Label ("Your game's unique name");
				gameName = GUILayout.TextField (gameName, 32);
		
				//GUILayout.Label ("Max Players (recommended 4 or less)");
				//playerCount = GUILayout.TextField (playerCount, 2);
		
				GUILayout.Label ("password (defult is nimda)");
				password = GUILayout.PasswordField (password, "*" [0], 32);

		
				if (GUILayout.Button ("start server")) {
						startServer ();
				}
				if (GUILayout.Button ("Close Server Setup")) {
						onFinder = true;
						onSettings = false;
				}
				GUILayout.EndVertical ();
		}

		void refreshListNewWay(){
		if (MasterServer.PollHostList ().Length > 0) {  
			refreshing = false;
			Debug.Log (MasterServer.PollHostList ().Length);
			hostData = MasterServer.PollHostList ();
			Debug.Log("ran New Way");
		}
		}

		void  Update ()
		{
				if (!onMainMenu || !onServer) {
						count -= Time.deltaTime;
						if (count < 0) {
								refreshHostList ();
								count = 3;
						}
						if (refreshing) {
								if (MasterServer.PollHostList ().Length > 0) {  
										refreshing = false;
										Debug.Log (MasterServer.PollHostList ().Length);
										hostData = MasterServer.PollHostList ();
								}
						}
				}
		}
		void OnGUI ()
		{
				if (onMainMenu) {
						if (mainMenuLink.onlineGroupActive) {
								if (onFinder) {
										windowRect1 = GUILayout.Window (0, windowRect1, DoMyWindow1, "");
								} else if (onSettings) {
										windowRect2 = GUILayout.Window (0, windowRect2, DoMyWindow2, "");
								}
						}
				}
				if (onMainMenu) {
						GUI.skin = mySkin;
						if (Network.isServer) {
								bool first = true;
								if (first) {
										Application.LoadLevel (levelToLoad);
										first = false;
										onMainMenu = false;
								}
						}
				}
		}
}