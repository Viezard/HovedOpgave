using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Permenent Buff Card", menuName = "utility/PermaBuffCard")]
public class SO_UtilityPermaBuff : CardUtility {
	public int toughness;
	public int spiked;

	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.Buffing(toughness, spiked);
	}

}
