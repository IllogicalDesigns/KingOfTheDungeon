using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class CrossBowz : MonoBehaviour
{
		public string fireString = "FixMe";
		public Transform arrowFirePos;
		public AudioClip crossbowDryfire;				//our sound efx
		public GameObject arrowPrefab;
		public Joystick rightStick;
		public bool loaded = false;
		bool firstTouch = true;
		protected Animator crossBow;
		float count = 0;
		int touchCount = 0;
		Move2p moveCache;
	

		// Use this for initialization
		void Awake ()
		{
				moveCache = GetComponentInParent<Move2p> ();
				crossBow = GetComponent<Animator> ();
				Screen.lockCursor = true;
		}
		
		void LoadedTrue ()
		{
				//make it so we can fire now
				loaded = true;
		}

		void FireProjectile ()
		{
				Screen.lockCursor = true;
				Vector3 v3Current = new Vector3 (0, 0, 0);
				v3Current = transform.eulerAngles;
				Quaternion qRotation = Quaternion.identity;
				qRotation.eulerAngles = v3Current;
				//instantiate a projectile here
				AudioSource.PlayClipAtPoint (crossbowDryfire, arrowFirePos.position, MainMenu.volume);
				Instantiate (arrowPrefab, arrowFirePos.position, qRotation);
		}
	

		// Update is called once per frame
		void Update ()
		{
				if (moveCache.typeOfControls == "Mobile") {
						if (!loaded) {
								crossBow.SetTrigger ("Load");
						}
						touchCount = Input.touchCount;
						if (count < 0)
								count = 0;
						if (count > 0)
								count -= Time.deltaTime;
						if (rightStick.tapCount > 0 && count == 0 && firstTouch) {
								count = 0.2f;
								firstTouch = false;
						}
						if (rightStick.isFingerDown == false && count > 0) {
								count = 0;
								firstTouch = true;
								if (loaded) {
										crossBow.SetTrigger ("Fire");
										loaded = false;
								}
						} 
						if (!rightStick.isFingerDown) {
								firstTouch = true;
						}
				} else {

						//make sure that we are pressing a button and are loaded
						if (InputManager.GetButtonDown (fireString) && loaded) {
								crossBow.SetTrigger ("Fire");
								loaded = false;
						}
						//no reload button so we have to load for them
						if (!loaded) {
								crossBow.SetTrigger ("Load");
						}
				}
		}
}
