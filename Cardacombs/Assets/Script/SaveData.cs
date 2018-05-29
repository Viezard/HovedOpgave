using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SaveData {

    public bool isNewGame = true;
    public List<int> currentDeckSave = new List<int>(); // The cards you have in your Deck
    public List<int> currentHandObjectsSave = new List<int>(); // holdes the cards in your hand 
    public List<int> currentDiscardSave = new List<int>(); // The cards you have in your discard
    public List<int> currentBanishedSave = new List<int>(); // The cards lost in this battle
    public List<GameObject> currentEffectsSave = new List<GameObject>(); // The effects currently in effect and how many turns they have left
    public List<int> currentEquipmentArmorSave = new List<int>(); // The current equpment cards the player has in play
    public List<int> currentEquipmentMeleeSave = new List<int>(); // The current equpment cards the player has in play
    public int currentStageSave; // Which stage your in 0 = effect and draw, 1 = Play cards, 2 = monster turn 
    public int currentApUsedSave; // how much has been used
    public int currentUtilityAPSave;
    public int currentApMaxSave; // What is the max ap for this turn  
    public List<int> eventsDoneSave = new List<int>(); // A list of all the events which the player allready have done
    public List<int> monsterDoneSave = new List<int>(); // A list of all the monsters the player has defeated 
    public List<int> fullDeckSave = new List<int>();
    public GameObject currentMonsterSave;
    public int currentEventSave; // The ID of the current event 
    public int slayCountSave; // The number of monsters slay 
    public List<int> lostCardsSave = new List<int>(); // A list of all the cards which the player has lost
    public bool firstEventSave;
    public string currentScene;





    // Save Data 

}
