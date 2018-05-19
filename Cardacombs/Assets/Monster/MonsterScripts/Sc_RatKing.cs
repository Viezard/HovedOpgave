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

		battleManager.currentStage = 0;
	}

	public void Bite() {
		damage = 2;
		poison = 1;
		for (int i = 0; i < debuffed; i++){
			if(damage > 0){
				damage -=1;
			} else if (0 < poison){
				poison -= 1;
			}
		}
		battleManager.DamageCalc(target: 1, damage: battleManager.currentSpiked);
		battleManager.DamageCalc(target: 0, damage: damage, poison: poison);
	}
	public void RatStorm(){
		damage = 1;
		battleManager.DamageCalc(target: 0, damage: damage);
	}

	public void RatDefence(){
		defence += 1;
	}
	public override void MonsterStart (){

	}

}
