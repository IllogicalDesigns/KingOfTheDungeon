using UnityEngine;
using System.Collections;

public class TestCamSmooth : MonoBehaviour {
	public Transform target;
	public float butteryness = 0.5f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, butteryness);
	}
}
