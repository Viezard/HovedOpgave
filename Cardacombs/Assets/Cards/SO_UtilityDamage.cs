using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Utility Damage Card", menuName = "utility/DamageCard")]
public class SO_UtilityDamage : CardUtility {

	public int damage;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.DamageCalc(target: 1, damage: damage);
	}
}
