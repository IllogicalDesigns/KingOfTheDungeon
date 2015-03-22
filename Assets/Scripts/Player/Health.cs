using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
		public int maxHealth = 100;
		public RespawnPoints reCache;
		public Treasure treasureCache;
		public int health = 0;
		SkeletonAi skeleAiChace;
		public GameObject deathRagdoll;
		public float countDown = 5;
		public GameObject[] playerDisableOnDeath;
		public int playerRespawnTime = 5;
		int maxPlayerRespawnTime;

		void Awake ()
		{
				health = maxHealth;
				maxPlayerRespawnTime = playerRespawnTime;
		}

		void ApplyDmg (int dmg)
		{
				// just making the dmg be applied
				health = health - dmg;
				// now time to play an injury sound and some acting
				//non for now as im not ready for that
		}

		void RemoveArrows ()
		{
				foreach (Transform child in transform) {
						if (child.CompareTag ("arrow"))
								Destroy (child.gameObject);				
				}
		}

		IEnumerator DeathCoroutine ()
		{
				Debug.Log (gameObject.name + " has died");
				if (gameObject.name == treasureCache.hasTreasure) { 
						treasureCache.hasTreasure = "none";
						treasureCache.MoveTreasure2Dead ();
				}
				Move2p moveCache = gameObject.GetComponent<Move2p> ();
				BoxCollider boxy = gameObject.GetComponent<BoxCollider> ();
				foreach (GameObject obj in playerDisableOnDeath) {
						obj.SetActive (false);
				}
				moveCache.enabled = false;
				boxy.enabled = false;
				RemoveArrows ();
				Vector3 v3Current = new Vector3 (0, 0, 0);
				v3Current = transform.eulerAngles;
				Quaternion qRotation = Quaternion.identity;
				qRotation.eulerAngles = v3Current;
				Instantiate (deathRagdoll, transform.position, qRotation);
				health = maxHealth;
				
				while (playerRespawnTime > 0) {
					yield return new WaitForSeconds(1);
					playerRespawnTime --;
					Debug.Log(gameObject.name + " time to respawn " + playerRespawnTime);
				}
				


				if (playerRespawnTime <= 0) {
						Transform rndSpwn = reCache.PlayerRandomSpawn ().transform;
						transform.position = rndSpwn.position;
						foreach (GameObject obj in playerDisableOnDeath) {
								obj.SetActive (true);
						}
						boxy.enabled = true;
						moveCache.enabled = true;
						moveCache.UpdateTreasure (false);
						playerRespawnTime = maxPlayerRespawnTime;
				}
		}

		void Death ()
		{
				//time to activate ragdollz or death anmin
				//for now we just remove the object as there is nothing set up for thi
				if (skeleAiChace == null)
						skeleAiChace = gameObject.GetComponent<SkeletonAi> ();
				skeleAiChace.AiRandomPatrolPoints ();
				Transform rndSpwn = reCache.AiRandomSpawn ().transform;
				Vector3 v3Current = new Vector3 (0, 0, 0);
				v3Current = transform.eulerAngles;
				Quaternion qRotation = Quaternion.identity;
				qRotation.eulerAngles = v3Current;
				Instantiate (deathRagdoll, transform.position, qRotation);
				transform.position = rndSpwn.position;
				RemoveArrows ();
				health = maxHealth;
		}
	
		void Update ()
		{
				if (health <= 0) {
						if (gameObject.tag == "Player")
								StartCoroutine ("DeathCoroutine");
						if (gameObject.tag != "Player")
								Death ();
				}
				if (health > maxHealth)
						health = maxHealth;
		}
}
