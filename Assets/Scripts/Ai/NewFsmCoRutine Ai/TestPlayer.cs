using UnityEngine;
using System.Collections;

public class TestPlayer : MonoBehaviour {
	public enum controlTypes
	{
		Mobile,
		Gamepad,
		KeyboardMouse
	}
	public controlTypes myControlType;
	public LayerMask floorMask;                     // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	public float speed = 5.5f;						//our speed
	public float rotSpeed = 5f;						//our Rotation speed
	public int playerIntimidationValue = 10;		//10 is the defualt
	private float camRayLength = 100f;          	// The length of the ray from the camera into the scene.
	private float v;
	private float h;
	public Camera myCam;
	Vector3 targetPoint = Vector3.forward;
	Plane playerpane;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		v = Input.GetAxis ("MoveVerticalMenu");
		h = Input.GetAxis ("MoveHorizontalMenu");
		playerpane = new Plane(Vector3.up, transform.position);
	}
	void FixedUpdate () {
		if (myControlType == controlTypes.KeyboardMouse) {
			MovementHorizntalVertical ();
			TurningMouse ();
		}
	
	}
	void TurningMouse ()
	{
		Ray tempRay = myCam.ScreenPointToRay (Input.mousePosition);
		Quaternion targetRotation = transform.rotation;
		float hitdist = 0.0f;
		if(playerpane.Raycast(tempRay, out hitdist)){
			targetPoint = tempRay.GetPoint(hitdist);
			targetRotation = Quaternion.LookRotation(targetPoint, transform.position);
		}
		transform.LookAt (targetPoint);
		transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y,0f);

	}
	void MovementHorizntalVertical () {
		if(h != 0f)
			transform.Translate (Vector3.forward * speed * h * Time.deltaTime, Space.World);
		if(v != 0f)
			transform.Translate (Vector3.right * speed * -v * Time.deltaTime, Space.World);
	}
}
