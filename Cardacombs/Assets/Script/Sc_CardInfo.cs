using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_CardInfo : MonoBehaviour {
	public int id;
	// the primary attribute such as normal damage or defence bonus 
	private bool hasPrimaryAttribute = false;
	private Text primaryAttributeTitle;
	private Text primaryAttributeDescription;
	// The secondary attribute if it has poison, spike or simular
	private bool hasFirstSecondaryAttribute = false;
	private Text firstSecondaryAttributeTitle;
	private Text firstSecondaryAttributeDescription;
	// If it has a second secdary attribute such as a weapon dealing both poisoned and piercing
	private bool hasSecondSecondaryAttribute = false;
	private Text secondSecondaryAttributeTitle;
	private Text secondSecondaryAttributeDescription;
	// If the card has a cost (a cost of 0 is still a cost)
	private bool hasCost = false;
	private Text costTitle;
	private Text costDescription;
	// the type of card Melee, Armor, Utility, Curse or Monster
	private Text typeTitle;
	private Text typeDescription;
	// If there are any special rules for the card 
	private bool hasSpecial = false;
	private Text specialTitle;
	private Text specialDescription;
	private GameObject background;	
	Image backgroundSR;

	private Sc_CardDataBase cardDataBase;

	// Use this for initialization
	void Start () {
		GetTexts();
		
		FindWhatItHas();
		RemoveFalse();
	}
	public void GetTexts(){
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		// Primary Attribute
		primaryAttributeTitle = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Text>();
		primaryAttributeDescription = this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Text>();
		// First Secondary Attribute
		firstSecondaryAttributeTitle = this.gameObject.transform.GetChild(1).GetChild(1).GetComponent<UnityEngine.UI.Text>();
		firstSecondaryAttributeDescription = this.gameObject.transform.GetChild(1).GetChild(2).GetComponent<UnityEngine.UI.Text>();
		// Second Secondary Attribute
		secondSecondaryAttributeTitle = this.gameObject.transform.GetChild(2).GetChild(1).GetComponent<UnityEngine.UI.Text>();
		secondSecondaryAttributeDescription = this.gameObject.transform.GetChild(2).GetChild(2).GetComponent<UnityEngine.UI.Text>();
		// Title
		typeTitle = this.gameObject.transform.GetChild(3).GetChild(1).GetComponent<UnityEngine.UI.Text>();
		typeDescription = this.gameObject.transform.GetChild(3).GetChild(2).GetComponent<UnityEngine.UI.Text>();
		// Cost
		costTitle = this.gameObject.transform.GetChild(4).GetChild(1).GetComponent<UnityEngine.UI.Text>();
		costDescription = this.gameObject.transform.GetChild(4).GetChild(2).GetComponent<UnityEngine.UI.Text>();
		// Special
		specialTitle = this.gameObject.transform.GetChild(5).GetChild(1).GetComponent<UnityEngine.UI.Text>();
		specialDescription = this.gameObject.transform.GetChild(5).GetChild(2).GetComponent<UnityEngine.UI.Text>();

		background = this.gameObject.transform.GetChild(6).gameObject;
		backgroundSR = background.GetComponent< Image>();
	}
	public void GetCost () {
		hasCost = true;
		costTitle.text = "COST" ;
		costDescription.text = "This is the amount of AP, the card cost to play";
	}
	public void FindWhatItHas (){
		if (id < 1000){
			SetDamage();
			SetType("Melee");
			GetCost();
		} else if (id < 2000){
			SetDefence();
			SetType("Armor");
			GetCost();
		} else if (id < 3000){
			SetType("Utility");
			GetCost();
			SetUtility();
		} else if (id < 4000){
			SetType("Curse");
			SetCurse();
		}
	}
	public void SetDamage(){
		hasPrimaryAttribute = true;
		SO_CardMelee card = cardDataBase.FindMeleeCardByID(id);
		backgroundSR.sprite = card.image;
		primaryAttributeTitle.text = "NORMAL DAMAGE";
		primaryAttributeDescription.text = "Normal damage deals it's damage first to defece and then HP";
		// Check if the weapon deals poison damage
		if (card.poisonDamage > 0){
			firstSecondaryAttributeTitle.text = "POISON";
			firstSecondaryAttributeDescription.text ="Poison damage, is only dealt, if the target has no armor before the attack";
			hasFirstSecondaryAttribute = true;
		}
		// Check if the weapon deals Blunt damage
		if (card.bluntDamage > 0){
			if (hasFirstSecondaryAttribute){
				secondSecondaryAttributeTitle.text = "BLUNT";
				secondSecondaryAttributeDescription.text ="Blunt damage is only able to damage defence, but it is dealt before the normal damage is";
				hasSecondSecondaryAttribute = true;
			}
			firstSecondaryAttributeTitle.text = "BLUNT";
			firstSecondaryAttributeDescription.text ="Blunt damage is only able to damage defence, but it is dealt before the normal damage is";
			hasFirstSecondaryAttribute = true;
		}
		// Check if the weapon deals piercing damage
		if (card.piercingDamage > 0){
			if (hasFirstSecondaryAttribute){
				secondSecondaryAttributeTitle.text = "PIERCING";
				secondSecondaryAttributeDescription.text ="Pierdcing Damage ignors Defence, deal dirat HP Damage";
				hasSecondSecondaryAttribute = true;
			}
			firstSecondaryAttributeTitle.text = "PIERCING";
			firstSecondaryAttributeDescription.text ="Pierdcing Damage ignors Defence, deal dirat HP Damage";
			hasFirstSecondaryAttribute = true;
		}
	}
	public void SetDefence(){
		hasPrimaryAttribute = true;
		SO_CardArmor card = cardDataBase.FindArmorCardByID(id);
		backgroundSR.sprite = card.image;
		primaryAttributeTitle.text = "DEFENCE";
		primaryAttributeDescription.text = "The defence is the amount of damage the armor need to take before destroyed";
		// Check if the weapon deals poison damage
		if (card.spickedBonus > 0){
			firstSecondaryAttributeTitle.text = "SPIKED";
			firstSecondaryAttributeDescription.text ="When you get attacked, the monster takes damage equel to your spiked";
			hasFirstSecondaryAttribute = true;
		}
		// Check if the weapon deals Blunt damage
		if (card.brawl > 0){
			if (hasFirstSecondaryAttribute){
				secondSecondaryAttributeTitle.text = "BRAWL";
				secondSecondaryAttributeDescription.text ="If you don't have a weapon equiped, you deal damage equel to your brawl";
				hasSecondSecondaryAttribute = true;
			}
			firstSecondaryAttributeTitle.text = "BRAWL";
			firstSecondaryAttributeDescription.text ="If you don't have a weapon equiped, you deal damage equel to your brawl";
			hasFirstSecondaryAttribute = true;
		}

	}
	public void SetUtility(){
		hasSpecial = true;
		CardUtility card = cardDataBase.FindUtilityCardByID(id);
		backgroundSR.sprite = card.image;
		print(card.GetType().Name);
		if (card.GetType().Name == "SO_UtilityAp"){
			specialTitle.text = "AP Increase";
			specialDescription.text = "This type of utility card, increases the amount of AP you have this turn";
		} else if (card.GetType().Name == "SO_UtilityDamage"){
			specialTitle.text = "Damage";
			specialDescription.text = "This type of utility card, deals damage to the monster when played";
		} else if (card.GetType().Name == "SO_UtilityDeBuff"){
			specialTitle.text = "Debuff";
			specialDescription.text = "This type of utility card, decreases the abilities of the monster";
		} else if (card.GetType().Name == "SO_UtilityDoubleAttack"){
			specialTitle.text = "Double Attack";
			specialDescription.text = "This type of utility card, makes you attack twice this turn";
		} else if (card.GetType().Name == "SO_UtilityDraw"){
			specialTitle.text = "Draw";
			specialDescription.text = "This type of utility card, draws extra cards, but you may have to dicard some";
		} else if (card.GetType().Name == "SO_UtilityHealth"){
			specialTitle.text = "Health";
			specialDescription.text = "This type of utility card, gives you back some of your banished card";
		} else if (card.GetType().Name == "SO_UtilityPermaBuff"){
			specialTitle.text = "Perment Buff";
			specialDescription.text = "This type of utility card, gives you buff which last until the of the battle";
		} else if (card.GetType().Name == "SO_UtilityRage"){
			specialTitle.text = "Rage";
			specialDescription.text = "For each weapon you have, add your rage bonus to your attacks";
		} else if (card.GetType().Name == "SO_UtilityRecycle"){
			specialTitle.text = "Recycle";
			specialDescription.text = "Allways you to take cards from your discard pile, back op to your hand";
		}

	}
	public void SetType(string type) {
		if (type == "Melee"){
			typeTitle.text = "MELEE";
			typeDescription.text = "Melee cards are placed to your left, and deals their damage at the end of your turn";
		} else if (type =="Armor"){
			typeTitle.text = "ARMOR";
			typeDescription.text = "Armor cards are placed to your right, and protect you from incoming attacks";
		} else if (type == "Utility"){
			typeTitle.text = "UTILITY";
			typeDescription.text = "Utility cards have a long array of diffrent effects they do when played";
		} else if (type == "Curse"){
			typeTitle.text = "CURSE";
			typeDescription.text = "Curse cards, are played at ones when drawn";
		}
	}
	public void SetCurse(){
		hasSpecial = true;
		CardCurse card = cardDataBase.FindCurseCardByID(id);
		backgroundSR.sprite = card.image;
		print(card.GetType().Name);
		if (card.GetType().Name == "SO_CurseAP"){
			specialTitle.text = "Loss of AP";
			specialDescription.text = "When this card is drawn, you lose some ap";
		} else if (card.GetType().Name == "SO_CurseBomb"){
			specialTitle.text = "Bomb";
			specialDescription.text = "When drawn, every one takes damage";
		}
	}
	public void RemoveFalse(){
		if (hasPrimaryAttribute == false) {
			Destroy(this.gameObject.transform.GetChild(0).gameObject);
		}
		if (hasFirstSecondaryAttribute == false) {
			Destroy(this.gameObject.transform.GetChild(1).gameObject);
		}
		if (hasSecondSecondaryAttribute == false) {
			Destroy(this.gameObject.transform.GetChild(2).gameObject);
		}
		if (hasCost == false) {
			Destroy(this.gameObject.transform.GetChild(4).gameObject);
		}
		if (hasSpecial == false) {
			Destroy(this.gameObject.transform.GetChild(5).gameObject);
		}
	}
}
