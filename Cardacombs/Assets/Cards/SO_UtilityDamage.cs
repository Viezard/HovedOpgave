using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Utility Damage Card", menuName = "utility/DamageCard")]
public class SO_UtilityDamage : CardUtility {

	public int damage;
	public int poison = 0;
	public int chanceHitSelf = 0;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		int random = Random.Range(0,100);
		if(chanceHitSelf <= random){
			battleManager.DamageCalc(target: 1, damage: damage, poison: poison);
		} else{
			battleManager.DamageCalc(target: 0, damage: damage, poison: poison);
		}
	}
}
