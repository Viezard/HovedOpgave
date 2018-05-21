using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Bomp Card", menuName = "Curse/Bomb")]
public class SO_CurseBomb : CardCurse {
	public int selfDamage;
	public int monsterDamage;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.DamageCalc(target: 1, damage: monsterDamage);
		battleManager.DamageCalc(target: 0, damage: selfDamage);		
	}

}
