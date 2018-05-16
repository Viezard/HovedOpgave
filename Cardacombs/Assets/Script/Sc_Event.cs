using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Event : MonoBehaviour {
	private Sc_EventDataBase eventDataBase;
	private Sc_GameManager gameManger;
	public int Eventid;
	public int OptionNumber;
	public GameObject title;
	private Events currentEvent;
	public GameObject numberOfCards;
	// Use this for initialization
	void Start () {
		eventDataBase = GameObject.FindObjectOfType<Sc_EventDataBase>();
		gameManger = GameObject.FindObjectOfType<Sc_GameManager>();  
		currentEvent = eventDataBase.FindEventByID(Eventid);
		title = this.gameObject.transform.GetChild(0).gameObject;
		numberOfCards = this.gameObject.transform.GetChild(1).gameObject;

		if (OptionNumber == 0){
			TextMesh titleText = title.GetComponent<TextMesh>();
			titleText.text = currentEvent.decriptionFirstEvent;

			TextMesh numberOfCardsText = numberOfCards.GetComponent<TextMesh>();
			numberOfCardsText.text = currentEvent.cardsFirstEvent.Count + "";
		} else if (OptionNumber == 1){
			TextMesh titleText = title.GetComponent<TextMesh>();
			titleText.text = currentEvent.decriptionSecondEvent;

			TextMesh numberOfCardsText = numberOfCards.GetComponent<TextMesh>();
			numberOfCardsText.text = currentEvent.cardsSecondEvent.Count + "";
		} else if (OptionNumber == 2){
			TextMesh titleText = title.GetComponent<TextMesh>();
			titleText.text = currentEvent.decriptionThirdEvent;

			TextMesh numberOfCardsText = numberOfCards.GetComponent<TextMesh>();
			numberOfCardsText.text = currentEvent.cardsThirdEvent.Count + "";
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		if (OptionNumber == 0){
			for (int i = 0; i < currentEvent.cardsFirstEvent.Count; i++){
			gameManger.fullDeck.Add(currentEvent.cardsFirstEvent[i]);
			print("add " + currentEvent.cardsFirstEvent[i] + " to the deck");
			}
			currentEvent.FirstEventFunction();
		} else if (OptionNumber == 1){
			for (int i = 0; i < currentEvent.cardsSecondEvent.Count; i++){
			gameManger.fullDeck.Add(currentEvent.cardsSecondEvent[i]);
			print("add " + currentEvent.cardsSecondEvent[i] + " to the deck");
			}
			currentEvent.SecondEventFunction();
		} else if (OptionNumber == 2){
			for (int i = 0; i < currentEvent.cardsThirdEvent.Count; i++){
			gameManger.fullDeck.Add(currentEvent.cardsThirdEvent[i]);
			print("add " + currentEvent.cardsThirdEvent[i] + " to the deck");
			}
			currentEvent.ThirdEventFunction();
		}
	}
}
