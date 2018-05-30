using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveDataManager : MonoBehaviour
{
    string filename = "SaveData.json";
    public string path;
    public SaveData saveData = new SaveData();

    public Sc_BattleManager battleManager;
    public Sc_GameManager gameManager;
    public Sc_NavigationManager navManager;
    

    public Sc_Card cardInHand;
    public Sc_Card cardInSave;
    public Sc_MeleeEquipment meleeEquipmentSave;
    public Sc_DefenceEquipment armorEquipmentSave;
    public GameObject effectSave;
    public GameObject card;
    private Scene scene;

    
    
    // Use this for initialization
    // Make sure it does not get destroied on level load 
    static int saveDataManagerexists = 0;
    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);


        if (saveDataManagerexists == 0)
        {
            saveDataManagerexists = 1;
        }
        else
        {
            Destroy(gameObject);
        }
        path = Application.persistentDataPath + "/" + filename;
        LoadGameData();
    }
    void Start()
    {

        
        Debug.Log(path);
        

    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
        gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
        navManager = GameObject.FindObjectOfType<Sc_NavigationManager>();
        
        

        if (Input.GetKeyDown(KeyCode.T) == true)
        {
            
           
            
        }
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            
     

            SaveGameData();
        }

        if (Input.GetKeyDown(KeyCode.L) == true)
        {
            LoadGameData();
        }
    }

    public void SaveGameData()
    {
        
        

        saveData.isNewGame = false;

        if (scene.name == "Battle")
        {
            saveData.currentDeckSave = battleManager.currentDeck;
            saveData.currentDiscardSave = battleManager.currentDiscard;
            saveData.currentBanishedSave = battleManager.currentBanished;
            saveData.currentStageSave = battleManager.currentStage;
            saveData.currentApUsedSave = battleManager.currentApUsed;
            saveData.currentUtilityAPSave = battleManager.currentUtilityAP;
            saveData.currentApMaxSave = battleManager.currentApMax;
            saveData.currentMonsterSave = battleManager.monster.monsterID;


            saveData.currentHandObjectsSave.Clear();
            for (int i = 0; i < Sc_BattleManager.currentHandObjects.Count; i++)
            {

                cardInHand = Sc_BattleManager.currentHandObjects[i].GetComponent<Sc_Card>();
                Debug.Log(cardInHand.cardID);
                saveData.currentHandObjectsSave.Add(cardInHand.cardID);

            }

            saveData.currentEquipmentArmorSave.Clear();
            for (int i = 0; i < battleManager.currentEquipmentArmor.Count; i++)
            {

                armorEquipmentSave = battleManager.currentEquipmentArmor[i].GetComponent<Sc_DefenceEquipment>();
                Debug.Log(armorEquipmentSave.id);
                saveData.currentEquipmentArmorSave.Add(armorEquipmentSave.id);

            }
            saveData.currentEquipmentMeleeSave.Clear();
            for (int i = 0; i < battleManager.currentEquipmentMelee.Count; i++)
            {

                meleeEquipmentSave = battleManager.currentEquipmentMelee[i].GetComponent<Sc_MeleeEquipment>();
                Debug.Log(meleeEquipmentSave.id);
                saveData.currentEquipmentMeleeSave.Add(meleeEquipmentSave.id);

            }

            saveData.currentEffectsSave.Clear();
            for (int i = 0; i < battleManager.currentEffects.Count; i++)
            {

                effectSave = battleManager.currentEffects[i];
                Debug.Log(effectSave);
                saveData.currentEffectsSave.Add(effectSave);

            }


        }

        if (scene.name == "Navigation" || scene.name == "Battle")
        {
            saveData.eventsDoneSave = gameManager.eventsDone;
            saveData.monsterDoneSave = gameManager.monsterDone;
            saveData.fullDeckSave = gameManager.fullDeck;
            
            
            saveData.slayCountSave = gameManager.slayCount;
            saveData.lostCardsSave = gameManager.lostCards;
        }
        if (scene.name == "Navigation")
        {
            saveData.firstEventSave = navManager.firstEvent;
            saveData.currentEventSave = gameManager.currentEvent;
        }

        SaveDataWrapper wrapper = new SaveDataWrapper();
        wrapper.saveData = saveData;
        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(path, contents);
       
    }

    public void LoadGameData()
    {
        path = Application.persistentDataPath + "/" + filename;

        try
        {
            Debug.Log("File Path" + System.IO.File.Exists(path));
            if(System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
                SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(contents);
                saveData = wrapper.saveData;

                gameManager.saveGameFound = true;

                if (scene.name == "Battle")
                {
                    battleManager.currentDeck = saveData.currentDeckSave;
                    battleManager.currentDiscard = saveData.currentDiscardSave;
                    battleManager.currentBanished = saveData.currentBanishedSave;
                    battleManager.currentEffects = saveData.currentEffectsSave;
                    battleManager.currentStage = saveData.currentStageSave;
                    battleManager.currentApUsed = saveData.currentApUsedSave;
                    battleManager.currentUtilityAP = saveData.currentUtilityAPSave;
                    battleManager.currentApMax = saveData.currentApMaxSave;


/*
                    for (int i = 0; i < saveData.currentHandObjectsSave.Count; i++)
                    {

                        GameObject newCard = (GameObject)Instantiate(card, new Vector3(10.55f, 2.21f, 3), Quaternion.Euler(0f, 180f, 90f));
                        Sc_Card newCardScript = newCard.GetComponent<Sc_Card>();
                        newCardScript.cardID = saveData.currentHandObjectsSave[i];

                    }
                    */

                }

                if (scene.name == "Navigation" || scene.name == "Battle")
                {
                    gameManager.eventsDone = saveData.eventsDoneSave;
                    gameManager.monsterDone = saveData.monsterDoneSave;

                    gameManager.fullDeck = saveData.fullDeckSave;


                    gameManager.slayCount = saveData.slayCountSave;
                    gameManager.lostCards = saveData.lostCardsSave;
                }

                if (scene.name == "Navigation")
                {
                    navManager.firstEvent = saveData.firstEventSave;
                    gameManager.eventsDone = saveData.eventsDoneSave;


                    // gameManager.currentMonster = saveData.currentMonsterSave;
                    gameManager.currentEvent = saveData.currentEventSave;

                }
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist");
                saveData = new SaveData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }



            
    }
        

        
}
 








