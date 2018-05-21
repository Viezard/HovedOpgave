using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Placeholder : MonsterClass {
	public GameObject preventType;
	public GameObject loseAP;
	public GameObject loseTurn;
	public GameObject doubleDamage;
	public override void MonsterTurn (){
		for (int i = 0; i < 2; i++){
			int random = Random.Range(0,10);
			if(random == 0){
				NullMagic ();
			} else if (random == 1){
				ParalyzingGaze();
			}else if (random == 2){
				PiercingMissile();
			}else if (random == 3){
				MindNumbingRay();
			}else if (random == 4){
				DisintegrationRay();
			}else if (random == 5){
				CharmingGaze();
			}else if (random == 6){
				SleepRay();
			}else if (random == 7){
				IntimidatingGaze();
			}else if (random == 8){
				CorrosionRay();
			}else if (random == 9){
				FrenzyRay();
			}
		}
		
		if(battleManager.currentStage != 10){
			battleManager.currentStage = 0;
		}
	}
	public void NullMagic (){
		print("Placeholder used NullMagic");
		GameObject newEffect = (GameObject)Instantiate (preventType, transform.position, transform.rotation);
		Sc_EffectPreventType neweffectScript =  newEffect.GetComponent<Sc_EffectPreventType>();
		neweffectScript.type = "Utility";
		neweffectScript.duration = 2;
	}
	public void ParalyzingGaze() {
		print("Placeholder used ParalyzingGaze");
		GameObject newEffect = (GameObject)Instantiate (loseAP, transform.position, transform.rotation);
		Sc_EffectLoseAP neweffectScript =  newEffect.GetComponent<Sc_EffectLoseAP>();
		neweffectScript.APloss = 2;
		neweffectScript.duration = 1;
	}
	public void PiercingMissile(){
		print("Placeholder used PiercingMissile");
		piercing = 2;
		battleManager.DamageCalc(target: 0, piercing: piercing);
	}
	public void MindNumbingRay(){
		print("Placeholder used MindNumbingRay");
		battleManager.DrawCards(discard: 2, isRandom: true, draw: 0, afterOrBefore: false);
	}
	public void DisintegrationRay(){
		print("Placeholder used DisintegrationRay");
		damage = 4;
		battleManager.DamageCalc(target: 0, damage: damage);
	}
	public void CharmingGaze(){
		print("Placeholder used CharmingGaze");
		if (battleManager.currentDeck.Count <= 0){
			if (battleManager.currentDiscard.Count <= 0){
				return;
			} else {
				for (int i = 0; i < battleManager.currentDiscard.Count; i++){
					battleManager.currentDeck.Add(battleManager.currentDiscard[i]);
				}
				battleManager.ShuffleDeck();
				battleManager.currentDiscard.Clear();
			}
		}
		int id = battleManager.currentDeck[0];
		Sc_CardDataBase cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		if (id < 1000){ // If the card is a melee card 
			SO_CardMelee card = cardDataBase.FindMeleeCardByID(id);
			print("Melee with the name " + card.name);
			damage = card.normalDamage;
			poison = card.poisonDamage;
			blunt = card.bluntDamage;
			piercing = card.piercingDamage;
			battleManager.DamageCalc(target: 0, damage: damage, poison: poison, blunt: blunt, piercing: piercing);
		} else if (id < 2000){
			SO_CardArmor card = cardDataBase.FindArmorCardByID(id);
			print("Armor with the name " + card.name);
			defence += card.armorBonus;
			UpdateText();
		} else if (id < 3000){
			print("A Utility");
		} else if (id < 4000){
		}
	}
	public void SleepRay(){
		print("Placeholder used SleepRay");
		GameObject newEffect = (GameObject)Instantiate (loseTurn, transform.position, transform.rotation);
		Sc_EffectLoseTurn neweffectScript =  newEffect.GetComponent<Sc_EffectLoseTurn>();
		neweffectScript.duration = 1;
	}
	public void IntimidatingGaze(){
		print("Placeholder used IntimidatingGaze");
		GameObject newEffect = (GameObject)Instantiate (preventType, transform.position, transform.rotation);
		Sc_EffectPreventType neweffectScript =  newEffect.GetComponent<Sc_EffectPreventType>();
		neweffectScript.type = "Melee";
		neweffectScript.duration = 1;
	}
	public void CorrosionRay() {
		print("Placeholder used CorrosionRay");
		blunt = 6;
		poison = 1;
		battleManager.DamageCalc(target: 0, blunt: blunt);
		battleManager.DamageCalc(target: 0, poison: poison);
	}
	public void FrenzyRay(){
		print("Placeholder used FrenzyRay");
		GameObject newEffect = (GameObject)Instantiate (preventType, transform.position, transform.rotation);
		Sc_EffectPreventType neweffectScript =  newEffect.GetComponent<Sc_EffectPreventType>();
		neweffectScript.type = "Armor";
		neweffectScript.duration = 2;

		GameObject newEffect2 = (GameObject)Instantiate (doubleDamage, transform.position, transform.rotation);
		Sc_EffectDoubleDamage neweffectScript2 =  newEffect2.GetComponent<Sc_EffectDoubleDamage>();
		neweffectScript2.extraAttaks = 1;
		neweffectScript2.duration = 1;
	}
	public override void MonsterStart (){

	}
}
