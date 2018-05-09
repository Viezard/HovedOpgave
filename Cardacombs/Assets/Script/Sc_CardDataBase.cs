using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CardDataBase : MonoBehaviour {

	public SO_CardMelee[] MeleeCards;
	// Use this for initialization
	public SO_CardMelee FindMeleeCardByID(int id){
		for (int i = 0; i < MeleeCards.Length;i++){
			if (MeleeCards[i].id == id){
				return MeleeCards[i];
			}
		}
		return MeleeCards[0];
	}
}
