using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Utility Draw Card", menuName = "utility/DrawCard")]
public class SO_UtilityDraw : CardUtility {
	public int draw;
	public int discard = 0;
	public bool isRandom = false; // does it just discard random card from your hand = false or do you get to pick = true
	public bool beforeOrAfter = false; // if false then you discard before you draw, if true you discard after
	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.DrawCards(draw, discard, isRandom, beforeOrAfter);
	}
}
