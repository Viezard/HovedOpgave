using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_NavigationManager : MonoBehaviour {

	private Sc_EventDataBase eventDataBase;
	public Sc_GameManager gameManager;
	public Sc_LevelManager levelManager;
	public Sc_MonsterDataBase monsterManager;
	public int MonsterChance;
	public Text eventTitle;
	public Text lostCards;
	public Text fullCards;
	public GameObject fab_Event;

	public Text eventDiscription;

	// Use this for initialization
	void Start () {
		eventDataBase = GameObject.FindObjectOfType<Sc_EventDataBase>();
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();
		monsterManager = GameObject.FindObjectOfType<Sc_MonsterDataBase>();
		MonsterChance = 6;
		EventPressed();
	}
	public void EventPressed (){
		int isNextMonster = Random.Range(0,MonsterChance);
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
	public void NextMonster(){
		gameManager.currentMonster = monsterManager.TierOneMonster[0];
		levelManager.ChangeSceneTo("Battle");
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
		if (tier == 1){
			newEventID = FindTierOneEvent();
		} else if (tier == 2){
			newEventID = FindTierTwoEvent();
		}
		gameManager.eventsDone.Add(newEventID);
		CallEvent(newEventID);
	}
	public int FindTierOneEvent () {
		for (int i = 1; i > 0; i++){
			int random = 0;
			random =Random.Range(0,eventDataBase.tierOneEvents.Length); 
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
		fullCards.text = gameManager.fullDeck.Count + " Cards in the fullDeck";
		lostCards.text = gameManager.lostCards.Count + " Cards has been lost";
	}

	public void CallEvent(int id) {
		Events aEvent = eventDataBase.FindEventByID(id);

		eventTitle.text = aEvent.eventName;
		eventDiscription.text = aEvent.eventDecription;

		for (int i = 0; i < aEvent.numberOfEvents; i++){
			if (aEvent.numberOfEvents != 2){
				Vector3 newPostion = new Vector3 (transform.position.x - 5 + (i * 5), transform.position.y - 1, transform.position.z);
				GameObject newOption = (GameObject)Instantiate (fab_Event, newPostion, transform.rotation);
				Sc_Event newOptionScript =  newOption.GetComponent<Sc_Event>();
				newOptionScript.Eventid = id;
				newOptionScript.OptionNumber = i;
				print("hollooo " + i);
			} else {
				Vector3 newPostion = new Vector3 (transform.position.x - 2.5f + (i * 5f), transform.position.y - 1, transform.position.z);
				GameObject newOption = (GameObject)Instantiate (fab_Event, newPostion, transform.rotation);
				Sc_Event newOptionScript =  newOption.GetComponent<Sc_Event>();
				newOptionScript.Eventid = id;
				newOptionScript.OptionNumber = i;
				print("hollooo " + i);
			}
			
		}
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
