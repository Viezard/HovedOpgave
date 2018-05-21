using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Blessing Event", menuName = "event/Blessing")]
public class SO_Blessings : Events {

	public int firstBonus = 0;
	public string firstBonusToo;
	public int secondBonus = 0;
	public string secondBonusToo;
	public int thirdBonus = 0;
	public string thirdBonusToo;
	
	public override void FirstEventFunction (){
		Sc_GameManager gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		if (firstBonusToo == "Defence"){
			gameManager.startingDefence += firstBonus;
		} else if (firstBonusToo == "UtilityAP"){
			gameManager.utilityAP += firstBonus;
		}
	}
	public override void SecondEventFunction (){
		Sc_GameManager gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		if (secondBonusToo == "Defence"){
			gameManager.startingDefence += secondBonus;
		} else if (secondBonusToo == "UtilityAP"){
			gameManager.utilityAP += secondBonus;
		}
	}
	public override void ThirdEventFunction (){
		Sc_GameManager gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		if (thirdBonusToo == "Defence"){
			gameManager.startingDefence += thirdBonus;
		} else if (thirdBonusToo == "UtilityAP"){
			gameManager.utilityAP += thirdBonus;
		}
	}
}
