using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour {
	public int id;
	public new string name;
	public bool doesStak;
	public int duration;
	public int placement;
	public Sc_BattleManager battleManager;

	void Start(){
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		placement = battleManager.currentEffects.Count; 
		battleManager.currentEffects.Add(this.gameObject);
	}
	public abstract bool StartEffect ();
	public abstract void EndEffect ();
	public bool DecreaseDuration () {
		duration -= 1;
		if(duration <= 0){
			EndEffect();
			return true;
		}
		return false;
	}
}
