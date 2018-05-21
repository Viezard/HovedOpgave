using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu (fileName = "New Utility Extra Attack Card", menuName = "utility/ExtraAttackCard")]
public class SO_UtilityDoubleAttack : CardUtility {

	public int extraAttacks;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.ExtraAttacks(extraAttacks);
	}
}

