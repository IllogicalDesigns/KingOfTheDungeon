using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public Transform target;
	public Transform targetTwo;
	
    private int speed = 15;
	private int speedTwo = 5;
	private float rotSpeed = 100;
	private int jumpForce = 250;
	private int count = 60;	
	private float DirectionDampTime = .5f;

	protected Animator shark;

	void Start()
	{
		shark = GetComponent<Animator>();
		Time.timeScale = 1;
	}
	
	
	 
    void FixedUpdate() {

		//if (networkView.isMine){

		float h;

			if(count <= 65)count++;


			if(count == 30)
			{
				shark.SetBool("Jump", false);
			}

//The mouse X Rotation Controller----------------------------------------------------------------------
		if(Input.GetAxis("MouseX") >= 1f)
			{
				// this will rotate the camera	
				float MRotate = Input.GetAxis("MouseX") * rotSpeed * Time.deltaTime; //input up and down
				h = Input.GetAxis("MouseX");
				shark.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);
				transform.Rotate(Vector3.up * MRotate); //output up and down
			}

		else if(Input.GetAxis("MouseX") <= 1f)
			{
			h = Input.GetAxis("MouseX");
			float MRotate = Input.GetAxis("MouseX") * rotSpeed * Time.deltaTime; //input up and down
			shark.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);
			transform.Rotate(Vector3.up * MRotate); //output up and down
			}
		else
			{
			h = 0;
			shark.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);
			}

//The mouse Y Rotation Controller----------------------------------------------------------------------
		if(Input.GetAxis("MouseY") >= 0.1f)
		{
			// this will rotate the camera	
			float MRotate2 = Input.GetAxis("MouseY") * rotSpeed * Time.deltaTime; //input up and down
			//transform.Rotate(Vector3.left * MRotate2); //output up and down
		}
		
		else if(Input.GetAxis("MouseY") <= 0.1f)
		{
			float MRotate2 = Input.GetAxis("MouseY") * rotSpeed * Time.deltaTime; //input up and down
			//transform.Rotate(Vector3.left * MRotate2); //output up and down
		}
		
		//The Jump force Controller----------------------------------------------------------------------------
			//if Jump is pressed add force upwards
		if(Input.GetButtonDown("Jump") && count >= 50)
			{
				GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
				GetComponent<Rigidbody>().AddForce(-transform.forward * jumpForce);
				shark.SetBool("Jump", true);
				count = 0;
			}

//The Forward Movement Controller----------------------------------------------------------------------
		if(!Input.GetButton("Forward") && !Input.GetButton("Backward"))
			{
				shark.SetFloat("Speed", 0);
			}
			
			//if Forward is pressed move Forward
		if(Input.GetButton("Forward"))
			{
			if(count >= 50){
				shark.SetFloat("Speed", 1);	
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}
			else{
				shark.SetFloat("Speed", 0.5f);	
				float step = (speed / 2) * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			}
		}

//The Backward Movement Controller---------------------------------------------------------------------
		//if Backward is pressed move Backward
		if(Input.GetButton("Backward"))
			{
				if(count >= 50){
					shark.SetFloat("Speed", -1);	
		       		float step = -speed * Time.deltaTime;
		        	transform.position = Vector3.MoveTowards(transform.position, target.position, step);
				}
			else{
					shark.SetFloat("Speed", -0.5f);	
					float step = (-speed / 2) * Time.deltaTime;
					transform.position = Vector3.MoveTowards(transform.position, target.position, step);
				}
		}

//The Leftward Movement Controller---------------------------------------------------------------------		
		//if Left is pressed move Left
		if(Input.GetButton("Left") && count >= 50)
			{
			if(count >= 50){
				//needs to smoothly move torwards target
	       		float step = speedTwo * Time.deltaTime;
	        	transform.position = Vector3.MoveTowards(transform.position, targetTwo.position, step);
			}
		else{	
			float step = (speedTwo / 2) * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetTwo.position, step);
		}
		}

//The Rightward Movement Controller---------------------------------------------------------------------
		//if Right is pressed move Right
		if(Input.GetButton("Right") && count >= 50)
			{
			if(count >= 50){
				//needs to smoothly move torwards target
	       		float step = -speedTwo * Time.deltaTime;
	        	transform.position = Vector3.MoveTowards(transform.position, targetTwo.position, step);
			}
			else{
				float step = (-speedTwo / 2) * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, targetTwo.position, step);
			}
		}

		//}

    }

}
