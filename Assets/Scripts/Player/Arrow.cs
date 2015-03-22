using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
		public int speed = 10;
		public int damage = 50;
		public ParticleSystem bloodSpray;
		BoxCollider boxColl;

		// Use this for initialization
		void Awake ()
		{

				Vector3 v3Current = new Vector3 (0, 0, 0);
				v3Current = transform.eulerAngles;
				Quaternion qRotation = Quaternion.identity;
				qRotation.eulerAngles = new Vector3 (90, v3Current.y, v3Current.z);
				GetComponent<Rigidbody>().velocity = transform.TransformDirection (new Vector3 (0, speed, 0));
				boxColl = gameObject.GetComponent<BoxCollider> ();
				boxColl.enabled = false;
		}
		
		void OnCollisionEnter (Collision collision)
		{
			if (collision.collider.tag == "Untagged") {
						boxColl.enabled = true;
						Arrow arrow = gameObject.GetComponent<Arrow> ();
						Destroy (arrow, 5f);
						GetComponent<Rigidbody>().useGravity = true;//This is to make it flop around when it hitz
						Destroy (gameObject, 15f);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				RaycastHit hit;
				Vector3 fwd = transform.TransformDirection (Vector3.up);
				Debug.DrawRay (transform.position, fwd, Color.red);
				if (Physics.Raycast (transform.position, fwd, out hit, 0.5f)) {
						if (hit.collider.tag == "Evil" || hit.collider.tag == "Player") {
								gameObject.transform.parent = hit.collider.gameObject.transform;
								Health healthCache = hit.collider.gameObject.GetComponent<Health> ();
								if (healthCache != null)
										healthCache.gameObject.SendMessage ("ApplyDmg", damage);
								Destroy (GetComponent<Rigidbody>());
								BoxCollider box = gameObject.GetComponent<BoxCollider> ();
								Arrow arrow = gameObject.GetComponent<Arrow> ();
								bloodSpray.Play ();
								Destroy (arrow);
								Destroy (box);
						}else{
								boxColl.enabled = true;
								Arrow arrow = gameObject.GetComponent<Arrow> ();
								Destroy (arrow, 10f);
								GetComponent<Rigidbody>().useGravity = true;//This is to make it flop around when it hitz
								Destroy (gameObject, 15f);
						}
				}
		}
}
