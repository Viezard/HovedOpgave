using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card : MonoBehaviour {
	private float placementInHand; 
	public int cardID;

	public static List<GameObject> currentHandObjects = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		currentHandObjects.Add(this.gameObject);
		placementInHand = currentHandObjects.Count;
		Vector3 newPosition = new Vector3 (2.2f * placementInHand, 2.2f, 0);
		this.transform.position = newPosition;
		if (IsEven(placementInHand)){
			print("true");
		} else {
			print("false");
		}
	}
	
	// Update is called once per frame
	void Update () {
		PositionCard();
	}

	void PositionCard() {
		
		if (IsEven(currentHandObjects.Count)){ // Is the number of cards in the hand odd or even 
			if (placementInHand < (currentHandObjects.Count /2 + 0.5)){ // Is this card located on the left or right side of the middle
				float PositionFromMiddle = (currentHandObjects.Count /2f) - placementInHand;
				float newXPosition = 7.5f - 2.2f * PositionFromMiddle - 1.1f;
				Vector3 newPosition = new Vector3 (newXPosition, 2.2f, 0);
				this.transform.position = newPosition;
			} else { // if it was to the right 
				float PositionFromMiddle = placementInHand - (currentHandObjects.Count /2f) - 1;
				float newXPosition = 7.5f + 2.2f * PositionFromMiddle + 1.1f;
				Vector3 newPosition = new Vector3 (newXPosition, 2.2f, 0);
				this.transform.position = newPosition;
			}
		} else { // If the number of cards or odd 
			if (placementInHand == (currentHandObjects.Count /2f + 0.5)){ // check if this is the middle card
				float newXPosition = 7.5f;
				Vector3 newPosition = new Vector3 (newXPosition, 2.2f, 0);
				this.transform.position = newPosition;
			} else if (placementInHand < (currentHandObjects.Count /2 + 0.5)) { // check if the card is to the left of the middle 
				float PositionFromMiddle = (currentHandObjects.Count /2f) - 0.5f - placementInHand;
				float newXPosition = 7.5f - 2.2f * PositionFromMiddle - 2.2f;
				Vector3 newPosition = new Vector3 (newXPosition, 2.2f, 0);
				this.transform.position = newPosition;
			} else if (placementInHand > (currentHandObjects.Count /2 + 0.5)) { // else if the card is to the right of the midle
				float PositionFromMiddle = placementInHand - 1.5f -  (currentHandObjects.Count /2f);
				float newXPosition = 7.5f + 2.2f * PositionFromMiddle + 2.2f;
				Vector3 newPosition = new Vector3 (newXPosition, 2.2f, 0);
				this.transform.position = newPosition;
			}
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
}
