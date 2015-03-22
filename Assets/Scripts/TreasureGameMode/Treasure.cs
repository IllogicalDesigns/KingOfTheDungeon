using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour
{
		public Collider exitArea;
		public GameObject treasure;
		public string hasTreasure = "none";
		public RespawnPoints reCache;
		GameObject currentHolder;

		void Start ()
		{
				Transform rndSpwn = reCache.TreasureRandomSpawn().transform;
				transform.position = rndSpwn.position;
		}

		public void MoveTreasure2Dead ()
		{
								Debug.Log ("droping treasure at " + currentHolder.name + "'s location");
								transform.position = currentHolder.transform.position;
								hasTreasure = "none";
		}

		void OnTriggerStay (Collider other)
		{
				//checks to see if we are close enuff and we be human
				if (other.GetComponent<Collider>().tag == "Player") {
						currentHolder = other.gameObject;
						hasTreasure = currentHolder.name;
						Move2p moveCache = currentHolder.gameObject.GetComponent<Move2p> ();
						moveCache.UpdateTreasure (true);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if (hasTreasure != "none") {
						treasure.gameObject.SetActive (false);
				}
				else treasure.gameObject.SetActive (true);
		
		}
}
