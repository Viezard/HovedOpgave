using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_EffectLoseAP : Effect {
	public int APloss = 0;
	public override bool StartEffect (){
		battleManager.currentApUsed += APloss;
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
