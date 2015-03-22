using UnityEngine;
using System.Collections;

public class BasicAiTest : MonoBehaviour {
	public Transform target;
	NavMeshAgent navAgent;

	void Start () {
		navAgent = gameObject.GetComponent<NavMeshAgent>();
	}

	void Update () {
			navAgent.SetDestination(target.position);
	}
}
