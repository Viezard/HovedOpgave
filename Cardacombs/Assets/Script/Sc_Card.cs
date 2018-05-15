using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card : MonoBehaviour {
	public int placementInHand; 
	public int cardID;
	private int apCost; 
	private float yPosition;

	public GameObject background;
	private GameObject[] _cards;
	private Sc_CardDataBase cardDataBase;
	private Sc_BattleManager battleManager; 


	

	void Start(){

		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		// Set the background of the card
		background = this.gameObject.transform.GetChild(0).gameObject;
		SpriteRenderer backgroundSR = background.GetComponent<SpriteRenderer>();
		if (cardID < 1000){
			 SO_CardMelee card = cardDataBase.FindMeleeCardByID(cardID);
			 apCost = card.apCost;
			 backgroundSR.sprite = card.image;
		} else if (cardID < 2000){
			 SO_CardArmor card = cardDataBase.FindArmorCardByID(cardID);
			 apCost = card.apCost;
			 backgroundSR.sprite = card.image;
		} else if (cardID < 3000){
			CardUtility card = cardDataBase.FindUtilityCardByID(cardID);
			apCost = card.apCost;
			backgroundSR.sprite = card.image;
		}

		yPosition = 0.5f;
		Sc_BattleManager.currentHandObjects.Add(this.gameObject); // Add it to the array which holds all card objects 
		placementInHand = Sc_BattleManager.currentHandObjects.Count; // Saves is place in the array 
		Vector3 newPosition = new Vector3 (2.2f * placementInHand, 2.2f, 0); // Just places it the right place
		this.transform.position = newPosition;
		// Get other components 
	}
	
	// Update is called once per frame
	void Update () {
		PositionCard();
	}

	void PositionCard() {
		
		if (IsEven(Sc_BattleManager.currentHandObjects.Count)){ // Is the number of cards in the hand odd or even 
			if (placementInHand < (Sc_BattleManager.currentHandObjects.Count /2 + 0.5)){ // Is this card located on the left or right side of the middle
				float PositionFromMiddle = (Sc_BattleManager.currentHandObjects.Count /2f) - placementInHand;
				float newXPosition = 7.5f - 2.2f * PositionFromMiddle - 1.1f;
				Vector3 newPosition = new Vector3 (newXPosition, yPosition, 0);
				this.transform.position = newPosition;
			} else { // if it was to the right 
				float PositionFromMiddle = placementInHand - (Sc_BattleManager.currentHandObjects.Count /2f) - 1;
				float newXPosition = 7.5f + 2.2f * PositionFromMiddle + 1.1f;
				Vector3 newPosition = new Vector3 (newXPosition, yPosition, 0);
				this.transform.position = newPosition;
			}
		} else { // If the number of cards or odd 
			if (placementInHand == (Sc_BattleManager.currentHandObjects.Count /2f + 0.5)){ // check if this is the middle card
				float newXPosition = 7.5f;
				Vector3 newPosition = new Vector3 (newXPosition, yPosition, 0);
				this.transform.position = newPosition;
			} else if (placementInHand < (Sc_BattleManager.currentHandObjects.Count /2 + 0.5)) { // check if the card is to the left of the middle 
				float PositionFromMiddle = (Sc_BattleManager.currentHandObjects.Count /2f) - 0.5f - placementInHand;
				float newXPosition = 7.5f - 2.2f * PositionFromMiddle - 2.2f;
				Vector3 newPosition = new Vector3 (newXPosition, yPosition, 0);
				this.transform.position = newPosition;
			} else if (placementInHand > (Sc_BattleManager.currentHandObjects.Count /2 + 0.5)) { // else if the card is to the right of the midle
				float PositionFromMiddle = placementInHand - 1.5f -  (Sc_BattleManager.currentHandObjects.Count /2f);
				float newXPosition = 7.5f + 2.2f * PositionFromMiddle + 2.2f;
				Vector3 newPosition = new Vector3 (newXPosition, yPosition, 0);
				this.transform.position = newPosition;
			}
		}
	}
	void OnMouseDown(){

		if (battleManager.currentStage == 1 && apCost + battleManager.currentApUsed <= battleManager.currentApMax){
		battleManager.PlayCard(cardID);
		battleManager.currentApUsed += apCost;
		DestroyCard();
		}
		
	}

	public bool IsEven (float number){ // checks if a float is even or odd 
		float newNumber = number / 2f;
		for (float i = newNumber; i > -3; i--){
			if (i == 0){
				return true;
			} else if (i < 0){
				return false;
			}
		}
		return false;
	}

	public void DestroyCard() {
		print(placementInHand - 1);
		Sc_BattleManager.currentHandObjects.RemoveAt(placementInHand - 1);
		_cards = GameObject.FindGameObjectsWithTag("Card");
		for (int i = 0; i < _cards.Length; i++){
			Sc_Card otherScript = _cards[i].GetComponent<Sc_Card>();
			if (otherScript.placementInHand > placementInHand){
				otherScript.placementInHand -= 1;
			}
		}
		Destroy(this.gameObject);
	}

}
