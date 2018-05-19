using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Utility Debuff Card", menuName = "utility/DebuffCard")]
public class SO_UtilityDeBuff : CardUtility {

	public int debuff;
	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.Debuff(debuff);

	}
}
