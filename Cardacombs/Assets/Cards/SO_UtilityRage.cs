using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Rage Card", menuName = "utility/RageCard")]
public class SO_UtilityRage : CardUtility {

	public int rage;
	public int selfDamage = 0;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.currentRage += rage;
		if (selfDamage != 0){
			battleManager.DamageCalc(target: 0, damage:selfDamage);
		}
	}
}
