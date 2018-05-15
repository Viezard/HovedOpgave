using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CardDataBase : MonoBehaviour {

	public SO_CardMelee[] MeleeCards;
	public SO_CardArmor[] ArmorCards;
	public CardUtility[] UtilityCards;
	// Use this for initialization
	public SO_CardMelee FindMeleeCardByID(int id){
		for (int i = 0; i < MeleeCards.Length;i++){
			if (MeleeCards[i].id == id){
				return MeleeCards[i];
			}
		}
		return MeleeCards[0];
	}

	public SO_CardArmor FindArmorCardByID(int id){
		for (int i = 0; i < ArmorCards.Length;i++){
			if (ArmorCards[i].id == id){
				return ArmorCards[i];
			}
		}
		return ArmorCards[0];
	}
	public CardUtility FindUtilityCardByID(int id){
		for (int i = 0; i < UtilityCards.Length;i++){
			if (UtilityCards[i].id == id){
				return UtilityCards[i];
			}
		}
		return UtilityCards[0];
	}

}
