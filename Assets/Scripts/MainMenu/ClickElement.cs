using UnityEngine;
using System.Collections;

public class ClickElement : MonoBehaviour {
	public TextMesh me;
	public GameObject messageTarget;
	public AudioClip click;
	public string messageName;
	public string messageValue;
	public Color hover;
	public Color mouseClick;
	public Color none;
	
	void Awake(){
		me.GetComponent<Renderer>().material.color = none;
	}
	
	void OnMouseEnter(){
		//Debug.Log("over " + me.name);
		AudioSource.PlayClipAtPoint(click, transform.position, MainMenu.volume / 2);
		me.GetComponent<Renderer>().material.color = hover;
	}
	
	void OnMouseExit(){
		//Debug.Log("left " + me.name);
		me.GetComponent<Renderer>().material.color = none;
	}
	
	void OnMouseUp(){
		//Debug.Log("clicked on " + me.name);
		AudioSource.PlayClipAtPoint(click, transform.position, MainMenu.volume / 2);
		me.GetComponent<Renderer>().material.color = mouseClick;
		SendMessage();
	}
	
	void SendMessage(){
		if(messageValue != ""){
			SendMessageUpwards(messageName, messageValue);
			Debug.Log(me.name + " sent " + messageTarget.name + " ''" + messageName + " : " + messageValue + "''");
		}
		else{
			SendMessageUpwards( messageName);
			Debug.Log(me.name + " sent " + messageTarget.name + " ''" + messageName + "''");
		}
	}
}