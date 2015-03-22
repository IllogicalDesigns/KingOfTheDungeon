using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class Move2p : MonoBehaviour
{
		public float speed = 5;
		public string horName = "HorizontalP2";
		public string verName = "VerticalP2";
		public string rotX = "RightStickVertP2";
		public string rotY = "RightStickHoriP2";
		public string typeOfControls = "Mouse&Keyboard"; //Mouse&Keyboard //GamePad //Mobile
		public GameObject treasureOnYourBack;
		public GameObject bomb;
		private float rotSpeed = 300;
		float h = 0;
		float v = 0;
		public GameObject dualSticks;
		public Joystick leftStick;
		public Joystick rightStick;
		public Camera myCamera;
		public HudScreen myHud;
		CamSmoothFollow camSmoothCache;
		Health myHeathLink;
	
		void Start ()
		{
				camSmoothCache = myCamera.GetComponent<CamSmoothFollow> ();
				myHeathLink = gameObject.GetComponent<Health> ();
				if (typeOfControls == "Mouse&Keyboard") {
						camSmoothCache.ActivateMyCursor ();
				} else {
						camSmoothCache.DeactivateCursorRender ();
				}
				if (typeOfControls == "Mobile" && dualSticks != null) {
						if (dualSticks != null)
								dualSticks.gameObject.SetActive (true);
				} else {
						if (dualSticks != null)
								dualSticks.gameObject.SetActive (false);
				}
				if (MainMenu.howManyPlayers == 1) {
						if (gameObject.name == "Player2")
								gameObject.SetActive (false);
						if (gameObject.name == "Player3")
								gameObject.SetActive (false);
						if (gameObject.name == "Player4")
								gameObject.SetActive (false);
				}
				if (MainMenu.howManyPlayers == 2) {
						if (gameObject.name == "Player3")
								gameObject.SetActive (false);
						if (gameObject.name == "Player4")
								gameObject.SetActive (false);
				}
				if (MainMenu.howManyPlayers == 3) {
						if (gameObject.name == "Player4")
								gameObject.SetActive (false);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				h = InputManager.GetAxis (horName);

				v = InputManager.GetAxis (verName);

				if (myHeathLink.health <= 0) {
						myHud.UpdateBombGui ();
				}
		}

		public void UpdateTreasure (bool enabled)
		{
				if (enabled) {
						treasureOnYourBack.gameObject.SetActive (true);
				} else
						treasureOnYourBack.gameObject.SetActive (false);
		}

		public void DropBomb ()
		{
				if (!(myHeathLink.health < 0) && !(myHud.bombCount <= 0)) {
						Vector3 bombDrop = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
						Instantiate (bomb, bombDrop, Quaternion.identity);
						myHud.UpdateBombGui ();
				}
		}

		void FixedUpdate ()
		{
				if (typeOfControls == "Mouse&Keyboard") {
						//if (Input.GetKeyDown (KeyCode.Space)) {
								//DropBomb ();
						//}

						transform.LookAt (camSmoothCache.RotateToMouse ());
						float step = speed * Time.deltaTime * h;
						Vector3 hVec3 = new Vector3 (1, transform.position.y, transform.position.z);
						Vector3 vVec3 = new Vector3 (transform.position.x, transform.position.y, 1);
						transform.position = Vector3.MoveTowards (transform.position, hVec3, step);
						float step2 = speed * Time.deltaTime * v;
						transform.position = Vector3.MoveTowards (transform.position, vVec3, step2);
				}
				if (typeOfControls == "GamePad") {
						float step = speed * Time.deltaTime * h;
						Vector3 hVec3 = new Vector3 (1, transform.position.y, transform.position.z);
						Vector3 vVec3 = new Vector3 (transform.position.x, transform.position.y, 1);
						transform.position = Vector3.MoveTowards (transform.position, hVec3, step);
						float step2 = speed * Time.deltaTime * -v;
						transform.position = Vector3.MoveTowards (transform.position, vVec3, step2);
						// We are going to read the input every frame
						Vector3 vNewInput = new Vector3 (InputManager.GetAxis (rotX), InputManager.GetAxis (rotY), 0.0f);
						// Only do work if meaningful
						if (vNewInput.sqrMagnitude < 0.1f) {
								return;
						}
						// Apply the transform to the object  
						float angle = Mathf.Atan2 (InputManager.GetAxis (rotX) * -1f, InputManager.GetAxis (rotY)) * Mathf.Rad2Deg;
						transform.rotation = Quaternion.Euler (0, angle, 0);
				}
				if (typeOfControls == "Mobile") {
						//moveTouchPad.position.x
						float step = speed * Time.deltaTime * (leftStick.position.x * -1);
						Vector3 hVec3 = new Vector3 (1, transform.position.y, transform.position.z);
						Vector3 vVec3 = new Vector3 (transform.position.x, transform.position.y, 1);
						transform.position = Vector3.MoveTowards (transform.position, hVec3, step);
						float step2 = speed * Time.deltaTime * (leftStick.position.y * -1);
						transform.position = Vector3.MoveTowards (transform.position, vVec3, step2);
						// We are going to read the input every frame
						Vector3 vNewInput = new Vector3 (rightStick.position.x, rightStick.position.y, 0.0f);
						// Only do work if meaningful
						if (vNewInput.sqrMagnitude < 0.1f) {
								return;
						}
						// Apply the transform to the object  
						float angle = Mathf.Atan2 (rightStick.position.x, rightStick.position.y) * Mathf.Rad2Deg;
						transform.rotation = Quaternion.Euler (0, angle, 0);
				}
		}
}
