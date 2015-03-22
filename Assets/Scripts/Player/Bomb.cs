using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
		public Light myPointLight;
		public GameObject bomb;
		public GameObject explosion;
		public GameObject bombDecal;
		public AudioSource fizzleSizzle;
		public AudioClip booooooooooooom;
		public float bombLifeTime = 3f;
		public float fxLifeTime = 15f;
		public float countVariable = 15f;
		public float explosionForce = 10f;
		SphereCollider mySphereCollider;
		float fuseRange = 3f;
		float fuseIntensity = 1f;
		float ExplodeRange = 5f;
		float ExplodeIntensity = 8f;
		float lightSmooth = 0.5f;

		// Use this for initialization
		void Start ()
		{
				myPointLight.range = fuseRange;
				myPointLight.intensity = fuseIntensity;
				countVariable = fxLifeTime;
				mySphereCollider = gameObject.GetComponent<SphereCollider> ();
				fizzleSizzle.volume = MainMenu.volume;
		}

		void MainFunction ()
		{
				countVariable -= Time.deltaTime;																			//the countdown
				if (!(countVariable > 0))																					//if zero delete
						Destroy (gameObject);																				//delete
				if (countVariable <= ((fxLifeTime - bombLifeTime) - 0.5f) && myPointLight.gameObject.activeInHierarchy) {	//if we have elapsed 0.5f past the explode
						myPointLight.intensity = Mathf.Lerp (myPointLight.intensity, 0f, lightSmooth * Time.deltaTime);		//then we will lerp our light intensity
				}
				if (myPointLight.intensity == 0)																			//disable light if we can't see it
						myPointLight.gameObject.SetActive (false);															//disable light
				if (countVariable <= (fxLifeTime - bombLifeTime) && bomb.activeInHierarchy) {								//if we have elapsed to explode time
						Quaternion qRotation = Quaternion.identity;															//set up a quaternion
						qRotation.eulerAngles = new Vector3 (90, 0, 0);														//set it to what we want out prefab to be
						Vector3 decalLocal = new Vector3 (bomb.transform.position.x, bomb.transform.position.y, bomb.transform.position.z);	//make a new location to spawn at
						Instantiate (bombDecal, decalLocal, qRotation);														//do the spawning of the decal
						Vector3 explodeLocal = new Vector3 (bomb.transform.position.x, bomb.transform.position.y + 1f, bomb.transform.position.z);
						Instantiate (explosion, bomb.transform.position, Quaternion.identity);								//make the boom
						fizzleSizzle.Stop ();																				//Make the FizzleSizzle Stop
						AudioSource.PlayClipAtPoint (booooooooooooom, explodeLocal, MainMenu.volume);							//Make It Sound Boom
						bomb.SetActive (false);																				//remove the bomb, it exploded
						myPointLight.range = ExplodeRange;																	//make the point light explode
						myPointLight.intensity = ExplodeIntensity;															//make the point light explode
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				myPointLight.transform.position = bomb.transform.position;
				MainFunction ();
				// && countVariable >= (fxLifeTime - (bombLifeTime - 1f))
				if (countVariable <= (fxLifeTime - bombLifeTime)) {
						if (countVariable >= (fxLifeTime - bombLifeTime - 0.05f)) {
								Vector3 explosionPos = bomb.transform.position;
								Collider[] colliders = Physics.OverlapSphere (explosionPos, mySphereCollider.radius);
								foreach (Collider hit in colliders) {
										if (hit.GetComponent<Collider>().gameObject.layer == 8) {
												Health tmpHealthCache = hit.GetComponent<Collider>().gameObject.GetComponent<Health> ();
												if (tmpHealthCache != null)
														tmpHealthCache.health = tmpHealthCache.health - 500;
										}
										if (hit && hit.GetComponent<Rigidbody>())
												hit.GetComponent<Rigidbody>().AddExplosionForce (explosionForce, explosionPos, mySphereCollider.radius, 2.0F);
				
								}
						}
				}
				
		}
}
