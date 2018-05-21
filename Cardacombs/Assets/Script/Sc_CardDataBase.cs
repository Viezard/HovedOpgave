using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CardDataBase : MonoBehaviour {

	public SO_CardMelee[] meleeCards;
	public SO_CardArmor[] armorCards;
	public CardUtility[] utilityCards;
	public CardCurse[] curseCards;
	// Use this for initialization
	public SO_CardMelee FindMeleeCardByID(int id){
		for (int i = 0; i < meleeCards.Length;i++){
			if (meleeCards[i].id == id){
				return meleeCards[i];
			}
		}
		return meleeCards[0];
	}

	public SO_CardArmor FindArmorCardByID(int id){
		for (int i = 0; i < armorCards.Length;i++){
			if (armorCards[i].id == id){
				return armorCards[i];
			}
		}
		return armorCards[0];
	}
	public CardUtility FindUtilityCardByID(int id){
		for (int i = 0; i < utilityCards.Length;i++){
			if (utilityCards[i].id == id){
				return utilityCards[i];
			}
		}
		return utilityCards[0];
	}
	public CardCurse FindCurseCardByID(int id){
		for (int i = 0; i < curseCards.Length;i++){
			if (curseCards[i].id == id){
				return curseCards[i];
			}
		}
		return curseCards[0];
	}

}
