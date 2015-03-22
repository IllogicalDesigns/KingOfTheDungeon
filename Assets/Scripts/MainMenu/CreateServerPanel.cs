using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateServerPanel : MonoBehaviour
{

		public Text serverName;
		public Image mapSprite;
		public Text mapName;
		public Text howManyPlayers;
		public Slider howManyPlayerSlider;
		public Text passwordField;
		public Toggle willUsePassToggle;
		public Text gameModeName;
		public Text confirmServerText;
		public GameObject confirmServerPanel;
		public Text connectedClients;
		public Sprite[] mapSprites;
		public string[] mapNames;
		public string[] gameModeNames;
		badWordFilter badWordLink;
		int thisManyPlayers;


		// Use this for initialization
		void Start ()
		{
				mapSprite.sprite = mapSprites [0];
				mapName.text = mapNames [0];
				gameModeName.text = gameModeNames [0];
				confirmServerPanel.SetActive (false);
				badWordLink = Camera.main.GetComponent<badWordFilter> ();
		}
		
		public void changePlayers ()
		{
				howManyPlayers.text = howManyPlayerSlider.value.ToString ();
		}
		
		public void openCloseCreateServer (bool open)
		{
				if (open) {
						confirmServerPanel.SetActive (false);
				} else {
						confirmServerPanel.SetActive (true);
				}
		}

		void OnServerInitialized ()
		{
				Network.Connect (Network.player.ipAddress, 25565, passwordField.text);
				Debug.Log ("(" + serverName.text + ") working and ready for connections.");
		}

		public void confirmServer ()
		{
				int.TryParse (howManyPlayers.text, out thisManyPlayers);
				if (willUsePassToggle.isOn)
						Network.incomingPassword = passwordField.text;
				bool useNat = !Network.HavePublicAddress ();
				Network.InitializeServer (thisManyPlayers, 25565, useNat);
				MasterServer.RegisterHost (serverName.text, "KingOfTheDungeon", mapName.text);
				Debug.Log ("Trying to start (" + serverName.text + ").");
		}

		public void CreateServer ()
		{
				serverName.text = serverName.text.ToLower ().ToString ();
				foreach (string badWord in badWordLink.profanity) {
						if (serverName.text.Contains (badWord)) {
								serverName.text = serverName.text.Replace (badWord, "Love");
						}
				}
				confirmServerText.text = "Creating (" + serverName.text.ToString () + ") On " +
						"(" + Network.player.ipAddress.ToString () + " : 25565) " +
						"With a max of (" + howManyPlayers.text.ToString () + ") players. " +
						"Playing (" + gameModeName.text.ToString () + ") on (" + mapName.text.ToString () + ")";
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Network.isServer) {
						connectedClients.text = "players Connected : " + Network.connections.ToString ();
				}
		}
}
