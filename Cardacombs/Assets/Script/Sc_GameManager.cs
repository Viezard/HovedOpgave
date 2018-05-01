using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour {
	// Varibels used in the Battle section
	public List<int> fullDeck = new List<int>(); // All the cards which a part of the ones deck
	public List<int> currentDeck = new List<int>(); // The cards you have in your Deck
	public List<int> currentHand = new List<int>(); // The cards you have in your hand 
	public List<int> currentDiscard = new List<int>(); // The cards you have in your discard
	public List<int> currentBanished = new List<int>(); // The cards lost in this battle
	public List<int> currentEffects = new List<int>(); // The effects currently in effect and how many turns they have left
	public int currentTurns; // The number of turns which as past in the current battle
	public bool currentTurnHolder; // Keeps track on how turn it is True equals Player and False equals Monster
	public List<int> currentMosnterStat = new List<int>(); // Keeps track of the monsters Life, defence and damage 
	public List<int> currentEquipment = new List<int>(); // The current equpment cards the player has in play
	public List<int> currentEquimentDamage = new List<int>(); // The armor equipment cards the player currenly has aktive and their hp
	public int currentMonster; // The id of the monster the player is figtig at the moment
	public int maxHandSize; // The current max hand size the player has. 

	public GameObject card; // getting the prefab of the card 

	// Varibels used in the Navigation section 
	public List<int> eventsDone = new List<int>(); // A list of all the events which the player allready have done
	public List<int> monsterDone = new List<int>(); // A list of all the monsters the player has defeated 
	public int slayCount; // The number of monsters slay 
	public int currentEvent; // The ID of the current event 
	public List<int> lostCards = new List<int>(); // A list of all the cards which the player has lost


	// Use this for initialization
	void Start () {
		// set op some basic variables 
		maxHandSize = 4;
		// Add some random cards to deck 
		for (int i = 0; i < 15; i++ ){
			int randomCard = Random.Range(0,3);
			fullDeck.Add (randomCard);
		}
		currentDeck = fullDeck;
		for (int i = 0; i < currentDeck.Count; i++){
			print ("number " + i + "In Deck is " + currentDeck[i]);
		}
		DrawHand();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DrawHand() { // Used to draw a full hand 
		for (int i = 0; i < maxHandSize ; i++){
			currentHand.Add (currentDeck[0]);
			
			print("added " + currentHand[i] + " To hand");
			GameObject newCard = (GameObject)Instantiate (card, transform.position, transform.rotation);
			Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
			newCardScript.cardID = currentDeck[0];

			currentDeck.RemoveAt(0);
		}
		for (int i = 0; i < currentDeck.Count ; i++){
			print ( i + "In Deck is " + currentDeck[i]);
		}
	}
	public void DrawCard(int toDraw){ // draws a set number of cards from the deck 
		if (currentDeck.Count != 0){
			for (int i = 0; i < toDraw; i++){
				currentHand.Add (currentDeck[0]);
				print("added " + currentDeck[0] + " To hand");
				GameObject newCard = (GameObject)Instantiate (card, transform.position, transform.rotation);
				Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
				newCardScript.cardID = currentDeck[0];
				currentDeck.RemoveAt(0);				
			}
		} else { // If there are no more cards in the deck, but there are cards in the dicards pile, the discard pile will become the deck and the deck will be shuffled 
			if (currentDiscard.Count > 0) {
				print("Dicard pile is being shuffled into deck");
				currentDeck = currentDiscard;
				ShuffleDeck();
				currentDiscard.Clear();
			} else {
				print("No Cards in deck or discard pile");
			}
		}
	}

	public void PlayCard(){ // Play a random card from the hand 
		int randomCard = Random.Range(0, currentHand.Count);
		print ("you just played " + currentHand[randomCard]);
		currentDiscard.Add(currentHand[randomCard]);
		currentHand.RemoveAt(randomCard);
		print("The number of cards left in hand is " + currentHand.Count);
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
}
