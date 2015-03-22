using UnityEngine;
using System.Collections;

public class SkeletonAi : MonoBehaviour
{
		public GameObject target;						//are target that we put into the crosshairs
		public int layerMask = 1 << 8;					//the thing to mask lauers
		public bool debugMode = false;					//debug to see things
		public SphereCollider col;                    	// Reference to the sphere collider trigger component.
		NavMeshAgent navAgent;							//we need the nav agent
		Animator myAnimatior;							//we need the animator
		Vector3 rndPatrolA;								//This will rnd point a
		Vector3 rndPatrolB;								//This will rnd point b
		Vector3 rndPatrolC;								//This will rnd point c
		public float closeDistance = 5.0F;				//How far is close enough
		public bool playerInSight = false;				//can we see the player
		int currentPosNum = 0;							//this is just to figure out which point to go to 
		bool checkNewAiPatrolPoses = true;				//should we check
		public bool justSaw = false;					//did we just see the Ai
		float coolDwn = 0;								//Well we need to chill out right
		bool wanderFirstTime = true;					//have we wandered before?  have we just seen the player?
		public bool chaseDirFirstTime = true;			//Have we checked for the players dir of travel before?
		private Vector3 velocity;						//a reference to velocity
		public Transform lastKnownTransform;			//where was the player last?
		Vector3 oldPos;									//we need this for direction
		Vector3 debugHitVector;							//a debug vector for debuging
		Animator myAnimator;							//my animatior for animatior stuff
		float countDownToRandPoints = 5f;					//Start the countDown

		void Start ()
		{
				// Setting up the references
				col = gameObject.GetComponentInChildren<SphereCollider> ();
				navAgent = gameObject.GetComponent<NavMeshAgent> ();
				myAnimator = gameObject.GetComponentInChildren<Animator> ();
		}

		void OnDrawGizmos ()
		{
				if (debugMode) {
						Gizmos.color = Color.red;	//these are just to have some points show up for debuging
						if (rndPatrolA != null)
								Gizmos.DrawSphere (rndPatrolA, 0.5f);
						if (rndPatrolB != null)
								Gizmos.DrawSphere (rndPatrolB, 0.5f);
						if (rndPatrolC != null)
								Gizmos.DrawSphere (rndPatrolC, 0.5f);
						if (debugHitVector != null)
								Gizmos.DrawSphere (debugHitVector, 0.25f);
				}
		}

		float HowCloseAmI (Vector3 pointA, Vector3 pointB)			//this is what you have to do to make this work
		{
				Vector3 offset = pointA - pointB;					//This is our offset from the target
				float sqrLen = offset.sqrMagnitude;					//this does a love function i cant describe to you
				return sqrLen;										//this gives you the product of that sweet love function
		}

		bool AmIFacing (float angle1)//suggested 0.5f to see if you are within 180 degresses
		{
				if (target != null) {
						Vector3 dir = (target.transform.position - transform.position).normalized;
						float direction = Vector3.Dot (dir, transform.forward);
						if (direction > angle1) {
								return true;
						} else {
								return false;
						}
				} else {
						Debug.Log ("can't check if facing becasue no target");
						return false;
				}
		}

		bool AmIClose (Vector3 pos2Check)
		{
				Vector3 offset = pos2Check - transform.position;
				float sqrLen = offset.sqrMagnitude;
				if (sqrLen < closeDistance * closeDistance) {
						return true;
				} else {
						return false;
				}
		}

		Vector3 ChaseInDatDirection (Transform lastKnownTransform, float distance)
		{																											//Well this is a party
				if (lastKnownTransform != null) {
						chaseDirFirstTime = false;
						//Vector3 targetRelativeForward = lastKnownTransform.TransformDirection (Vector3.forward);	//point relatively forward
						Vector3 dirOfTravel = -(oldPos - target.transform.position).normalized; 
						Vector3 rayCastFrom = new Vector3 (lastKnownTransform.position.x, lastKnownTransform.position.y + 0.5f, lastKnownTransform.position.z);
						if (debugMode)
								Debug.DrawRay (rayCastFrom, dirOfTravel * distance, Color.red);						//draw ray to show WTF is happening
						RaycastHit hit;																				//make a hit variable
						if (Physics.Raycast (rayCastFrom, dirOfTravel, out hit, distance, layerMask)) {				//make a raycast check to see if we can move that way
								NavMeshHit navHit;																	//variable for sampling the nav mesh
								NavMesh.SamplePosition (hit.point, out navHit, 15f, 1);								//if we hit sample the nav mesh for close point
								if (debugMode)
										debugHitVector = navHit.position;											//debug if debug mode
								return navHit.position;																//return our navigation point to where this was called from
						} else {																					//if we don't hit we can move full speed
								debugHitVector = rayCastFrom;														//set up the debug vector
								debugHitVector += dirOfTravel * distance;											//move it in the relative direction for full speed
								NavMeshHit navHit;																	//set up a nav hit variable for sampling
								NavMesh.SamplePosition (debugHitVector, out navHit, 15f, 1);						//sampling comenceing
								if (debugMode)
										debugHitVector = navHit.position;											//well if we need to see shit we can
								return navHit.position;																//return our non-hit point so we can do our shit
						}																							//close the if-else
				} else {
						Debug.Log ("lastKnownTransform returned null when checked last");
						return Vector3.zero;
				}
		}																									//well that's all folkz!

