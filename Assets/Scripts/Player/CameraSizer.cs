using UnityEngine;
using System.Collections;

public class CameraSizer : MonoBehaviour {
	public Camera camP1;
	public Camera camP2;
	public Camera camP3;
	public Camera camP4;
	public RectTransform hudP1;
	public RectTransform hudP2;
	public RectTransform hudP3;
	public RectTransform hudP4;
	public float testH = 5;
	public float testW = 5;
	public bool debugMode = false;
	// Use this for initialization
	void Start () {
		if (MainMenu.howManyPlayers == 1) {
			camP1.rect = new Rect (0, 0, 1, 1);
			camP1.enabled = true;
			camP2.enabled = false;
			camP3.enabled = false;
			camP4.enabled = false;

			hudP1.gameObject.SetActive(true);
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -3.05175e-05f, Screen.currentResolution.width);
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -3.05175e-05f, Screen.currentResolution.height);
			hudP2.gameObject.SetActive(false);
			hudP3.gameObject.SetActive(false);
			hudP4.gameObject.SetActive(false);
		}
		
		if (MainMenu.howManyPlayers == 2) {
			camP1.rect = new Rect (0, 0.5f, 1, 0.5f);
			camP2.rect = new Rect (0, 0, 1, 0.5f);
			camP1.enabled = true;
			camP2.enabled = true;
			camP3.enabled = false;
			camP4.enabled = false;

			hudP1.gameObject.SetActive(true);
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -3.05175e-05f, Screen.currentResolution.width);
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -3.05175e-05f, (Screen.currentResolution.height / 2));
			hudP2.gameObject.SetActive(true);
			hudP2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -3.05175e-05f, Screen.currentResolution.width);
			hudP2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, -3.05175e-05f, (Screen.currentResolution.height / 2));
			hudP3.gameObject.SetActive(false);
			hudP4.gameObject.SetActive(false);
		}
		
		if (MainMenu.howManyPlayers == 3) {
			camP1.rect = new Rect (0, 0.5f, 0.5f, 0.5f);
			camP2.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
			camP3.rect = new Rect (0, 0, 1, 0.5f);
			camP1.enabled = true;
			camP2.enabled = true;
			camP3.enabled = true;
			camP4.enabled = false;

			hudP1.gameObject.SetActive(true);
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -3.05175e-05f, (Screen.currentResolution.width / 2));
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -3.05175e-05f, (Screen.currentResolution.height / 2));
			hudP2.gameObject.SetActive(true);
			hudP2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, -3.05175e-05f, (Screen.currentResolution.width / 2));
			hudP2.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -3.05175e-05f, (Screen.currentResolution.height / 2));
			hudP3.gameObject.SetActive(true);
			hudP3.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -3.05175e-05f, Screen.currentResolution.width);
			hudP3.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, -3.05175e-05f, (Screen.currentResolution.height / 2));
			hudP4.gameObject.SetActive(false);
		}
		
		if (MainMenu.howManyPlayers == 4) {
			camP1.rect = new Rect (0, 0.5f, 0.5f, 0.5f);
			camP2.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
			camP3.rect = new Rect (0, 0, 0.5f, 0.5f);
			camP4.rect = new Rect (0.5f, 0, 0.5f, 0.5f);
			camP1.enabled = true;
			camP2.enabled = true;
			camP3.enabled = true;
			camP4.enabled = true;

			hudP1.gameObject.SetActive(true);
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -3.05175e-05f, (Screen.currentResolution.width / 2));
			hudP1.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -3.05175e-05f, (Screen.currentResolution.height / 2));
			hudP2.gameObject.SetActive(true);
			hudP3.gameObject.SetActive(true);
			hudP4.gameObject.SetActive(true);
		}
	}

	void Update ()
	{
		if (debugMode) {
						hudP1.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, testW);
						hudP1.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, testH);
				}
	}
}
