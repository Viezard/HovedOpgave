using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_BattleManager : MonoBehaviour {

   
    public SaveDataManager SaveDataManager;
    public Sc_GameManager gameManager;
	private Sc_CardDataBase cardDataBase;

	private Sc_LevelManager levelManager;
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
	public List<int> currentEquimentspiked = new List<int>(); // The armor equipment cards the player currenly has aktive and their hp
	private int currentTotalDefence; 
	private int currentNormalAttack;
	private int currentBluntAttack;
	private int currentPiercingAttack;
	
	private int currentPoisonttack;
	public int currentSpiked;
	public int currentRage;
	public Text defenceText;
	public Text SpikedText;
	public int currentMonster; // The id of the monster the player is figtig at the moment
	public int maxHandSize; // The current max hand size the player has. 

	public GameObject card; // getting the prefab of the card 
	public GameObject armor;
	public GameObject weapon;

	public Sc_Monster monster;

	public Text discardPile;
	public Text DeckPile;
	public Text BanishPile;

	void Awake(){
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		monster = GameObject.FindObjectOfType<Sc_Monster>();
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
        SaveDataManager = GameObject.FindObjectOfType<SaveDataManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();
        
    }
	// Use this for initialization
	void Start () {
		// set op some basic variables 
		maxHandSize = 5;
		currentStage = 0;
		currentApMax = 2; 
		currentApUsed = 0; 
		currentTotalDefence = 0; 
		currentNormalAttack = 0;
		currentBluntAttack = 0;
		currentPiercingAttack = 0;
		currentPoisonttack = 0;
		currentSpiked = 0;
		currentRage = 0;
		currentHandObjects.Clear();
		currentEquipmentArmor.Clear();
		currentEquipmentMelee.Clear();
		currentEquimentDamage.Clear();
		currentEquimentspiked.Clear();

		
		// Setting player related text
		defenceText.text = "" + currentTotalDefence;
		// Add some random cards to deck 
		for (int i = 0; i < gameManager.fullDeck.Count; i++){
			currentDeck.Add(gameManager.fullDeck[i]);
		}
		for (int i = 0; i < currentDeck.Count; i++){
			print ("number " + i + "In Deck is " + currentDeck[i]);
		}
		// Shuffle the deck 
		ShuffleDeck();
		// Draw Hand
		DrawHand();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSpiked();
		SpikedText.text = "" + currentSpiked;
		discardPile.text = currentDiscard.Count + " in the discard pile";
		DeckPile.text = currentDeck.Count + " in the deck pile";
		BanishPile.text = currentBanished.Count + " in the banish pile";
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
		if (currentStage == 10){
			BattleWon();
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
		if (id < 1000){ // If the card is a melee card 
			SO_CardMelee card = cardDataBase.FindMeleeCardByID(id);
			Melee (id, card.normalDamage, card.bluntDamage, card.piercingDamage, card.poisonDamage);
		} else if (id < 2000){
			SO_CardArmor card = cardDataBase.FindArmorCardByID(id);
			Defence (id, card.armorBonus, card.spickedBonus);
		} else if (id < 3000){
			
			Utility(id);
		}
	}

	public void Melee (int id, int normal, int blunt, int piercing, int poison) {
		
	currentNormalAttack += normal + currentRage;
	currentBluntAttack += blunt;
	currentPiercingAttack += piercing;
	currentPoisonttack += poison;
	print("helloooooo");
	
	GameObject newEquipment = (GameObject)Instantiate (weapon, transform.position, transform.rotation);
	Sc_MeleeEquipment newCardScript =  newEquipment.GetComponent<Sc_MeleeEquipment>();
	newCardScript.id = id;
	}

	public void Utility (int id) {
		CardUtility card = cardDataBase.FindUtilityCardByID(id);
		card.PlayedFunction();
	}
	public void Defence(int id, int defence, int spiked) {
		print("" + spiked);
		currentTotalDefence += defence; 
		GameObject newEquipment = (GameObject)Instantiate (armor, transform.position, transform.rotation);
		Sc_DefenceEquipment newCardScript =  newEquipment.GetComponent<Sc_DefenceEquipment>();
		newCardScript.id = id;
		defenceText.text = "" + currentTotalDefence;
		currentEquimentDamage.Add(defence);
		currentEquimentspiked.Add(spiked);
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
		print(target + "Is being attacked");
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
			if (monster.defence <= 0){
				monster.health -= poison;
			} 
			if (monster.defence - blunt >= 0){
				monster.defence -= blunt;
			} else {
				monster.defence = 0;
			}
			
			for (int i = 0; i < damage; i++){
				if (monster.defence > 0){
					monster.defence -= 1;
				} else {
					monster.health -=1;
				}
			}
			monster.health -= piercing;
			monster.healthText.text = "" + monster.health;
			monster.defenceText.text = "" + monster.defence;
			if (monster.health <= 0){
				currentStage = 10;
			}
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
						currentEquimentspiked.RemoveAt(0);
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
						currentDeck.Add(currentDiscard[j]);
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
	public void Healing(int healing) {
		print("number of cards in banish pile " + currentBanished.Count);
		print("number of cards in Discard pile " + currentDiscard.Count);
		for (int i = 0; i < healing; i++){
			if (currentBanished.Count > 0){
				int random = Random.Range(0, currentBanished.Count - 1);
				currentDiscard.Add(currentBanished[random]);
				currentBanished.RemoveAt(random);
			}
		}
		print("number of cards in banish pile " + currentBanished.Count);
		print("number of cards in Discard pile " + currentDiscard.Count);
	}
	public void CalcSpiked () {
		currentSpiked = 0;
		for (int i = 0; i < currentEquimentspiked.Count; i++){
			currentSpiked += currentEquimentspiked[i];
		}
	}

	public void BattleWon () {
		if (Input.GetMouseButtonDown(0)){
			ExitBattle();
			levelManager.ChangeSceneTo("Navigation");
		}
	}
	public void ExitBattle() {
		gameManager.fullDeck.Clear();
		for (int i = 0; i < currentDeck.Count; i++){
			print("Card added from deck");
			gameManager.fullDeck.Add(currentDeck[i]);
		}
		for (int i = 0; i < currentDiscard.Count; i++){
			print("Card added from Discard");
			gameManager.fullDeck.Add(currentDiscard[i]);
		}
		for (int i = 0; i < currentHandObjects.Count; i++){
			print("Card added from Hand");
			GameObject card = currentHandObjects[i];
			Sc_Card cardScript = card.GetComponent<Sc_Card>();
			gameManager.fullDeck.Add(cardScript.cardID);
		}
		
		for (int i = 0; i < currentBanished.Count; i++){
			gameManager.lostCards.Add(currentBanished[i]);
		}
	}

	public void Recycle (int cardsToTake){
		if (currentDiscard.Count > 0){
			for (int i = 0; i < cardsToTake; i++){
				int random = Random.Range(0, currentDiscard.Count);
				GameObject newCard = (GameObject)Instantiate (card, transform.position, transform.rotation);
				Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
				newCardScript.cardID = currentDiscard[random];
				currentDiscard.RemoveAt(random);
			}
		}
	}
	public void EndTurn () {
		currentStage = 2;
		currentApUsed = 0;
		DamageCalc(target: 1, damage: currentNormalAttack, blunt: currentBluntAttack, piercing: currentPiercingAttack, poison: currentPoisonttack);
		currentNormalAttack = 0;
		currentBluntAttack = 0;
		currentPiercingAttack = 0;
		for (int i = 0; i < currentEquipmentMelee.Count; i++){
			Destroy(currentEquipmentMelee[i]);
		}
		currentEquipmentMelee.Clear();
        SaveDataManager.saveData.date = System.DateTime.Now.ToShortDateString();
        SaveDataManager.saveData.time = System.DateTime.Now.ToShortTimeString();
        SaveDataManager.SaveGameData();
    }
}
