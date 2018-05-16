using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_NavigationManager : MonoBehaviour {

	private Sc_EventDataBase eventDataBase;
	private Sc_GameManager gameManager;

	public Text eventTitle;
	public Text lostCards;
	public Text fullCards;
	public GameObject fab_Event;

	public Text eventDiscription;

	// Use this for initialization
	void Start () {
		eventDataBase = GameObject.FindObjectOfType<Sc_EventDataBase>();
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		CallEvent(1);
	}
	
	// Update is called once per frame
	void Update () {
		fullCards.text = gameManager.fullDeck.Count + " Cards in the fullDeck";
		lostCards.text = gameManager.lostCards.Count + " Cards has been lost";
	}

	public void CallEvent(int id) {
		Events aEvent = eventDataBase.FindEventByID(0);

		eventTitle.text = aEvent.eventName;
		eventDiscription.text = aEvent.eventDecription;

		for (int i = 0; i < aEvent.numberOfEvents; i++){
			
			Vector3 newPostion = new Vector3 (transform.position.x - 5 + (i * 5), transform.position.y - 1, transform.position.z);
			GameObject newOption = (GameObject)Instantiate (fab_Event, newPostion, transform.rotation);
			Sc_Event newOptionScript =  newOption.GetComponent<Sc_Event>();
			newOptionScript.Eventid = id;
			newOptionScript.OptionNumber = i;
			print("hollooo " + i);
		}
	}

	public void Restore (int restore){
		for (int i = 0; i < restore; i ++){
			if (gameManager.lostCards.Count > 0){
				int random = Random.Range(0, gameManager.lostCards.Count);
				gameManager.fullDeck.Add(gameManager.lostCards[random]);
				gameManager.lostCards.RemoveAt(random);
			}
		}
	}

}
