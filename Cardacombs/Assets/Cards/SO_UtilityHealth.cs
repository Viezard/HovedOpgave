using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New health Card", menuName = "utility/HealthCard")]
public class SO_UtilityHealth : CardUtility {

	public int maxHealth;
	public int minHealth;
	public int chance = 100; 

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.Healing(maxHealth, minHealth, chance);
	}

}

