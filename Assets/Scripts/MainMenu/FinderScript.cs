using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinderScript : MonoBehaviour
{
		private HostData[] hostData;
		public GameObject serverButton;
		public Text ipAddForce;
		public Text passForce;
		public Text PortForce;

		// Use this for initialization
		void Start ()
		{
				int i = 0;
				MasterServer.ClearHostList ();
				MasterServer.RequestHostList ("KingOfTheDungeon");
				hostData = MasterServer.PollHostList ();
				Debug.Log (hostData.Length.ToString () + " Servers Found");
				while (i < hostData.Length) {
						Debug.Log ("Game Name: " + hostData [i].gameName);
						i++;
				}
		}

		public void ForceConnect ()
		{
		int tmpPort = 0;
		int.TryParse (PortForce.text, out tmpPort);
		Network.Connect (ipAddForce.text.ToString(), tmpPort, passForce.text.ToString());
			Debug.Log ("Forcing A Connections To " + ipAddForce.text);
		}
	
		public void RefreshServerList ()
		{
				int i = 0;
				MasterServer.ClearHostList ();
				MasterServer.RequestHostList ("KingOfTheDungeon");
				hostData = MasterServer.PollHostList ();
				Debug.Log (hostData.Length.ToString () + " Servers Found");
				while (i < hostData.Length) {
						Debug.Log ("Game Name: " + hostData [i].gameName);
						i++;
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
