using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EventDataBase : MonoBehaviour {

	public Events[] tierOneEvents; 
	public Events[] tierTwoEvents; 
	public Events[] tierThreeEvents; 

	public Events FindEventByID(int id){
		for (int i = 0; i < tierOneEvents.Length;i++){
			if (tierOneEvents[i].eventID == id){
				return tierOneEvents[i];
			}
		}
		for (int i = 0; i < tierTwoEvents.Length;i++){
			if (tierTwoEvents[i].eventID == id){
				return tierTwoEvents[i];
			}
		}
		for (int i = 0; i < tierThreeEvents.Length;i++){
			if (tierThreeEvents[i].eventID == id){
				return tierThreeEvents[i];
			}
		}
		return tierOneEvents[0];
	}
}