		void WanderEndlesslyWithAnEnd ()
		{
				if (target != null) {
						if (!Physics.Linecast (transform.position, target.transform.position, layerMask)) {
								if (debugMode)
										Debug.DrawLine (transform.position, target.transform.position, Color.grey);
								float myClose = HowCloseAmI (transform.position, target.transform.position);
								if (myClose < 30 * 30 && AmIFacing (0.6f)) {
										if (target != null)
												oldPos = target.transform.position;
										OnPlayerStay ();
										chaseDirFirstTime = true;
										coolDwn = 10f;
								}
						}
				}
				
				if (wanderFirstTime) {				//if this is the first time give a search time limit
						coolDwn = 20;				//set the timeline
						wanderFirstTime = false;	//well this just makes this not execute again
						chaseDirFirstTime = true;
				}
				if (coolDwn <= 0 && justSaw) {		//if our time is up then we reset back to patrol
						AiRandomPatrolPoints ();	//go to patrol
						justSaw = false;			//well we haven't seen him in a while
				}
		}

		void Patrol ()
		{
				//Needs to wander then return to patrol after so long of a time of wandering
				if (!AmIClose (rndPatrolA) && currentPosNum == 0) {
						UpdateNavDestination (rndPatrolA);
				}
				if (AmIClose (rndPatrolA) && currentPosNum == 0) { 
						currentPosNum = 1;
						UpdateNavDestination (rndPatrolB);
				}
				if (AmIClose (rndPatrolB) && currentPosNum == 1) {
						currentPosNum = 2;
						UpdateNavDestination (rndPatrolC);
				}
				if (AmIClose (rndPatrolC) && currentPosNum == 2) { 
						currentPosNum = 0;
						UpdateNavDestination (rndPatrolA);
				}
		}

		void LookAtSmoothly (Vector3 tar2LookAt)
		{
				Quaternion rotateDirection = Quaternion.LookRotation (tar2LookAt - this.transform.position);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, rotateDirection, 250 * Time.deltaTime);
		}

		void Update ()
		{		
				if (countDownToRandPoints > 0)
						countDownToRandPoints -= Time.deltaTime;
				if (countDownToRandPoints == 1) {
						AiRandomPatrolPoints ();
						countDownToRandPoints = 0;
				}
				myAnimator.SetFloat ("Speed", navAgent.velocity.magnitude);
				float angle = FindAngle (transform.forward, navAgent.desiredVelocity, transform.up);
				myAnimator.SetFloat ("TurnSpeed", angle);
				if (justSaw && chaseDirFirstTime && coolDwn > 0) {
						navAgent.SetDestination (ChaseInDatDirection (lastKnownTransform, 30f));
						chaseDirFirstTime = false;
				}
				if (coolDwn > 0)
						coolDwn -= Time.deltaTime;
				if (playerInSight) {
						if (navAgent.velocity.x < 1f && navAgent.velocity.z < 1f && HowCloseAmI (transform.position, target.transform.position) < (30f * 30f))
								LookAtSmoothly (target.transform.position);
						justSaw = true;
						wanderFirstTime = true;
				}
				if (!playerInSight && !justSaw)
						Patrol ();
				if (!playerInSight && justSaw)
						WanderEndlesslyWithAnEnd ();
				if (rndPatrolA != null && rndPatrolB != null && checkNewAiPatrolPoses) {
						float dist = HowCloseAmI (rndPatrolA, rndPatrolB);
						float dist2 = HowCloseAmI (rndPatrolA, rndPatrolB);
						if (dist < (40 * 40) || dist > (50 * 50) && dist2 < (40 * 40) || dist2 > (50 * 50)) {
								AiRandomPatrolPoints ();
						} else {
								checkNewAiPatrolPoses = false;
						}
				}
		}

		float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
		{
				// If the vector the angle is being calculated to is 0...
				if (toVector == Vector3.zero)
			// ... the angle between them is 0.
						return 0f;
		
				// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
				float angle = Vector3.Angle (fromVector, toVector);
		
				// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
				Vector3 normal = Vector3.Cross (fromVector, toVector);
		
				// The dot product of the normal with the upVector will be positive if they point in the same direction.
				angle *= Mathf.Sign (Vector3.Dot (normal, upVector));
		
				// We need to convert the angle we've found from degrees to radians.
				angle *= Mathf.Deg2Rad;
		
				return angle;
		}

		public void AiRandomPatrolPoints ()
		{
				target = null;
				coolDwn = 0;
				checkNewAiPatrolPoses = true;
				currentPosNum = 0;
				rndPatrolA = new Vector3 (Random.Range (AiManager.levelSizeXStatic, 0), 0, Random.Range (AiManager.levelSizeYStatic, 0));
				NavMeshHit navPointA;
				NavMesh.SamplePosition (rndPatrolA, out navPointA, 40f, 1);
				rndPatrolA = navPointA.position;

				rndPatrolB = new Vector3 (Random.Range (AiManager.levelSizeXStatic, 0), 0, Random.Range (AiManager.levelSizeYStatic, 0));
				NavMeshHit navPointB;
				NavMesh.SamplePosition (rndPatrolB, out navPointB, 40f, 1);
				rndPatrolB = navPointB.position;

				rndPatrolC = new Vector3 (Random.Range (AiManager.levelSizeXStatic, 0), 0, Random.Range (AiManager.levelSizeYStatic, 0));
				NavMeshHit navPointC;
				NavMesh.SamplePosition (rndPatrolC, out navPointC, 40f, 1);
				rndPatrolC = navPointC.position;
		}

		void UpdateNavDestination (Vector3 posToGoTo)
		{
				navAgent.SetDestination (posToGoTo);
		}

		//void Update
		public void OnPlayerStay ()
		{
				navAgent.SetDestination (target.transform.position);
				if (target != null)
						lastKnownTransform = target.transform;
		}
}
