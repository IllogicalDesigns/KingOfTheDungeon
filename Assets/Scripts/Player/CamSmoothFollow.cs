using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class CamSmoothFollow : MonoBehaviour {
	public Transform myPlayerTransform;
	public GameObject myCrossHair;
	public string mouseX = "mouseX";
	public string mouseY = "mouseY";
	private float smoothTime = 0F;
	public float sensetivity = 5;
	private Vector3 velocity = Vector3.zero;
	public float followHeight = 9;
	private Camera myCam;
	private float h = 0;
	private float v = 0;

	void Awake (){
			myCam = gameObject.GetComponent<Camera> ();
			myCrossHair.transform.position = transform.position;
	}

	public void DeactivateCursorRender () {
		myCam.cullingMask = ~(1 << 9);
		}

	public void ActivateMyCursor () {
		myCrossHair.GetComponent<Renderer>().enabled = true;
	}

	public Vector3 RotateToMouse ()
	{ 
		return  new Vector3 (myCrossHair.transform.position.x, myPlayerTransform.position.y, myCrossHair.transform.position.z);
	}

	void FixedUpdate () {
		float step = sensetivity * Time.deltaTime * h;
		float step2 = sensetivity * Time.deltaTime * v;
		Vector3 hVec3 = new Vector3 (1, transform.position.y, transform.position.z);
		Vector3 vVec3 = new Vector3 (transform.position.x, transform.position.y, 1);
		myCrossHair.transform.position = Vector3.MoveTowards (myCrossHair.transform.position, hVec3, step);
		myCrossHair.transform.position = Vector3.MoveTowards (myCrossHair.transform.position, vVec3, step2);
		myCrossHair.transform.position = new Vector3 (myCrossHair.transform.position.x, transform.position.y - 1f, myCrossHair.transform.position.z);
		}
	
	// Update is called once per frame
	void Update () {
		//Vector3 pos = myCam.WorldToViewportPoint (myCrossHair.transform.position);
		//pos.x = Mathf.Clamp01(pos.x);
		//pos.y = Mathf.Clamp01(pos.y);
		//myCrossHair.transform.position = Camera.main.ViewportToWorldPoint(pos);
		v = InputManager.GetAxis ("mouseY");
		h = InputManager.GetAxis ("mouseX");
		RotateToMouse ();
		Vector3 targetPoint = new Vector3 (myPlayerTransform.position.x, followHeight, myPlayerTransform.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, targetPoint, ref velocity, smoothTime);
	}
}
