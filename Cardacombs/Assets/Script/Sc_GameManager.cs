﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour {
	// Varibels used in the Battle section
	public List<int> fullDeck = new List<int>(); // All the cards which a part of the ones deck
	

	// Varibels used in the Navigation section 
	public List<int> eventsDone = new List<int>(); // A list of all the events which the player allready have done
	public List<int> monsterDone = new List<int>(); // A list of all the monsters the player has defeated 
	public int slayCount; // The number of monsters slay 
	public int currentEvent; // The ID of the current event 
	public List<int> lostCards = new List<int>(); // A list of all the cards which the player has lost


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    void SaveGame()
    {
        // saves all information that is need to recreate the current point of the game 
        // Player hp, current deck, current discard, current banish, current hand, current hand size, current buffs and debuffs.
        // Slay Count, Current boss, current scene, current choice progress, Boss hp, boss buff debuff, 
    }
}
