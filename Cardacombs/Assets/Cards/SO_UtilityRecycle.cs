using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Utility Recycle Card", menuName = "utility/RecycleCard")]
public class SO_UtilityRecycle : CardUtility {
	public int cardsToTake;
	public override void PlayedFunction (){
		Sc_BattleManager battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		battleManager.Recycle(cardsToTake);
	}
}
