using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KeyBindEvents : MonoBehaviour
{
		public Text player1Fire;
		public Image player1FireTest;
		public Color defultColor;
		public bool rebinding = false;
		float coolDown = 0f;
		float coolDownTime = 0.25f;

		void RebindPlayer1Fire ()
		{
				player1FireTest.color = Color.red;
				coolDown = coolDownTime;
		}

		// Update is called once per frame
		void Update ()
		{
				if (coolDown < 0 && player1FireTest.color == Color.red)
						player1FireTest.color = defultColor;
				if (coolDown > 0)
						coolDown -= Time.deltaTime;
		}
}
