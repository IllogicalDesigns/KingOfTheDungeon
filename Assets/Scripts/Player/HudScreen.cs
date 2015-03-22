using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TeamUtility.IO;

public class HudScreen : MonoBehaviour
{
		//Bombs
		public Text bombText;
		public int bombCount = 5;
		int maxBombs;
		//Mini Map
		public Image mapGraphic;
		public GameObject myPlayer;
		public float amountOfZoom = 0.25f;
		public Camera myCamera;
		public float calibrationX = 0.16f;
		public float calibrationY = 0.27f;
		public Canvas myCanvas;

		// Use this for initialization
		void Awake ()
		{
				bombText.text = bombCount.ToString ();
		}

		public void UpdateBombGui ()
		{
				bombCount --;
				if (bombCount != 0) {
						bombText.text = bombCount.ToString ();
						bombText.color = Color.white;
				} else if (bombCount <= 0) {
						bombCount = 0;
						bombText.color = Color.red;
						bombText.text = "X";
				}
		}
		
		void Update () 
		{
		//Vector3 mapPos = myCamera.transform.TransformVector(myPlayer.transform.position);
		Vector2 mapPos = myCamera.WorldToScreenPoint(myPlayer.transform.position);
		float test = Screen.currentResolution.width - (Screen.currentResolution.width * calibrationX);
		float test2 = Screen.currentResolution.height - (Screen.currentResolution.height * calibrationY);
		//mapGraphic.transform = RectTransformUtility.PixelAdjustRect(mapPos, mapGraphic.transform, myCanvas);
		//mapGraphic.transform.position = new Vector3 ((mapPos.x - test) ,(mapPos.y - test2) , mapGraphic.transform.position.z);
		}
}
