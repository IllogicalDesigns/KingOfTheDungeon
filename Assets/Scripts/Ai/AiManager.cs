using UnityEngine;
using System.Collections;

public class AiManager : MonoBehaviour {
	public static float levelSizeXStatic;
	public static float levelSizeYStatic;
	public float levelSizeX = 50;
	public float levelSizeY = 50;

	void OnDrawGizmos () {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (new Vector3 (0f, 0f, 0f), 1f);
		Gizmos.DrawSphere (new Vector3 (levelSizeX, 0f, 0f), 1f);
		Gizmos.DrawSphere (new Vector3 (0f, 0f, levelSizeY), 1f);
		Gizmos.DrawSphere (new Vector3 (levelSizeX, 0f, levelSizeY), 1f);
	}

	// Use this for initialization
	void Start () {
		levelSizeXStatic = levelSizeX;
		levelSizeYStatic = levelSizeY;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
