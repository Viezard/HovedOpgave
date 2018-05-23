using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Event : MonoBehaviour {
	private Sc_EventDataBase eventDataBase;
	public Sc_GameManager gameManger;
	public Sc_NavigationManager navigationManager;
	public int Eventid;
	public int OptionNumber;
	public GameObject title;
	private Events currentEvent;
	public GameObject numberOfCards;
	// Use this for initialization
	void Start () {
		eventDataBase = GameObject.FindObjectOfType<Sc_EventDataBase>();
		gameManger = GameObject.FindObjectOfType<Sc_GameManager>();  
		navigationManager = GameObject.FindObjectOfType<Sc_NavigationManager>();  
		currentEvent = eventDataBase.FindEventByID(Eventid);
		title = this.gameObject.transform.GetChild(0).gameObject;
		numberOfCards = this.gameObject.transform.GetChild(1).gameObject;
		TextMesh titleText = title.GetComponent<TextMesh>();
		if (OptionNumber == 0){
			
			titleText.text = currentEvent.decriptionFirstEvent;

			TextMesh numberOfCardsText = numberOfCards.GetComponent<TextMesh>();
			numberOfCardsText.text = currentEvent.cardsFirstEvent.Count + "";
		} else if (OptionNumber == 1){
			titleText.text = currentEvent.decriptionSecondEvent;

			TextMesh numberOfCardsText = numberOfCards.GetComponent<TextMesh>();
			numberOfCardsText.text = currentEvent.cardsSecondEvent.Count + "";
		} else if (OptionNumber == 2){
			titleText.text = currentEvent.decriptionThirdEvent;

			TextMesh numberOfCardsText = numberOfCards.GetComponent<TextMesh>();
			numberOfCardsText.text = currentEvent.cardsThirdEvent.Count + "";
		}
		titleText.text.Replace("\\n","\n");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManger){
			gameManger = GameObject.FindObjectOfType<Sc_GameManager>();
		}
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
		navigationManager.EventPressed();
	}

	public void EventPressed(){

	}
}
