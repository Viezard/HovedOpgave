using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GelatinousCube : MonsterClass {
	public override void MonsterTurn (){
		Consume();
		Slam();

		battleManager.currentStage = 0;
	}

	public void Consume() {
		defence += battleManager.currentEquipmentArmor.Count;
		damage += battleManager.currentEquipmentMelee.Count;
		print("Gelatinous Cube used Consume and it's attack became " + damage);
	}
	public void Slam(){
		print("Gelatinous Cube used Slam");
		battleManager.DamageCalc(target: 1, damage: battleManager.currentSpiked);
		int target = FindTarget ();
		battleManager.DamageCalc(target: target, damage: damage);
	}


	public override void MonsterStart (){

	}

}
