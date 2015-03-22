using UnityEngine;
using System.Collections;

public class OnlinePanelController : MonoBehaviour {
	public GameObject finderGroup;
	public GameObject creatorGroup;
	public GameObject RunningServerPanel;

	// Use this for initialization
	void Start () {
		finderGroup.SetActive (true);
		creatorGroup.SetActive (false);
		RunningServerPanel.SetActive (false);
	
	}
	void OnServerInitialized () 
	{
		finderGroup.SetActive (false);
		creatorGroup.SetActive (false);
		RunningServerPanel.SetActive (true);
	}
	public void SwitchToFinderGroup()
	{
		finderGroup.SetActive (true);
		creatorGroup.SetActive (false);
		RunningServerPanel.SetActive (false);
	}
	public void SwitchToCreaterGroup()
	{
		finderGroup.SetActive (false);
		creatorGroup.SetActive (true);
		RunningServerPanel.SetActive (false);
	}
}
