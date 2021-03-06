﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_NavigationManager : MonoBehaviour {

    public SaveDataManager saveDataManager;
    private Sc_EventDataBase eventDataBase;
	public Sc_GameManager gameManager;
	public Sc_LevelManager levelManager;
	public Sc_MonsterDataBase monsterManager;
	public int MonsterChance;
	public Text eventTitle;
	public Text lostCards;
	public Text fullCards;
	public GameObject fab_Event;
	public bool firstEvent;

	public Text eventDiscription;

	// Use this for initialization
	void Start () {
		firstEvent = true;
		eventDataBase = GameObject.FindObjectOfType<Sc_EventDataBase>();
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();
		monsterManager = GameObject.FindObjectOfType<Sc_MonsterDataBase>();
        saveDataManager = GameObject.FindObjectOfType<SaveDataManager>();
        saveDataManager.saveData.currentScene = "Navigation";
        MonsterChance = 6;
		EventPressed();
	}
	public void EventPressed (){
		print(gameManager.isLoading + " is the state of loading");
        if (gameManager.isLoading == true){
				print("Did load");
				loadEvent();
		} else {
			int isNextMonster = Random.Range(0,MonsterChance);
			if (firstEvent == true){
				firstEvent = false;
				isNextMonster = 1;
			}
			if (isNextMonster == 0){
				NextMonster();
			} else {
				MonsterChance -= 1;
				float tierTwoChance = 3/(-0.01f*gameManager.slayCount - 0.03f) +100;
				float random = Random.Range (0, 100);
				print ("is " + random + " higher then " +tierTwoChance);
				if (random > tierTwoChance){
					NextEvent(1);
				} else {
					NextEvent(2);
				}
			}
		}
        
    }
	public void NextMonster(){
		if(gameManager.slayCount == 0){
			gameManager.currentMonster = monsterManager.IntroMonster[0];
			levelManager.ChangeSceneTo("Battle");
		} else if (gameManager.slayCount == 1) {
			gameManager.currentMonster = monsterManager.TierOneMonster[0];
			levelManager.ChangeSceneTo("Battle");
		} else if (gameManager.slayCount == 2){
			gameManager.currentMonster = monsterManager.TierTwoMonster[0];
			levelManager.ChangeSceneTo("Battle");
		} else {
			levelManager.ChangeSceneTo("MainMenu");
		}
		
		
	}
	public void NextEvent(int tier){
		// Remove all events in scene 
		GameObject[] eventOptions = GameObject.FindGameObjectsWithTag("Event");
		for (int i = eventOptions.Length; i > 0; i--){
			print(i + "halloo" + eventOptions.Length);
			Destroy(eventOptions[i - 1]);
		}

		// Find and start new event 
     
        int newEventID = 0;
       
		if (tier == 1)
		{
			newEventID = FindTierOneEvent();
		}
		else if (tier == 2)
		{
			newEventID = FindTierTwoEvent();
		}

		gameManager.eventsDone.Add(newEventID);
		CallEvent(newEventID);
        
	}
	public void loadEvent (){
		GameObject[] eventOptions = GameObject.FindGameObjectsWithTag("Event");
		for (int i = eventOptions.Length; i > 0; i--){
			print(i + "halloo" + eventOptions.Length);
			Destroy(eventOptions[i - 1]);
		}

		// Find and start new event 
        CallEvent(saveDataManager.saveData.currentEventSave);
		gameManager.isLoading = false;
	}

	public int FindTierOneEvent () {
		for (int i = 1; i > 0; i++){
			int random = 0;
			random =Random.Range(0,eventDataBase.tierOneEvents.Length); 
			print(random);
			bool isNewEvent = true;
			for (int j= 0; j < gameManager.eventsDone.Count; j++){
				if (eventDataBase.tierOneEvents[random].eventID ==  gameManager.eventsDone[j]){ // Checks if the random event id is allready in the eventsDone list
					isNewEvent = false;
				}
			}	
			if (isNewEvent == true){ // if the event which was found is new 
				return eventDataBase.tierOneEvents[random].eventID;
			} 
			if (i == 100){ // A pack op if there is no events they have not allready done. 
				return eventDataBase.tierOneEvents[random].eventID;
			}
		}
		return 0;
	}

	public int FindTierTwoEvent () {
		for (int i = 1; i > 0; i++){
			int random = 0;
			random =Random.Range(0,eventDataBase.tierTwoEvents.Length); 
			bool isNewEvent = true;
			for (int j= 0; j < gameManager.eventsDone.Count; j++){
				if (eventDataBase.tierTwoEvents[random].eventID ==  gameManager.eventsDone[j]){ // Checks if the random event id is allready in the eventsDone list
					isNewEvent = false;
				}
			}	
			if (isNewEvent == true){ // if the event which was found is new 
				return eventDataBase.tierTwoEvents[random].eventID;
			} 
			if (i == 100){ // A pack op if there is no events they have not allready done.
				gameManager.eventsDone.Clear(); 
				return eventDataBase.tierTwoEvents[random].eventID;
			}
		}
		return 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager){
			gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		}
		fullCards.text = gameManager.fullDeck.Count + " Cards in Deck";
		lostCards.text = gameManager.lostCards.Count + " Cards Lost";
	}

	public void CallEvent(int id) {
		Events aEvent = eventDataBase.FindEventByID(id);

		eventTitle.text = aEvent.eventName;
		eventDiscription.text = aEvent.eventDecription;

		for (int i = 0; i < aEvent.numberOfEvents; i++){
			if (aEvent.numberOfEvents != 2){
				Vector3 newPostion = new Vector3 (transform.position.x - 2 + (i * 2), transform.position.y - 1, transform.position.z);
				GameObject newOption = (GameObject)Instantiate (fab_Event, newPostion, transform.rotation);
				Sc_Event newOptionScript =  newOption.GetComponent<Sc_Event>();
				newOptionScript.Eventid = id;
				newOptionScript.OptionNumber = i;
			} else {
				Vector3 newPostion = new Vector3 (transform.position.x - 1f + (i * 2f), transform.position.y - 1, transform.position.z);
				GameObject newOption = (GameObject)Instantiate (fab_Event, newPostion, transform.rotation);
				Sc_Event newOptionScript =  newOption.GetComponent<Sc_Event>();
				newOptionScript.Eventid = id;
				newOptionScript.OptionNumber = i;
			}
			
		}
		gameManager.currentEvent = id;
        saveDataManager.SaveGameData();
	}

	public void Restore (int restore){ // Add lost cards to the deck 
		for (int i = 0; i < restore; i ++){
			if (gameManager.lostCards.Count > 0){
				int random = Random.Range(0, gameManager.lostCards.Count);
				gameManager.fullDeck.Add(gameManager.lostCards[random]);
				gameManager.lostCards.RemoveAt(random);
			}
		}
	}

}
