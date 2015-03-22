using UnityEngine;
using System.Collections;

public class SkeleCollider : MonoBehaviour
{
		SkeletonAi skelAiCache;
		public Transform gunEndPos; 					//we launch the arrows from here
		public GameObject arrowPrefab;					//dont want to be on the other end of this
		public GameObject target;						//are target to kill
		public AudioClip crossbowDryfire;				//our sound efx
		public bool debugMode = false;					//debug to see things
		public int layerMask = 1 << 8;					//the thing to mask lauers
		public float shootingAngleArea1 = 0.8f;			//what is the radius of aim inacuracy
		bool playerFaceAndSight = false;				//are we facing someting
		public NavMeshAgent navAgent;
		public float maxCoolDown = 0.75f;
		float coolDown = 0;
		Color inShootRange = Color.red;
		Color notInShootRange = Color.white;
		Color myColor;
		Health healthCache = null;



		// Use this for initialization
		void Start ()
		{
				skelAiCache = gameObject.GetComponentInParent<SkeletonAi> ();
				myColor = notInShootRange;
		}

		void OnPlayerStay ()
		{
				navAgent.SetDestination (target.transform.position);
		}

		void FireArrow ()
		{
				Vector3 v3Current = new Vector3 (0, 0, 0);
				v3Current = transform.eulerAngles;
				Quaternion qRotation = Quaternion.identity;
				qRotation.eulerAngles = v3Current;
				coolDown = maxCoolDown;
				Instantiate (arrowPrefab, gunEndPos.position, qRotation);
				AudioSource.PlayClipAtPoint (crossbowDryfire, gunEndPos.position, MainMenu.volume);
		}

		void Update ()
		{

				if (healthCache != null) {
						if (healthCache.health <= 0f) {
								target = null;
								skelAiCache.AiRandomPatrolPoints ();
						}
				}
				if (healthCache == null && target != null) {
						healthCache = target.gameObject.GetComponentInParent<Health> ();
				}
				if (coolDown > 0) //if there is something to cool off we do it
						coolDown -= Time.deltaTime;
				
				if (playerFaceAndSight && coolDown <= 0) { //if we are facing and we are cooled off we fire
						FireArrow ();
				}
		}

		void OnTriggerStay (Collider other)
		{
				skelAiCache.playerInSight = false;
				//checks to see if we are close enuff and we be human
				if (other.GetComponent<Collider>().tag == "Player") {
						skelAiCache.target = other.gameObject;
						target = other.gameObject;
						skelAiCache.playerInSight = false;
						if (!Physics.Linecast (transform.position, other.transform.position, layerMask)) {
								if (debugMode)
										Debug.DrawLine (transform.position, other.transform.position);
								Vector3 dir = (other.transform.position - transform.position).normalized;
								float direction = Vector3.Dot (dir, transform.forward);
								if (direction > shootingAngleArea1) {
										playerFaceAndSight = true;
										myColor = inShootRange;
								} else
										playerFaceAndSight = false;
								myColor = notInShootRange;
								target = other.gameObject;
								skelAiCache.playerInSight = true;
								OnPlayerStay ();
						} else {
								playerFaceAndSight = false;
								if (debugMode)
										Debug.Log ("stuff in way");
						}
				}
		}

		void OnTriggerExit (Collider other)
		{
				// If the player leaves the trigger zone...
				if (other.gameObject.tag == "Player")
			// ... the player is not in sight.
						skelAiCache.playerInSight = false;
				playerFaceAndSight = false;

		}
}
