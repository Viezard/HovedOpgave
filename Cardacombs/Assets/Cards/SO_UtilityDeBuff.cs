using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Utility Debuff Card", menuName = "utility/DebuffCard")]
public class SO_UtilityDeBuff : CardUtility {

	public int decreaseDamage;
	public int chanceDecreaseDamage = 100;
	public bool cantHit = false;
	public int chanceCantHit = 100;
	public bool hitself = false;
	public int chancehitself = 100;
	
	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.Debuff(decreaseDamage, chanceDecreaseDamage, cantHit, chanceCantHit, hitself, chancehitself);

	}
}
