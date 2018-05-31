using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_RatKing : MonsterClass {
	[TextArea]
	public string biteDescription;
	public string biteName;
	[TextArea]
	public string ratStormDescription;
	public string ratStormName;
	[TextArea]
	public string ratDefenceDescription;
	public string ratDefenceName;
	public override void MonsterTurn (){
		if (monsterStage == 0 && doStuff == true){
			doStuff = false;
			Bite();
			
		} else if (monsterStage == 1 && doStuff == true){
			doStuff = false;
			int random = Random.Range(0,2);
			if (random == 0){
				RatDefence();
			} else {
				RatStorm();
			}
	
		} else if (monsterStage == 2 && doStuff == true){
			if(battleManager.currentStage != 10){
				battleManager.currentStage = 0;
			}
			if (battleManager.currentStage != 10){
				battleManager.currentStage = 0;
			}
			monsterStage = 0;

		}
        
    }

	public void Bite() {
		PlayingCard(0, biteDescription, biteName);
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
		PlayingCard(1, ratStormDescription, ratStormName);
		damage = 1;
		int target = FindTarget ();
		battleManager.DamageCalc(target: target, damage: damage);
	}

	public void RatDefence(){
		PlayingCard(2, ratDefenceDescription, ratDefenceName);
		defence += 1;
		UpdateText();
	}
	public override void MonsterStart (){

	}

}
