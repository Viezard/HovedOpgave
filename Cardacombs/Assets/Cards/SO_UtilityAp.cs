using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Utility Ap Card", menuName = "utility/APCard")]
public class SO_UtilityAp : CardUtility {

	public int apBonus;
	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.BonusAP(apBonus);
	}
}
