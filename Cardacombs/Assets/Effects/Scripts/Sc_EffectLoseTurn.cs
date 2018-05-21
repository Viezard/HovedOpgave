using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_EffectLoseTurn : Effect {
	public override bool StartEffect (){

		battleManager.HasLostTurn = true;
		return DecreaseDuration();
	} 
	public override void EndEffect (){
		print(placement);
		battleManager.currentEffects.RemoveAt(placement);
		for (int i = 0; i < battleManager.currentEffects.Count; i++){
			Effect effect =  battleManager.currentEffects[i].GetComponent<Effect>();
			if (effect.placement > placement){
				effect.placement -=1;
			}
		}
		Destroy(this.gameObject);
	}
}
