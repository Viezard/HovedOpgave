using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Card : MonoBehaviour {
	public int placementInHand; 
	public int cardID;
	private int apCost; 
	private float yPosition;
	private float infoPositionX = 7.5f;
	private float infoPositionY = 5;
	public bool showingInfo = false;
	public static bool aCardIsShowingInfo = false;
	public float endPointX = 0;
	public float endPointY = 0;
    public float endPointDiscardX = 0;
    public float endPointDiscardY = 0;
    public float endPointBanishX = 0;
    public float endPointBanishY = 0;

    public GameObject background;
	private GameObject[] _cards;
	private Sc_CardDataBase cardDataBase;
	private Sc_BattleManager battleManager;
    

	public GameObject CardInfo;


	

	void Start(){
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
        endPointDiscardX = GameObject.Find("DiscardPile").transform.position.x;
        endPointDiscardY = GameObject.Find("DiscardPile").transform.position.y;
        Debug.Log(endPointDiscardX);
        Debug.Log(endPointDiscardY);

        endPointY = 0.5f;
		Sc_BattleManager.currentHandObjects.Add(this.gameObject); // Add it to the array which holds all card objects 
		placementInHand = Sc_BattleManager.currentHandObjects.Count; // Saves is place in the array 
        // Vector3 newPosition = new Vector3 (1.1f * placementInHand, 2.2f, 0); // Just places it the right place
        // this.transform.position = newPosition;


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
		} else if (cardID < 4000){
			CardCurse card = cardDataBase.FindCurseCardByID(cardID);
			apCost = card.apCost;
			backgroundSR.sprite = card.image;
		}

		
		// Get other components 
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)&& aCardIsShowingInfo == true){
			Sc_GameManager.click();
			if (showingInfo == true){
				CloseInfo();
			}
		}
		PositionCard();
		if ( cardID < 4000 && cardID >= 3000){
			OnMouseDown();
			battleManager.Draw();
		}
		
	}
	float timer = 0.0f;
	void PositionCard() {
		/// Moving the card 
	if (this.transform.position.x != endPointX || this.transform.position.y != endPointY){
		
		if (timer < 1){
			timer +=  0.05f;
			float newCurrentX = Mathf.Lerp(this.transform.position.x, endPointX, timer);
			float newCurrentY = Mathf.Lerp(this.transform.position.y, endPointY, timer);

            Vector3 newVector = new Vector3(newCurrentX, newCurrentY, placementInHand);
			transform.position = newVector;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0f,0f,0f), timer);

            } else {
			Vector3 newVector = new Vector3(endPointX, endPointY, placementInHand);
			transform.position = newVector;
			
		}
	} 
	else {
		timer = 0;
	}
		
	

		if (showingInfo == true){
			if (timer < 1){
				
				timer +=  0.05f;
				float newScale = Mathf.Lerp(8, 6, timer);
				Vector3 newVector = new Vector3(newScale, newScale, 1);
				transform.localScale = newVector;

				float newPostionX = Mathf.Lerp(this.gameObject.transform.position.x, infoPositionX, timer);
				float newPostionY = Mathf.Lerp(this.gameObject.transform.position.y, infoPositionY, timer);
				this.gameObject.transform.position = new Vector3(newPostionX, newPostionY, -5);
			}
		} else {
			if (this.transform.localScale.x !=6){
				timer +=  0.05f;
				float newScale = Mathf.Lerp(6, 8, timer);
			}
			if (IsEven(Sc_BattleManager.currentHandObjects.Count)){ // Is the number of cards in the hand odd or even 
				if (placementInHand < (Sc_BattleManager.currentHandObjects.Count /2 + 0.5)){ // Is this card located on the left or right side of the middle
					float PositionFromMiddle = (Sc_BattleManager.currentHandObjects.Count /2f) - placementInHand;
					endPointX = 7.5f - 1.1f * PositionFromMiddle - 0.6f;

				} else { // if it was to the right 
					float PositionFromMiddle = placementInHand - (Sc_BattleManager.currentHandObjects.Count /2f) - 1;
					endPointX = 7.5f + 1.1f * PositionFromMiddle + 0.6f;

				}
			} else { // If the number of cards or odd 
				if (placementInHand == (Sc_BattleManager.currentHandObjects.Count /2f + 0.5)){ // check if this is the middle card
					endPointX = 7.5f;
				} else if (placementInHand < (Sc_BattleManager.currentHandObjects.Count /2 + 0.5)) { // check if the card is to the left of the middle 
					float PositionFromMiddle = (Sc_BattleManager.currentHandObjects.Count /2f) - 0.5f - placementInHand;
					endPointX = 7.5f - 1.1f * PositionFromMiddle - 1.1f;
				} else if (placementInHand > (Sc_BattleManager.currentHandObjects.Count /2 + 0.5)) { // else if the card is to the right of the midle
					float PositionFromMiddle = placementInHand - 1.5f -  (Sc_BattleManager.currentHandObjects.Count /2f);
					endPointX = 7.5f + 1.1f * PositionFromMiddle + 1.1f;
				}
			}
		}
		
	}
	public void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1)&& Sc_GameManager.buttonPress == 0){
			//Sc_GameManager.click();
			//if (aCardIsShowingInfo == false){
			//	Info();
			//}
			print(endPointX);
		}
	}
	void OnMouseDown(){		
		if (aCardIsShowingInfo == false){
			if ((cardID < 1000 && battleManager.mayPlayMelee) || (cardID < 2000 && cardID >= 1000 && battleManager.mayPlayArmor)||(cardID < 3000 && cardID >= 2000 && battleManager.mayPlayUtility)|| cardID > 1999 ){
				if (cardID > 1999 && cardID < 3000 && battleManager.currentUtilityAP - apCost >= 0){ // Utility AP only 
					battleManager.PlayCard(cardID);
					battleManager.currentUtilityAP -= apCost;
					DestroyCard();
				} else	if (battleManager.currentStage == 1 && apCost + battleManager.currentApUsed <= battleManager.currentApMax){
					battleManager.PlayCard(cardID);
					battleManager.currentApUsed += apCost;
                    DestroyCard();
                    
				}
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
	public void Info () {
		timer = 0;
		showingInfo = true;
		aCardIsShowingInfo = true;
		GameObject Info = (GameObject)Instantiate (CardInfo, transform.position, transform.rotation);
		Sc_CardInfo InfoScript =  Info.GetComponent<Sc_CardInfo>();
		Info.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
		InfoScript.id = cardID;
		
	}
	public void CloseInfo(){
		timer = 0;
		showingInfo = false;
		aCardIsShowingInfo = false;
		Destroy(GameObject.FindGameObjectWithTag("CardInfo"));
	}
	public void DestroyCard() {
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


    public void DiscardCardTransform()
    {
        Sc_BattleManager.currentHandObjects.RemoveAt(placementInHand - 1);
        _cards = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < _cards.Length; i++)
        {
            Sc_Card otherScript = _cards[i].GetComponent<Sc_Card>();
            if (otherScript.placementInHand > placementInHand)
            {
                otherScript.placementInHand -= 1;
            }
        }
        if (this.transform.position.x != endPointDiscardX || this.transform.position.y != endPointDiscardY)
        {

            if (timer < 1)
            {
                timer += 0.5f;
                float newCurrentX = Mathf.Lerp(this.transform.position.x, endPointDiscardX, timer);
                float newCurrentY = Mathf.Lerp(this.transform.position.y, endPointDiscardY, timer);
                Vector3 newVector = new Vector3(newCurrentX, newCurrentY, placementInHand);
                transform.position = newVector;
                transform.localScale = new Vector3(4f, 4f, 0f);

            }
            else
            {
                Vector3 newVector = new Vector3(endPointDiscardX, endPointDiscardY, placementInHand);
                transform.position = newVector;
                transform.localScale = new Vector3(4f, 4f, 0f);

            }
        }
        else
        {
            timer = 0;
        }
        if (this.transform.position.x == endPointDiscardX && this.transform.position.y != endPointDiscardY)
        {
            Destroy(this.gameObject);
        }
    }
}
