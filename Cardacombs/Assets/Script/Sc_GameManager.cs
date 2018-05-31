using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_GameManager : MonoBehaviour {
	public Sc_MonsterDataBase monsterDataBase;
    public SaveDataManager saveDataManager;
    // Varibels used in the Battle section
    public List<int> fullDeck = new List<int>(); // All the cards which a part of the ones deck
	// [HideInInspector] public int currentMonster ; // The id of the monster the player is figtig at the moment
	// Varibels used in the Navigation section 
	public List<int> eventsDone = new List<int>(); // A list of all the events which the player allready have done
	public List<int> monsterDone = new List<int>(); // A list of all the monsters the player has defeated 
	public GameObject currentMonster;
	public int startingDefence = 0;
	public int utilityAP = 0;
	public int slayCount = 0; // The number of monsters slay 
	public static int isCreated = 0;
	public int currentEvent; // The ID of the current event 
	public static int buttonPress = 60;
	public List<int> lostCards = new List<int>(); // A list of all the cards which the player has lost
    public bool saveGameFound = false;
	public bool isLoading = false;
  

	void Awake()
	{
        
        if (isCreated == 0){
			GameObject.DontDestroyOnLoad(gameObject);
			isCreated = 1;
		} else {
			Destroy(this.gameObject);
			print("Destoy GameManager");
		}
	}
	// Use this for initialization
	void Start () {
		monsterDataBase = GameObject.FindObjectOfType<Sc_MonsterDataBase>();
    }
	public void load(){
		isLoading = true;
		saveDataManager = GameObject.FindObjectOfType<SaveDataManager>();
        saveDataManager.LoadGameData();
		 if(saveDataManager.saveData.currentMonsterSave == 0) {
            currentMonster = monsterDataBase.IntroMonster[0];
        } else if (saveDataManager.saveData.currentMonsterSave == 1) {
            currentMonster = monsterDataBase.TierOneMonster[0];
		} else if (saveDataManager.saveData.currentMonsterSave == 3) {
            currentMonster = monsterDataBase.TierTwoMonster[0];
        }
		if (saveDataManager.saveData.isNewGame == false)
        {
            saveGameFound = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
        
        if (monsterDataBase == null){
			monsterDataBase = GameObject.FindObjectOfType<Sc_MonsterDataBase>();
		}
		if (buttonPress > 0){
			buttonPress -=1;
		}
	}
	public static void click(){
		buttonPress = 0;
	}
    
}
