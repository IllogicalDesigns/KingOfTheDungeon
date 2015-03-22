using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {
	public int lifeTime = 180;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
}
