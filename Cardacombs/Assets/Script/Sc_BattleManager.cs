using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Sc_BattleManager : MonoBehaviour {

   
    public SaveDataManager saveDataManager;
    public Sc_GameManager gameManager;
	private Sc_CardDataBase cardDataBase;
   

    private Sc_LevelManager levelManager;
// Varibels used in the Battle section
	public List<int> currentDeck = new List<int>(); // The cards you have in your Deck
	public static List<GameObject> currentHandObjects = new List<GameObject>(); // holdes the cards in your hand 
	public List<int> currentDiscard = new List<int>(); // The cards you have in your discard
	public List<int> currentBanished = new List<int>(); // The cards lost in this battle
	public List<GameObject> currentEffects = new List<GameObject>(); // The effects currently in effect and how many turns they have left
	public int currentStage; // Which stage your in 0 effect and draw 1 Play cards 2 end turn 
	public int currentApUsed; // how much has been used
	public int currentUtilityAP;
	public int currentApMax; // What is the max ap for this turn
    public string nyScene;
    public List<GameObject> currentEquipmentArmor = new List<GameObject>(); // The current equpment cards the player has in play
	public List<GameObject> currentEquipmentMelee = new List<GameObject>(); // The current equpment cards the player has in play
	public List<int> currentEquimentDamage = new List<int>(); // The armor equipment cards the player currenly has aktive and their hp
	public List<int> currentEquimentBrawl = new List<int>(); //  their brawl bonus
	public int currentToughness;
	private int currentTotalDefence; // how much defece the player as at this point 
	public int currentNumberOfAttacks;
	private int currentNormalAttack;	// how much normal damage the players weapon are dealing totally this turn
	private int currentBluntAttack; // Same just with blunt damage
	private int currentPiercingAttack; // with piercing damage
	private int currentPoisonttack; // With Poision damage 
	public int currentPermenentSpiked;
	public int currentSpiked; // What is total spike damage is right now
	public int currentBrawl;  // How much damage he deal if he does not have a weapon 
	public int currentRage; // How much extre damage every weapon deals 
	public TextMesh defenceText;
	public TextMesh SpikedText;
	public int maxHandSize; // The current max hand size the player has. 
	// Effect realted varibles 

	public bool mayPlayUtility;
	public bool mayPlayMelee;
	public bool mayPlayArmor;
	public bool HasLostTurn;

	public GameObject card; // getting the prefab of the card 
	public GameObject armor;
	public GameObject weapon;
    
	public MonsterClass monster;

	public Text discardPile;
	public Text DeckPile;
	public Text BanishPile;

	public List<GameObject> apObjects = new List<GameObject>(); // 
	public GameObject standardApIcon;

	// log book bliver ikke brugt 
	public List<Text> logBook = new List<Text>();
	public Text aLog;

	public void PrintLog(string text, string color){
		Text newLog = (Text)Instantiate (aLog, transform.position, transform.rotation);
		newLog.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
		newLog.text = text;
		if (color == "blue"){
			newLog.color = Color.blue;
		} else if (color == "red"){
			newLog.color = Color.red;
		} else if (color == "green"){
			newLog.color = Color.green;
		} else if (color == "black"){
			newLog.color = Color.black;
		} else if (color == "yellow"){
			newLog.color = Color.yellow;
		}
	}


	void Awake(){
        cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
        saveDataManager = GameObject.FindObjectOfType<SaveDataManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();

        saveDataManager.LoadGameData();
    }
	// Use this for initialization
	void Start () {	
		print("make monster");
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		GameObject newMonster = (GameObject)Instantiate (gameManager.currentMonster, transform.position, transform.rotation);
		Vector3 monsterPosition = new Vector3 (-0.57f, 1.96f, 0);
		newMonster.transform.position = monsterPosition;
		monster = GameObject.FindObjectOfType<MonsterClass>();
		
        saveDataManager.saveData.currentScene = "Battle";

		currentDeck.Clear();
		currentBanished.Clear();
		currentDiscard.Clear();
		currentHandObjects.Clear();
		currentEquipmentArmor.Clear();
		currentEquipmentMelee.Clear();
		
        // set op some basic variables 
        maxHandSize = 5;

        currentApUsed = 0;
		currentStage = 0;
        currentUtilityAP = gameManager.utilityAP;
        currentTotalDefence = gameManager.startingDefence; 
		currentToughness = 0;
		currentNormalAttack = 0;
		currentBluntAttack = 0;
		currentPiercingAttack = 0;
		currentPoisonttack = 0;
		currentSpiked = 0;
		currentNumberOfAttacks = 1;
		currentPermenentSpiked = 0;
		currentRage = 0;
      
        if (gameManager.isLoading == true){
			Load();
		}else {
			for (int i = 0; i < gameManager.fullDeck.Count; i++) {
                currentDeck.Add(gameManager.fullDeck[i]);
            }
			currentApMax = 2;
		}
		// Create Ap Icons
		for (int i = 0; i < currentApMax; i ++){
			GameObject apIcon = (GameObject)Instantiate(standardApIcon, this.gameObject.transform.position, this.gameObject.transform.rotation);
			Sc_ApIcon newscript = apIcon.GetComponent<Sc_ApIcon>();
			newscript.isStandard = true;
		}
		for (int i = 0; i < gameManager.utilityAP; i ++){
			GameObject apIcon = (GameObject)Instantiate(standardApIcon, this.gameObject.transform.position, this.gameObject.transform.rotation);
			Sc_ApIcon newscript = apIcon.GetComponent<Sc_ApIcon>();
			newscript.isUtility = true;
		}
        // effect realted varibles
        mayPlayUtility = true;
		mayPlayMelee = true;
		mayPlayArmor = true;
		HasLostTurn = false;
   
        // Setting player related text
        defenceText.text = "" + currentTotalDefence;
        // Add some random cards to deck 
        ShuffleDeck();

	}

	public void	Load(){
		gameManager.isLoading = false;
		currentDeck = saveDataManager.saveData.currentDeckSave;
		currentDiscard = saveDataManager.saveData.currentDiscardSave;
		currentBanished = saveDataManager.saveData.currentBanishedSave;
		currentEffects = saveDataManager.saveData.currentEffectsSave;
		currentApUsed = saveDataManager.saveData.currentApUsedSave;
		currentUtilityAP = saveDataManager.saveData.currentUtilityAPSave;
		currentApMax = saveDataManager.saveData.currentApMaxSave;
		currentStage = 1;
	
		// Draw hand 
		for (int i = 0; i < saveDataManager.saveData.currentHandObjectsSave.Count; i++) {
			Debug.Log(saveDataManager.saveData.currentHandObjectsSave[i]);
			GameObject newCard = (GameObject)Instantiate(card, new Vector3(2.50f, -3.1f, 3), Quaternion.Euler(0f, 180f, 90f));
			Sc_Card newCardScript = newCard.GetComponent<Sc_Card>();
			newCardScript.cardID = saveDataManager.saveData.currentHandObjectsSave[i]; 
		}
		// Add saved armor cards into play 
		for (int i = 0; i < saveDataManager.saveData.currentEquipmentArmorSave.Count; i++) {
			Debug.Log(saveDataManager.saveData.currentEquipmentArmorSave[i]);
			int idArmor = saveDataManager.saveData.currentEquipmentArmorSave[i];
			Debug.Log(idArmor);
			if (idArmor < 2000) {
				SO_CardArmor card = cardDataBase.FindArmorCardByID(idArmor);
				Defence(idArmor, card.armorBonus, card.spickedBonus);
				PrintLog("you just played " + card.name, "green");
			}
		}
		// Add saved melee cards into play
		for (int i = 0; i < saveDataManager.saveData.currentEquipmentMeleeSave.Count; i++){
			Debug.Log(saveDataManager.saveData.currentEquipmentMeleeSave[i]);
			int idMelee = saveDataManager.saveData.currentEquipmentMeleeSave[i];
			Debug.Log(idMelee);
			if (idMelee < 1000)	{ // If the card is a melee card 
				SO_CardMelee card = cardDataBase.FindMeleeCardByID(idMelee);
				Melee(idMelee, card.normalDamage, card.bluntDamage, card.piercingDamage, card.poisonDamage);
				PrintLog("you just played " + card.name, "green");
			}
		}
		// Draw hand 
		for (int i = 0; i < gameManager.fullDeck.Count; i++) {
			currentDeck.Add(gameManager.fullDeck[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager == null){
			// create a monster
			
		} else {
			SpikedText.text = "" + currentSpiked;
			discardPile.text = "Discard: " + currentDiscard.Count;
			DeckPile.text = "Deck: " + currentDeck.Count;
			BanishPile.text = "Banish: "  + currentBanished.Count;
			StageManager ();
		}

		if (Input.GetButtonDown("Jump")){
			GameObject apIcon = (GameObject)Instantiate(standardApIcon, this.gameObject.transform.position, this.gameObject.transform.rotation);
			Sc_ApIcon newscript = apIcon.GetComponent<Sc_ApIcon>();
			newscript.isOneTime = true;
		}
		
		
	}
	void StageManager () {
		if (currentStage == 0){
            TurnStart();
			saveDataManager.SaveGameData();
        }
		if (currentStage == 2){
			
			monster.MonsterTurn();
            
        }
		if (currentStage == 10){
			BattleWon();
		}
	}
	public void TurnStart(){
        // Resetting Varibles
		currentEquipmentMelee.Clear();
        currentStage = 1;
            currentApUsed = 0;
            currentUtilityAP = gameManager.utilityAP;
            currentNormalAttack = 0;
            currentBluntAttack = 0;
            currentPiercingAttack = 0;
            currentNumberOfAttacks = 1;
            // Draw a hand 
            DrawHand();

        mayPlayUtility = true;
		mayPlayMelee = true;
		mayPlayArmor = true;
		HasLostTurn = false;

		// Reset monster varibles
		monster.chanceToHitSelf = 0;
		monster.chanceToMiss = 0;
		// Run all effects 
		for (int i = 0; i < currentEffects.Count; i++){
			Effect effect =  currentEffects[i].GetComponent<Effect>();
			bool wasDestroyed = effect.StartEffect();
			if (wasDestroyed){
				i -=1;
			}
		}
		
		if (currentToughness != 0){
			HasToughness();
		}	
		if (currentStage != 10 && HasLostTurn == false){
			currentStage = 1;
		} else if (HasLostTurn == true){
			currentStage = 2;
		}
        
    }
	public void DrawHand() { // Used to draw a full hand 
		for (int i = currentHandObjects.Count; i < maxHandSize ; i++){
			Draw ();
		}
	}
	public void DrawSomeCards(int toDraw){ // draws a set number of cards from the deck 
		for (int i = 0; i < toDraw; i++){
			Draw ();
		}
        
    }

	public void Draw () {
		if (currentDeck.Count != 0){
            GameObject newCard = (GameObject)Instantiate(card, new Vector3(2.5f, -3.1f, 3), Quaternion.Euler(0f, 180f, 90f));
            //GameObject newCard = (GameObject)Instantiate(card, new Vector3(12.5f, 1.1f, 0), Quaternion.identity);
            // instantiate at deck pile
            // transform 180 from back to show front 
            // Transform to hand position
            Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
			newCardScript.cardID = currentDeck[0];
			currentDeck.RemoveAt(0);
			} else if (currentDiscard.Count > 0) { // If there are no more cards in the deck, but there are cards in the dicards pile, the discard pile will become the deck and the deck will be shuffled 
				for (int i = 0; i < currentDiscard.Count; i++){
					currentDeck.Add(currentDiscard[i]);
				}
				ShuffleDeck();
				currentDiscard.Clear();
            // Make discard pile invisable
            GameObject newCard = (GameObject)Instantiate(card, new Vector3(10.55f, 2.21f, 3), Quaternion.FromToRotation(new Vector3(0, 180, 90), new Vector3(0, 0, 0)));
            Sc_Card newCardScript =  newCard.GetComponent<Sc_Card>();
				newCardScript.cardID = currentDeck[0];
				currentDeck.RemoveAt(0);
				
			} else {
				PrintLog("No Cards in deck or discard pile", "black");
			}
	}
	public void PrintDeck(){
		for(int i = 0; i < currentDeck.Count; i++){
			PrintLog("added " + currentDeck[i] + " To hand", "black");
		}
	}

	public void PrintDiscard(){
		for(int i = 0; i < currentDiscard.Count; i++){
			PrintLog( currentDiscard[i] + " Is in the discard Pile", "black");
		}
	}
	public void PlayCard(int id){ // Play a card
		
		currentDiscard.Add(id);
        // if( discard > 0 ) make discard pile visable
        // make discard pile sprite be last discarded card
        //currentHand.RemoveAt(randomCard);
        if (id < 1000){ // If the card is a melee card 
			SO_CardMelee card = cardDataBase.FindMeleeCardByID(id);
			Melee (id, card.normalDamage, card.bluntDamage, card.piercingDamage, card.poisonDamage);
			PrintLog ("you just played " + card.name, "green");
		} else if (id < 2000){
			SO_CardArmor card = cardDataBase.FindArmorCardByID(id);
			Defence (id, card.armorBonus, card.spickedBonus);
			PrintLog ("you just played " + card.name, "green");
		} else if (id < 3000){
			CardUtility card = cardDataBase.FindUtilityCardByID(id);
			PrintLog ("you just played " + card.name, "green");
			Utility(id);
		} else if (id < 4000){
			CardCurse card = cardDataBase.FindCurseCardByID(id);
			PrintLog ("You got cursed by" + card.name, "red");
			card.PlayedFunction();
		}
	}

	public void Melee (int id, int normal, int blunt, int piercing, int poison) {
		
	currentNormalAttack += normal + currentRage;
	currentBluntAttack += blunt;
	currentPiercingAttack += piercing;
	currentPoisonttack += poison;

	GameObject newEquipment = (GameObject)Instantiate (weapon, transform.position, transform.rotation);
	Sc_MeleeEquipment newCardScript =  newEquipment.GetComponent<Sc_MeleeEquipment>();
	newCardScript.id = id;
	}

	public void Utility (int id) {
		CardUtility card = cardDataBase.FindUtilityCardByID(id);
		card.PlayedFunction();
	}
	public void Defence(int id, int defence, int spiked) {
		currentTotalDefence += defence; 
		GameObject newEquipment = (GameObject)Instantiate (armor, transform.position, transform.rotation);
		Sc_DefenceEquipment newCardScript =  newEquipment.GetComponent<Sc_DefenceEquipment>();
		newCardScript.id = id;
		defenceText.text = "" + currentTotalDefence;
		currentEquimentDamage.Add(defence);
	}

	public void ShuffleDeck(){ // Shuffleling the deck useing the Fisher Yates Shuffle
		for (int i = currentDeck.Count - 1; i > 0; i--){
			int j = Random.Range(0,i);
			int temp = currentDeck[i];
			currentDeck[i] = currentDeck[j];
			currentDeck[j] = temp;
		}
	}

	public void DamageCalc (int target,int lifeDrain = 0, int poison = 0, int blunt = 0, int damage = 0, int piercing = 0 ){ // Target player = 0  enemy = 1 
		if (target == 0){
			PrintLog(" You are being attacked","red");
		} else {
			PrintLog("Monster Is being attacked","yellow");
		}
		
		if (target == 0){ // if the target is the player 
			if (currentTotalDefence <= 0){ // if the player has 0 defence 
				if (lifeDrain > 0){ // If there is any life drain damage dealt 
					TakeDamage(lifeDrain, 1); // deal the damge 
					if (lifeDrain + monster.health < monster.maxHealth){ // check if the value healed plus the current health is more then the max
						monster.health += lifeDrain; // Give the monster som health 
					} else {
						monster.health = monster.maxHealth; // set the monsters health to it's max
					}
					PrintLog ("You took "+ lifeDrain + " lifedrain damage", "black");
				}
				if (poison > 0){ // Check is there is being dealt some poison damage 
					TakeDamage(poison, 1); 
					PrintLog ("You took "+ poison + " poison damage", "black");
				}
			}
			if (blunt > 0){// Check is there is being dealt some blunt damage 
				TakeDamage(blunt, 0);
				PrintLog ("You took "+ blunt + " blunt damage", "black");
			}
			if (damage > 0){// Check is there is being dealt some normal damage 
				TakeDamage(damage, 1);
				PrintLog ("You took "+ damage + " damage damage", "black");
			}
			if (piercing > 0){// Check is there is being dealt some piercing damage 
				TakeDamage(piercing, 2);
				PrintLog ("You took "+ piercing + " piercing damage", "black");
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
					if (currentEquipmentArmor.Count > 0){
						Sc_DefenceEquipment ArmorScript =  currentEquipmentArmor[0].GetComponent<Sc_DefenceEquipment>();
						ArmorScript.defence -=1;
						ArmorScript.UpdateText();
						if (ArmorScript.defence <= 0){
                            // Sc_DefenceEquipment defenceScript = currentEquipmentArmor[i].GetComponent<Sc_DefenceEquipment>();
                            ArmorScript.moveToDiscard = true;
                            // Destroy(currentEquipmentArmor[0]);
							currentEquipmentArmor.RemoveAt(0);
							GameObject[] _armor = GameObject.FindGameObjectsWithTag("Armor");
							for (int j = 0; j < _armor.Length; j++){
								Sc_DefenceEquipment otherScript = _armor[j].GetComponent<Sc_DefenceEquipment>();
								otherScript.placementInEquipent -= 1;
							}
						}
					}
				}
			}
			if (type == 1 && damageDealt == 0 || type == 2){
				if (currentDeck.Count > 0){ // check if there are still cards in the deck
					currentBanished.Add(currentDeck[0]);
                    // if banished pile is > 0 then make banishpile visable 
                    // make discard pile sprite be last discarded card
                    currentDeck.RemoveAt(0);
				} else if (currentDiscard.Count > 0){ // Check if there are cards in discard pile. 
					PrintLog("Dicard pile is being shuffled into deck","black");
					for (int j = 0; j < currentDiscard.Count; j++){
						currentDeck.Add(currentDiscard[j]);
					}
					ShuffleDeck();
					currentDiscard.Clear();
					currentBanished.Add(currentDeck[0]);
                    // if banished pile is > 0 then make banishpile visable 
                    // make discard pile sprite be last discarded card
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
                    // if banished pile is > 0 then make banishpile visable 
                    // make discard pile sprite be last discarded card
                    }

                } else {
					PrintLog ("you lost","red");
                    playerLost();
				}
			}
		}
	}
	public void Healing(int maxHealth, int minHealth, int chance) {
		int healing = 0;
		int random = Random.Range(0, 100);
		if (chance > random){
			healing = maxHealth;
		} else {
			healing = minHealth;
		}
		for (int i = 0; i < healing; i++){
			if (currentBanished.Count > 0){
				random = Random.Range(0, currentBanished.Count - 1);
				currentDiscard.Add(currentBanished[random]);
				currentBanished.RemoveAt(random);
			}
		}

	}

	public void BattleWon () {
		if (Input.GetMouseButtonDown(0)){
			gameManager.slayCount += 1;
			ExitBattle();
           // saveDataManager.saveData.readyfornewBattle = true;
            // SaveDataManager.saveData.currentStageSave = 0;
			currentEquipmentMelee.Clear();
            saveDataManager.SaveGameData();
            levelManager.ChangeSceneTo("Navigation");
		}
	}
	public void ExitBattle() {
		gameManager.fullDeck.Clear();
		for (int i = 0; i < currentDeck.Count; i++){
			gameManager.fullDeck.Add(currentDeck[i]);
		}
		for (int i = 0; i < currentDiscard.Count; i++){
			gameManager.fullDeck.Add(currentDiscard[i]);
		}
		for (int i = 0; i < currentHandObjects.Count; i++){
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

	public void Debuff(int debuff, int chanceDecreaseDamage, bool cantHit, int chanceCantHit, bool hitSelf, int chanceHitself) {

		monster.debuffed += debuff;
		if (cantHit == true) {
			monster.chanceToMiss = chanceCantHit;
		}
		if (hitSelf == true){
			monster.chanceToHitSelf = chanceHitself;
		}
	}

	public void DrawCards(int draw, int discard, bool isRandom, bool afterOrBefore ){
		if (afterOrBefore == true){
			for (int i = 0; i < draw; i++){
				Draw();
			}
		}
		if (0 < discard){
			DiscardCards(discard,isRandom);
		}
		if (afterOrBefore == false){
			for (int i = 0; i < draw; i++){
				Draw();
			}
		}	
	}
	public void DiscardCards (int discard, bool isRandom){
		if (isRandom == true){
			for(int i = 0; i < discard; i++){
				if (currentHandObjects.Count > 0){
					// get random card in hand 
					int random = Random.Range(0, currentHandObjects.Count);
					GameObject card = currentHandObjects[random];
					Sc_Card cardScript =  card.GetComponent<Sc_Card>();
					currentDiscard.Add(cardScript.cardID);
					cardScript.DestroyCard();
				}
			}
		}
	}

	public void BonusAP (int apBonus){
		currentApUsed -=1;
		if (currentApUsed < 0){
			GameObject apIcon = (GameObject)Instantiate(standardApIcon, this.gameObject.transform.position, this.gameObject.transform.rotation);
			Sc_ApIcon newscript = apIcon.GetComponent<Sc_ApIcon>();
			newscript.isOneTime = true;
		} 
	}
	public void Buffing (int toughness, int spiked){
		if (toughness != 0){
			currentToughness += toughness;
			HasToughness();
		}
		if (spiked != 0){
			currentSpiked += spiked;
		}
	}
	public void HasToughness(){ // is runned if you have toughnees and checks if it as a toughness card to update, or if it has to make one. 
		bool toughnessIsInPlay = false;
		int toughnessPlace = 0;
		for (int i = 0; i < currentEquipmentArmor.Count; i++){
			GameObject card = currentEquipmentArmor[i];
			Sc_DefenceEquipment cardScript =  card.GetComponent<Sc_DefenceEquipment>();
			if (cardScript.id == -10){
				toughnessIsInPlay = true;
				toughnessPlace = i;
			}
		}
		if (toughnessIsInPlay == true){
			GameObject card = currentEquipmentArmor[toughnessPlace];
			Sc_DefenceEquipment cardScript =  card.GetComponent<Sc_DefenceEquipment>();
			currentTotalDefence += currentToughness - cardScript.defence;
			cardScript.UpdateToughness();
			
		} else {
			GameObject newEquipment = (GameObject)Instantiate (armor, transform.position, transform.rotation);
			Sc_DefenceEquipment CardScript =  newEquipment.GetComponent<Sc_DefenceEquipment>();
			CardScript.id = -10;
			currentTotalDefence += currentToughness;
			CardScript.UpdateToughness();
		}
		defenceText.text = "" + currentTotalDefence;
	}
	public void ExtraAttacks(int extraAttacks) {
		currentNumberOfAttacks += extraAttacks;
	}
	public void EndTurn () {
        
        for (int i = 0; i < currentEquipmentMelee.Count; i++)
        {
            
            Sc_MeleeEquipment meleeScript = currentEquipmentMelee[i].GetComponent<Sc_MeleeEquipment>();
            meleeScript.moveToDiscard = true;
            // Destroy(currentEquipmentMelee[i]);
        }
        if (currentStage != 10){
			currentStage = 2;
		}
		for (int i = 0; i < currentNumberOfAttacks; i++){
			if (currentNormalAttack + currentBluntAttack + currentPiercingAttack + currentPoisonttack > 0 ){
				DamageCalc(target: 1, damage: currentNormalAttack, blunt: currentBluntAttack, piercing: currentPiercingAttack, poison: currentPoisonttack);
			} else {
				DamageCalc(target: 1, damage: currentBrawl);
			}	
		}

      
    }

    public void playerLost ()
    {
        // SaveDataManager.saveData = new SaveData();
        // SaveDataManager.SaveGameData();
            System.IO.File.Delete(saveDataManager.path);
            //FileUtil.DeleteFileOrDirectory(SaveDataManager.path);
            // SaveDataManager.LoadGameData();
            gameManager.saveGameFound = false;

            levelManager.ChangeSceneTo(nyScene);
        
    }
}
