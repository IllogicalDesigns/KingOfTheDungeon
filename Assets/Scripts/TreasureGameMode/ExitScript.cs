using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour
{
		public Treasure treasureCache;
		protected Animator myAnimatior;
		public ParticleSystem happySpray;
		public AudioClip winningHorn;
		public PauseAndEnd pauseAndEndCache;
		public string nameOfWinner;

		void Start ()
		{
				myAnimatior = gameObject.GetComponent<Animator> ();
		}

		void OnPlayerWin () {
		Debug.Log ("Woot");

		}

		void OnEnterPlayerWin () {
				happySpray.Play ();
				AudioSource.PlayClipAtPoint (winningHorn, transform.position, MainMenu.volume);
				pauseAndEndCache.TheWinnerIs (nameOfWinner);
	}

		void OnTriggerStay (Collider other)
		{
				//checks to see if we are close enuff and we be human
				if (other.GetComponent<Collider>().tag == "Player") {
						if (other.GetComponent<Collider>().name == treasureCache.hasTreasure) {
								//play the exit animation
								Debug.Log ("someone has delivered the treasure");
								myAnimatior.SetTrigger ("Exit");
								nameOfWinner = other.GetComponent<Collider>().name;
						}
		}
	}
}

