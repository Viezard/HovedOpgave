using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GelatinousCube : MonsterClass {
	[TextArea]
	public string consumeDescription;
	public string consumeName;
	[TextArea]
	public string slamDescription;
	public string slamName;
	public override void MonsterTurn (){
		if (monsterStage == 0 && doStuff == true){
			Consume();
			doStuff = false;
		} else if (monsterStage == 1 && doStuff == true){
			Slam();
			doStuff = false;
		} else if (monsterStage == 2 && doStuff == true){
			if (battleManager.currentStage != 10){
				battleManager.currentStage = 0;
				monsterStage = 0;
			}
		}
		
	}

	public void Consume() {
		PlayingCard(0, consumeDescription, consumeName);
		defence += battleManager.currentEquipmentArmor.Count;
		damage += battleManager.currentEquipmentMelee.Count;
		battleManager.PrintLog("Gelatinous Cube used Consume and it's attack became " + damage,"red");
	}
	public void Slam(){
		PlayingCard(1, slamDescription, slamName);
		battleManager.PrintLog("Gelatinous Cube used Slam","red");
		battleManager.DamageCalc(target: 1, damage: battleManager.currentSpiked);
		int target = FindTarget ();
		battleManager.DamageCalc(target: target, damage: damage);
	}


	public override void MonsterStart (){

	}

}
