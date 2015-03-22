using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
	//Gui Stuff start ----------------------------------------
	public GUISkin mySkin;
	public string levelToLoad;
	public Transform playerPrefab;
	public Rpc RpcLink;

	private int lastLevelPrefix = 0;

	private float inGameVolume = 2f;

	private bool onMainMenu = true;
	private bool main;
	private bool options;
	private bool multiplayer;
	private bool startServerWin;
	private bool loadinglevelMenu;
	
	private Rect windowRect = new Rect(25, 25, 600, 100);
	private Rect windowRect2 = new Rect(25, 25, 600, 100);
	private Rect windowRect3 = new Rect(25, 25, 600, 100);
	private Rect windowRect4 = new Rect(25, 25, 600, 100);
	private Rect windowRect5 = new Rect(25, 25, 600, 100);
	//Gui Stuff end ------------------------------------------

	//multiplayer stuff start --------------------------------
	public string disconnectedLevel = "MainMenu";

	private string gameName = "A Virus";
	private bool refreshing = false;
	private HostData[] hostData;

	private string password = "nimda";
	private string level = "1";
	private string username = "Your Crappy Player Name";
	private string ip = "if you see this then there is a problem";
	private string port = "27050";
	private string playerCount = "8";

	private int players;
	private int portint;
	//multiplayer stuff end ----------------------------------
	
	void Awake()
	{
		main = true;
		AudioListener.volume = inGameVolume;
		Time.timeScale = 1f;
		ip = Network.player.ipAddress;
		HostData[] hostdata = MasterServer.PollHostList();
		DontDestroyOnLoad(this);
		GetComponent<NetworkView>().group = 1;
		Network.SetLevelPrefix(lastLevelPrefix);
		
		if (Network.HavePublicAddress()){
			Debug.Log("This machine has a public IP address");
		}else{
			Debug.Log("This machine has a private IP address");
		}
	}
	//----------------------------------------------------------------window Main
	void DoMyWindow(int windowID) 
	{
		GUILayout.BeginVertical();
		GUILayout.Label ("ShArksGiving");
		GUILayout.Label("                  ", "Divider");
		if (GUILayout.Button("Start Game")) 
		{
			options = false;
			main = false;
			multiplayer = true;
			startServerWin = false;
		}
		if (GUILayout.Button("Options")) 
		{
			options = true;
			main = false;
			multiplayer = false;
			startServerWin = false;
		}
		if (GUILayout.Button("Tutorial")) 
		{
			Application.LoadLevel("Test");
		}
		//if (GUILayout.Button("about Game")) 
		//{
			//Got to About scence
		//}
		if (GUILayout.Button("Exit Game")) 
		{
			Application.Quit();
		}
		GUILayout.Label("                  ", "Divider");
		GUILayout.EndVertical();
	}
	//----------------------------------------------------------------window options
	void DoMyWindow2(int windowID) 
	{
		string[] names = QualitySettings.names;
		
		GUILayout.BeginVertical();
		GUILayout.Label("                  ", "Divider");
		GUILayout.Label ("Select your qaulity setting");
		
        int i = 0;
        while (i < names.Length) {
            if (GUILayout.Button(names[i]))
                QualitySettings.SetQualityLevel(i, true);
            
            i++;
        }
		
		GUILayout.Label("                  ", "Divider");
		
			GUILayout.Label ("Select your Volume Level");
			inGameVolume = GUILayout.HorizontalSlider(inGameVolume, 0.0F, 10.0F);
			
		GUILayout.Label("                  ", "Divider");
		
		if (GUILayout.Button("Back To Main")) 
		{
			options = false;
			main = true;
			multiplayer = false;
			startServerWin = false;
		}
		
		GUILayout.Label("                  ", "Divider");
		GUILayout.EndVertical();
	}
	//----------------------------------------------------------------window Multiplayer Main
	void DoMyWindow3(int windowID) 
	{
		GUILayout.BeginVertical();
		GUILayout.Label ("Multiplayer Setup");
		GUILayout.Label("                  ", "Divider");
		GUILayout.Label ("Username");
		username = GUILayout.TextField(username, 32);
		GUILayout.Label("                  ", "Divider");

		if (GUILayout.Button("start a server")) 
		{
			options = false;
			main = false;
			multiplayer = false;
			startServerWin = true;

		}

		if (GUILayout.Button("Refresh Server list")) 
		{
			Debug.Log("Refresh Hosts");
			refreshHostList();
		}
		Debug.Log("Refresh");
		refreshHostList();

		if (GUILayout.Button("Back To Main")) 
		{
			options = false;
			main = true;
			multiplayer = false;
			startServerWin = false;
		}

		GUILayout.Label ("password (defult is nimda)");
		password = GUILayout.PasswordField(password, "*"[0], 32);

		GUILayout.Label ("Multiplayer Servers (may need refresh)");
		GUILayout.Label("                  ", "Divider");
		int i = 0;
		if(MasterServer.PollHostList().Length > i) { 
			foreach (HostData element in hostData){
				if(GUILayout.Button( element.connectedPlayers + " of " + element.playerLimit + " on level " + element.comment + "[" + element.ip[0] + "]")) 
				{ 
					string tmpIp = "";
					tmpIp = element.ip[i] + "";

					options = false;
					main = false;
					multiplayer = false;
					startServerWin = false;
					loadinglevelMenu = true;

					Connect(tmpIp, element.port);
				}
				i++;
			}
		}
		GUILayout.Label("                  ", "Divider");
		GUILayout.EndVertical();

	}

	//----------------------------------------------------------------window Multiplayer start server
	void DoMyWindow4(int windowID)
	{
		GUILayout.BeginVertical();
		GUILayout.Label ("Server Setup");
		GUILayout.Label("                  ", "Divider");

		GUILayout.Label ("ip address (should be prefilled out)");
		ip = GUILayout.TextField(ip, 32);

		GUILayout.Label ("port");
		port = GUILayout.TextField(port, 32);

		GUILayout.Label ("level (1) (2) (3)");
		level = GUILayout.TextField(level, 1);

		GUILayout.Label ("Your game's unique name");
		gameName = GUILayout.TextField(gameName, 32);

		GUILayout.Label ("Max Players (recommended 8)");
		playerCount = GUILayout.TextField(playerCount, 2);

		GUILayout.Label ("password (defult is nimda)");
		password = GUILayout.PasswordField(password, "*"[0], 32);

		GUILayout.Label("                  ", "Divider");

		if (GUILayout.Button("start server")) 
		{
			startServer();
		}
		
		if (GUILayout.Button("Close Server Setup")) 
		{
			options = false;
			main = false;
			multiplayer = true;
			startServerWin = false;
		}
		GUILayout.Label("                  ", "Divider");
		GUILayout.EndVertical();

	}

	void DoMyWindow5(int windowID)
	{
		GUILayout.BeginVertical();
		GUILayout.Label ("loading");
		GUILayout.EndVertical();
		
	}
	
	void MainMenu ()
	{
		windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "");
	}
	
	void Options ()
	{
		windowRect2 = GUILayout.Window(0, windowRect2, DoMyWindow2, "");	
	}
	void Multiplayer ()
	{
		windowRect3 = GUILayout.Window(0, windowRect3, DoMyWindow3, "");	
	}

	void ServerWindow ()
	{
		windowRect4 = GUILayout.Window(0, windowRect4, DoMyWindow4, "");	
	}

	void loadinglevel()
	{
		windowRect5 = GUILayout.Window(0, windowRect5, DoMyWindow5, "");	
	}

	//on connection to a server --------------------------------------------------------
	void OnConnectedToServer() {
		Network.RemoveRPCsInGroup(0);
		Network.RemoveRPCsInGroup(1);
		RpcLink.GetComponent<NetworkView>().RPC( "LoadLevel", RPCMode.AllBuffered, level,lastLevelPrefix+ 1);
		int tmpLevel;
		int.TryParse(level, out tmpLevel);
		Application.LoadLevel(tmpLevel.ToString());
		Debug.Log("Connected to server");
		onMainMenu = false;
	}
	
	//connect to a server --------------------------------------------------------
	void  Connect( string ip ,   int port  ){
		Network.Connect(ip, port, password);

	}
	//start server ----------------------------------------------------------------
	void startServer () {

		int.TryParse(playerCount, out players);
		int.TryParse(port, out portint);

		Network.incomingPassword = password;
		bool useNat = !Network.HavePublicAddress();
		Network.InitializeServer(players, portint, useNat);
		MasterServer.RegisterHost(gameName, "ShArksGiving", level);
		Debug.Log("started server");
		//Spawn server Manager Prefab ---------------------------------------

		onMainMenu = false;
		options = false;
		main = false;
		multiplayer = false;
		startServerWin = false;
		loadinglevelMenu = false;
		
	}
	//refresh host ----------------------------------------------------------------
	void refreshHostList () {
		
		MasterServer.RequestHostList(gameName);
		refreshing = true;
		
		
		
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		options = false;
		main = true;
		multiplayer = false;
		startServerWin = false;
		loadinglevelMenu = false;
	}
	
	void  OnDisconnectedFromServer (){
		Application.LoadLevel(disconnectedLevel);
		onMainMenu = true;
		options = false;
		main = true;
		multiplayer = false;
		startServerWin = false;
		loadinglevelMenu = false;
	}

	void  Update (){
		
		if(refreshing) {
			if(MasterServer.PollHostList().Length > 0) {           
				refreshing = false;
				Debug.Log(MasterServer.PollHostList().Length);
				hostData = MasterServer.PollHostList();
				
				
			}
			
			
		}
	}


	
	void OnGUI () 
	{
		AudioListener.volume = inGameVolume;
		GUI.skin = mySkin;

		if(Network.isServer)
		{
			bool first = true;
			if(first)
			{
				int tmpLevel;
				int.TryParse(level, out tmpLevel);
				Application.LoadLevel(tmpLevel);
				first = false;
			}
		}

		if(onMainMenu)
		{
			enabled = true;
		
			if(main)
			{
				MainMenu();
			}
			if(options)
			{
				Options();
			}
			if(multiplayer)
			{
				Multiplayer();
			}
			if(startServerWin)
			{
				ServerWindow();
			}
			if(loadinglevelMenu)
			{
				loadinglevel();
			}
		}
		else
		{

			enabled = false;

		}
	}
}
