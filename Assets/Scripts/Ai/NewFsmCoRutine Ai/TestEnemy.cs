using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]
public class TestEnemy : MonoBehaviour
{
	public enum m_AiStates
	{
		Patrol,				//search area to look for potential problems
		Combat,				//decide if it is possible to eliminate threats
		Searching,			//Something is fishy seek to destroy any threats	
		Survival			//my safety is more important then my job "high stress"
	}
	public m_AiStates m_MyState;
	public LayerMask m_TargetMask;
	public float m_SightRange = 5.5f;
	public int m_CurrentHealth = 100;
	public float visionAngle = 0.75f;		//0.75f is a good range
	public float m_Health = 1.0f, m_Stress = 0;
	private SphereCollider m_sphereCol;
	private NavMeshAgent m_Agent;
	public bool m_DebugMode = true;
	Vector3 oldPos;									//we need this for direction
	Vector3 debugHitVector;							//a debug vector for debuging or maybe other stuff
	public GameObject target;						//are target that we put into the crosshairs
	public LayerMask layerMask;
	public bool debugMode = true;
	public Vector3 lastKnownTransform;				//where was the player last?
	public bool justSaw = false;

	void OnDrawGizmosSelected (){
		if (debugMode) {
			Gizmos.DrawWireSphere(lastKnownTransform, 0.3f);
			Gizmos.DrawFrustum(transform.position, visionAngle * 100f, m_SightRange, 0.1f, 1f);
					if (debugHitVector != null)
						Gizmos.DrawSphere (debugHitVector, 0.25f);
		}
	}
	void Start ()
	{
		m_MyState = m_AiStates.Patrol;
		m_sphereCol = gameObject.GetComponent<SphereCollider> ();
		m_sphereCol.radius = m_SightRange;
		m_Agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	//when someone enters our stress area
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player" || col.tag == "Enemy") {
			StressCalculation ();
			if (m_Stress > -100000 && col.tag == "Player" && Vector3.Dot ((col.transform.position - transform.position).normalized, transform.forward) > visionAngle) {
				m_Agent.SetDestination (col.transform.position);
				m_MyState = m_AiStates.Combat;
				target = col.gameObject;
				justSaw = true;
			}
		}

	}

	void OnTriggerStay (Collider col)
	{
		if (m_Stress > - 1000000) {
			if (col.tag == "Player" && Vector3.Dot ((col.transform.position - transform.position).normalized, transform.forward) > visionAngle) {
				m_Agent.SetDestination (col.transform.position);
				m_Agent.SetDestination (col.transform.position);
				m_MyState = m_AiStates.Combat;
				target = col.gameObject;
				justSaw = true;
			} else {
				if (target != null && justSaw) {
					lastKnownTransform = target.transform.position;
					m_Agent.SetDestination (ChaseInDatDirection (lastKnownTransform, 30f));
					Debug.Log("CHASEDINDATDIRECTION");
					justSaw = false;
				}
			}
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Player" || col.tag == "Enemy") {
			StressCalculation ();
			if (m_Stress > -100000 && col.tag == "Player" && Vector3.Dot ((col.transform.position - transform.position).normalized, transform.forward) > visionAngle) {
				m_Agent.SetDestination (col.transform.position);
				m_MyState = m_AiStates.Combat;
				target = col.gameObject;
				justSaw = true;
			}
		}
	}
	//Used to calculate how stressed out we currently our
	void StressCalculation ()
	{
		m_Stress = -10f;
		m_Stress = m_Stress + m_CurrentHealth;
		if (Input.GetKey (KeyCode.Space)) {
			Debug.Log (m_Stress);
		}
		Collider[] actorsNearMe = Physics.OverlapSphere (transform.position, m_sphereCol.radius, m_TargetMask.value);
		foreach (Collider m_col in actorsNearMe) {
			if (m_col.tag == "Player" && Vector3.Dot ((m_col.transform.position - transform.position).normalized, transform.forward) > visionAngle) {
				TestPlayer m_testPlay = m_col.gameObject.GetComponent<TestPlayer> ();
				m_Stress = m_Stress - m_testPlay.playerIntimidationValue;								//figure out the players worth to scare our Ai
				if (m_DebugMode)
					Debug.DrawLine (transform.position, m_col.transform.position, Color.red, 1f);
				//eventually have them clump up to protect each other
			}
			if (m_col.tag == "Enemy") {
				m_Stress = m_Stress + 10f;
				if (m_DebugMode)
					Debug.DrawLine (transform.position, m_col.transform.position, Color.green, 1f);
			}
		}
	}

	Vector3 ChaseInDatDirection (Vector3 lastKnownTransform, float distance)
	{																									//Well this is a party
		if (lastKnownTransform != null) {
			//Vector3 targetRelativeForward = lastKnownTransform.TransformDirection (Vector3.forward);	//point relatively forward
			Vector3 dirOfTravel = (target.transform.position - lastKnownTransform).normalized; 
			Vector3 rayCastFrom = new Vector3 (lastKnownTransform.x, lastKnownTransform.y + 0.5f, lastKnownTransform.z);
			if (debugMode)
				Debug.DrawRay (rayCastFrom, dirOfTravel * distance, Color.red, 1f);							//draw ray to show WTF is happening
			RaycastHit hit;																				//make a hit variable
			if (Physics.Raycast (rayCastFrom, dirOfTravel, out hit, distance, layerMask)) {				//make a raycast check to see if we can move that way
				NavMeshHit navHit;																		//variable for sampling the nav mesh
				NavMesh.SamplePosition (hit.point, out navHit, 15f, 1);									//if we hit sample the nav mesh for close point
				if (debugMode)
					debugHitVector = navHit.position;													//debug if debug mode
				return navHit.position;																	//return our navigation point to where this was called from
			} else {																					//if we don't hit we can move full speed
				debugHitVector = rayCastFrom;															//set up the debug vector
				debugHitVector += dirOfTravel * distance;												//move it in the relative direction for full speed
				NavMeshHit navHit;																		//set up a nav hit variable for sampling
				NavMesh.SamplePosition (debugHitVector, out navHit, 15f, 1);							//sampling comenceing
				if (debugMode)
					debugHitVector = navHit.position;													//well if we need to see shit we can
				return navHit.position;																	//return our non-hit point so we can do our shit
			}																							//close the if-else
		} else {
			Debug.Log ("lastKnownTransform returned null when checked last");
			return Vector3.zero;
		}
	}					
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Space)) {
			m_Agent.SetDestination (ChaseInDatDirection (lastKnownTransform, 30f));
		}
	}
}
