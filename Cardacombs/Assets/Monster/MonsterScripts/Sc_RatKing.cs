using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RatKing : MonsterClass {
	public override void MonsterTurn (){
		Bite();
		int random = Random.Range(0,2);
		if (random == 0){
			RatDefence();
		} else {
			RatStorm();
		}
		if(battleManager.currentStage != 10){
			battleManager.currentStage = 0;
		}
		
	}

	public void Bite() {
		battleManager.PrintLog("Rat King used Bite","red");
		damage = 2;
		poison = 1;
		for (int i = 0; i < debuffed; i++){
			if(damage > 0){
				damage -=1;
			} else if (0 < poison){
				poison -= 1;
			}
		}
		int target = FindTarget ();
		battleManager.DamageCalc(target: 1, damage: battleManager.currentSpiked);
		battleManager.DamageCalc(target: target, damage: damage, poison: poison);
	}
	public void RatStorm(){
		battleManager.PrintLog("Rat King used RatStorm","red");
		damage = 1;
		int target = FindTarget ();
		battleManager.DamageCalc(target: target, damage: damage);
	}

	public void RatDefence(){
		battleManager.PrintLog("Rat King used RatDefence","red");
		defence += 1;
		UpdateText();
	}
	public override void MonsterStart (){

	}

}
