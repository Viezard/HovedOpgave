﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_BattleManager : MonoBehaviour {

	public Sc_GameManager gameManager;
// Varibels used in the Battle section
	public List<int> currentDeck = new List<int>(); // The cards you have in your Deck
	public static List<GameObject> currentHandObjects = new List<GameObject>(); // holdes the cards in your hand 
	public List<int> currentDiscard = new List<int>(); // The cards you have in your discard
	public List<int> currentBanished = new List<int>(); // The cards lost in this battle
	public List<int> currentEffects = new List<int>(); // The effects currently in effect and how many turns they have left
	public int currentTurns; // The number of turns which as past in the current battle
	public bool currentTurnHolder; // Keeps track on how turn it is True equals Player and False equals Monster
	public int currentStage; // Which stage your in 0 effect and draw 1 Play cards 2 end turn 
	public List<int> currentMosnterStat = new List<int>(); // Keeps track of the monsters Life, defence and damage 
	public int currentApUsed; // how much has been used
	public int currentApMax; // What is the max ap for this turn  
	public static List<GameObject> currentEquipmentArmor = new List<GameObject>(); // The current equpment cards the player has in play
	public static List<GameObject> currentEquipmentMelee = new List<GameObject>(); // The current equpment cards the player has in play
	public List<int> currentEquimentDamage = new List<int>(); // The armor equipment cards the player currenly has aktive and their hp
	private int currentTotalDefence; 
	private int currentTotalAttack;
	public Text defenceText;
	public int currentMonster; // The id of the monster the player is figtig at the moment
	public int maxHandSize; // The current max hand size the player has. 

	public GameObject card; // getting the prefab of the card 
	public GameObject armor;
	public GameObject weapon;

	public Sc_Monster monster;

	void Awake(){
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		monster = GameObject.FindObjectOfType<Sc_Monster>();
	}
	// Use this for initialization
	void Start () {
		// set op some basic variables 
		maxHandSize = 5;
		currentStage = 0;
		currentApMax = 2; 
		currentApUsed = 0; 
		currentTotalDefence = 0; 
		currentTotalAttack = 0;
		
		// Setting player related text
		defenceText.text = "" + currentTotalDefence;
		// Add some random cards to deck 
		
		currentDeck = gameManager.fullDeck;
		for (int i = 0; i < currentDeck.Count; i++){
			print ("number " + i + "In Deck is " + currentDeck[i]);
		}
		DrawHand();
	}
	
	// Update is called once per frame
	void Update () {
		StageManager ();
	}
	void StageManager () {
		if (currentStage == 0){
			DrawHand();
			currentStage = 1;
		}
		if (currentStage == 2){
			monster.MonsterTurn();
		}
	}
	public void DrawHand() { // Used to draw a full hand 
		for (int i = currentHandObjects.Count; i < maxHandSize ; i++){
			Draw ();
		}
		for (int i = 0; i < currentDeck.Count ; i++){
			print ( i + "In Deck is " + currentDeck[i]);
		}
	}
	public void DrawSomeCards(int toDraw){ // draws a set number of cards from the deck 
		for (int i = 0; i < toDraw; i++){
			Draw ();
		}
	}

	public void Draw () {
		if (currentDeck.Count != 0){
			print("added " + currentDeck[0] + " To hand");
			GameObject newCard = (GameObject)Instantiate (card, transform.position, transform.rotation);
			Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
			newCardScript.cardID = currentDeck[0];
			currentDeck.RemoveAt(0);
			} else if (currentDiscard.Count > 0) { // If there are no more cards in the deck, but there are cards in the dicards pile, the discard pile will become the deck and the deck will be shuffled 
				print("Dicard pile is being shuffled into deck");
				for (int i = 0; i < currentDiscard.Count; i++){
					currentDeck.Add(currentDiscard[i]);
				}
				ShuffleDeck();
				currentDiscard.Clear();
				GameObject newCard = (GameObject)Instantiate (card, transform.position, transform.rotation);
				Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
				newCardScript.cardID = currentDeck[0];
				currentDeck.RemoveAt(0);
				
			} else {
				print("No Cards in deck or discard pile");
			}
	}
	public void PrintDeck(){
		for(int i = 0; i < currentDeck.Count; i++){
			print("added " + currentDeck[i] + " To hand");
		}
	}

	public void PrintDiscard(){
		for(int i = 0; i < currentDiscard.Count; i++){
			print( currentDiscard[i] + " Is in the discard Pile");
		}
	}
	public void PlayCard(int id){ // Play a card
		print ("you just played " + id);
		currentDiscard.Add(id);
		//currentHand.RemoveAt(randomCard);
		print("The number of cards left in hand is " + currentHandObjects.Count);
		if (id == 0){ // If the card is a melee card 
			Melee ();
		} else if (id == 1){
			Utility ();
		} else if (id == 2){
			Defence();
		}
	}

	public void Melee () {
		currentTotalAttack +=1;
		print("helloooooo");
		GameObject newEquipment = (GameObject)Instantiate (weapon, transform.position, transform.rotation);
	}

	public void Utility () {
		monster.TakeDamage(0, 2);
	}
	public void Defence() {
		currentTotalDefence += 1; 
		GameObject newEquipment = (GameObject)Instantiate (armor, transform.position, transform.rotation);
		defenceText.text = "" + currentTotalDefence;
		currentEquimentDamage.Add(1);
	}

	public void ShuffleDeck(){ // Shuffleling the deck useing the Fisher Yates Shuffle
		for (int i = currentDeck.Count - 1; i > 0; i--){
			int j = Random.Range(0,i);
			int temp = currentDeck[i];
			currentDeck[i] = currentDeck[j];
			currentDeck[j] = temp;
		}
		for (int i = 0; i < currentDeck.Count ; i++){
			print ( i + "In Deck is " + currentDeck[i]);
		}
	}

	public void DamageCalc (int target,int lifeDrain = 0, int poison = 0, int blunt = 0, int damage = 0, int piercing = 0 ){ // Target 0 = player 1 = enemy
		if (target == 0){ // if the target is the player 
			if (currentTotalDefence <= 0){ // if the player has 0 defence 
				if (lifeDrain > 0){ // If there is any life drain damage dealt 
					TakeDamage(lifeDrain, 1); // deal the damge 
					if (lifeDrain + monster.health < monster.maxHealth){ // check if the value healed plus the current health is more then the max
						monster.health += lifeDrain; // Give the monster som health 
					} else {
						monster.health = monster.maxHealth; // set the monsters health to it's max
					}
					print ("You took "+ lifeDrain + " lifedrain damage");
				}
				if (poison > 0){ // Check is there is being dealt some poison damage 
					TakeDamage(poison, 1); 
					print ("You took "+ poison + " poison damage");
				}
			}
			if (blunt > 0){// Check is there is being dealt some blunt damage 
				TakeDamage(blunt, 0);
				print ("You took "+ blunt + " blunt damage");
			}
			if (damage > 0){// Check is there is being dealt some normal damage 
				TakeDamage(damage, 1);
				print ("You took "+ damage + " damage damage");
			}
			if (piercing > 0){// Check is there is being dealt some piercing damage 
				TakeDamage(piercing, 2);
				print ("You took "+ piercing + " piercing damage");
			}
		}
		else if (target == 1){
			
		}
	}
	public void TakeDamage (int damage, int type){	// type blunt = 0 normal = 1 piercing = 2 
		for (int i = 0; i < damage; i ++){ // run though ones for every damage taken
			int damageDealt = 0; 
			if (type == 0 || type == 1){
				if (currentTotalDefence > 0){
					currentTotalDefence -= 1;
					damageDealt = 1;
					defenceText.text = "" + currentTotalDefence;
					currentEquimentDamage[0] -=1;
					if (currentEquimentDamage[0] <= 0){
						Destroy(currentEquipmentArmor[0]);
						currentEquipmentArmor.RemoveAt(0);
						currentEquimentDamage.RemoveAt(0);
						GameObject[] _armor = GameObject.FindGameObjectsWithTag("Armor");
						for (int j = 0; j < _armor.Length; j++){
							Sc_DefenceEquipment otherScript = _armor[j].GetComponent<Sc_DefenceEquipment>();
							otherScript.placementInEquipent -= 1;
						}
					}
				}
			}
			if (type == 1 && damageDealt == 0 || type == 2){
				if (currentDeck.Count > 0){ // check if there are still cards in the deck
					currentBanished.Add(currentDeck[0]);
					currentDeck.RemoveAt(0);
				} else if (currentDiscard.Count > 0){ // Check if there are cards in discard pile. 
					print("Dicard pile is being shuffled into deck");
					for (int j = 0; j < currentDiscard.Count; j++){
						currentDeck.Add(currentDiscard[i]);
					}
					ShuffleDeck();
					currentDiscard.Clear();
					currentBanished.Add(currentDeck[0]);
					currentDeck.RemoveAt(0);
				} else if (currentHandObjects.Count > 0) {
					int targetCard = Random.Range(0, currentHandObjects.Count);
					Destroy(currentHandObjects[targetCard]);
					currentHandObjects.RemoveAt(targetCard);
					GameObject[] _cards = GameObject.FindGameObjectsWithTag("Card");
					for (int j = 0; j < _cards.Length; j++){
						Sc_Card otherScript = _cards[j].GetComponent<Sc_Card>();
						if (otherScript.placementInHand > targetCard + 1){
							otherScript.placementInHand -= 1;
						}
					}
					
				} else {
					print ("you lost");
				}
			}
		}
	}
	public void EndTurn () {
		currentStage = 2;
		currentApUsed = 0;
		monster.TakeDamage(currentTotalAttack, 0);
		currentTotalAttack = 0;
		for (int i = 0; i < currentEquipmentMelee.Count; i++){
			Destroy(currentEquipmentMelee[i]);
		}
		currentEquipmentMelee.Clear();
	}
}
