﻿using System.Collections;
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
		print("Rat King used Bite");
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
		print("Rat King used RatStorm");
		damage = 1;
		int target = FindTarget ();
		battleManager.DamageCalc(target: target, damage: damage);
	}

	public void RatDefence(){
		print("Rat King used RatDefence");
		defence += 1;
		UpdateText();
	}
	public override void MonsterStart (){

	}

}
