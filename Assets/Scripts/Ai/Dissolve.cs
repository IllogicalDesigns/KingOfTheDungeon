using UnityEngine;
using System.Collections;

public class Dissolve : MonoBehaviour {
	float dissolveamount = 0;
	public float dissolveTimeFactor = 15;
	public Material[] myMats;

	void Awake () {
		foreach(Material mat in myMats){
			mat.SetFloat ("_SliceAmount", 0f);
		}
	}
	

	// Update is called once per frame
	void Update () {
		dissolveamount += (Time.deltaTime / dissolveTimeFactor);
		foreach(Material mat in myMats){
			mat.SetFloat ("_SliceAmount", dissolveamount);
		}
	}
}
