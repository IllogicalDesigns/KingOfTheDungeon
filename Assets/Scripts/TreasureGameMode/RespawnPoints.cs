using UnityEngine;
using System.Collections;

public class RespawnPoints : MonoBehaviour {
	public GameObject[] playerRespawnTransforms;
	public GameObject[] aiRespawnTransforms;
	public GameObject[] treasureRespawnTransforms;
	public GameObject AiFolder;
	public bool debugMode = false;

	// Use this for initialization
	void Start () {
		playerRespawnTransforms = GameObject.FindGameObjectsWithTag("playerRespawnPoint");
		aiRespawnTransforms = GameObject.FindGameObjectsWithTag("AiRespawnPoint");
		treasureRespawnTransforms = GameObject.FindGameObjectsWithTag("TreasureRespawnPoint");
		if(!AiFolder.activeInHierarchy && AiFolder != null) AiFolder.SetActive (true);
	}

	public Transform PlayerRandomSpawn () {
		int rndNum = Random.Range (0, playerRespawnTransforms.Length);
		if(debugMode) Debug.Log ("i am repawning at " + playerRespawnTransforms[rndNum].name + " Spawn Point");
		return playerRespawnTransforms[rndNum].transform;
	}

	public Transform AiRandomSpawn () {
		int rndNum = Random.Range (0, aiRespawnTransforms.Length);
		if(debugMode) Debug.Log ("i am repawning at " + aiRespawnTransforms[rndNum].name + " Spawn Point");
		return aiRespawnTransforms[rndNum].transform;
	}

	public Transform TreasureRandomSpawn () {
		int rndNum = Random.Range (0, treasureRespawnTransforms.Length);
		if(debugMode) Debug.Log ("i am repawning at " + treasureRespawnTransforms[rndNum].name + " Spawn Point");
		return treasureRespawnTransforms[rndNum].transform;
	}
}
