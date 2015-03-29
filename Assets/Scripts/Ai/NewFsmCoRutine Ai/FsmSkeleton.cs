using UnityEngine;
using System;
using System.Collections;

namespace Examples.FSM
{ 
	public class FsmSkeleton : EnumDelegateBehaviour
	{
		public enum m_AiStates
		{
			Patrol,				//search area to look for potential problems
			Combat,				//decide if it is possible to eliminate threats
			Searching,			//Something is fishy seek to destroy any threats	
			Survival			//my safety is more important then my job "high stress"
		}
		
		
		protected override Type InitializeStateType ()
		{
			return typeof (m_AiStates);
		}
		
		
		int m_RunCount = 0;
		
		
		void UpdatePatrolState ()
		{
			Debug.Log ("Patrol state.");
			CurrentState = (int)m_AiStates.Searching;
		}
	}
}

