using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EventDataBase : MonoBehaviour {

	public Events[] events; 

	public Events FindEventByID(int id){
		for (int i = 0; i < events.Length;i++){
			if (events[i].eventID == id){
				return events[i];
			}
		}
		return events[0];
	}
}
