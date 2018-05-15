using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New health Card", menuName = "utility/HealthCard")]
public class SO_UtilityHealth : CardUtility {

	public int health;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.Healing(health);
	}

}

