using UnityEngine;
using System.Collections;

public class Rpc : MonoBehaviour {
	private int lastLevelPrefix= 0;

	void Awake (){
		DontDestroyOnLoad(this);
	}

	[RPC]
	public void LoadLevel(string level, int levelPrefix)
	{
		if (!GetComponent<NetworkView>().isMine)
		{
			Debug.Log("here");
			StartCoroutine(loadLevel(level, levelPrefix));
		}
	}
	
	private IEnumerator loadLevel (string level, int levelPrefix)
	{
		if (!GetComponent<NetworkView>().isMine)
		{
			lastLevelPrefix = levelPrefix;
			
			// There is no reason to send any more data over the network on the default channel,
			// because we are about to load the level, thus all those objects will get deleted anyway
			Network.SetSendingEnabled(0, false);	
			
			// We need to stop receiving because first the level must be loaded first.
			// Once the level is loaded, rpc's and other state update attached to objects in the level are allowed to fire
			Network.isMessageQueueRunning = false;
			
			// All network views loaded from a level will get a prefix into their NetworkViewID.
			// This will prevent old updates from clients leaking into a newly created scene.
			Network.SetLevelPrefix(levelPrefix);
			int tmpLevel;
			int.TryParse(level, out tmpLevel);
			Application.LoadLevel(tmpLevel);
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			
			// Allow receiving data again
			Network.isMessageQueueRunning = true;
			// Now the level has been loaded and we can start sending out data to clients
			Network.SetSendingEnabled(0, true);
			Debug.Log("loaded");
		}
		
	}
}
