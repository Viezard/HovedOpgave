using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveDataManager : MonoBehaviour
{
    string filename = "SaveData.json";
    public string path;

    public Sc_BattleManager battleManager;
    public Sc_GameManager gameManager;
    public Sc_NavigationManager navManager;
    public Sc_Card cardInHand;
    public Sc_Card cardInSave;
    public Sc_MeleeEquipment meleeEquipmentSave;
    public Sc_DefenceEquipment armorEquipmentSave;
    public GameObject effectSave;
    public GameObject card;

    public SaveData saveData = new SaveData();
    private bool battleManagerExist = false;
    private bool gameManagerExist = false;
    private bool navManagerExist = false;
    // Use this for initialization
    void Start()
    {
        battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
        gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
        navManager = GameObject.FindObjectOfType<Sc_NavigationManager>();
        

        if (battleManager != null)
        {
            battleManagerExist = true;
        }
        if (gameManager != null)
        {
            gameManagerExist = true;
        }
        if (navManager != null)
        {
            navManagerExist = true;
        }
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);

        
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.T) == true)
        {
            
           
            
        }
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            // Debug.Log(battleManager.currentHandObjects);
            if( battleManagerExist == true)
            {
                saveData.currentDeckSave = battleManager.currentDeck;
                saveData.currentDiscardSave = battleManager.currentDiscard;
                saveData.currentBanishedSave = battleManager.currentBanished;
                saveData.currentStageSave = battleManager.currentStage;
                saveData.currentApUsedSave = battleManager.currentApUsed;
                saveData.currentUtilityAPSave = battleManager.currentUtilityAP;
                saveData.currentApMaxSave = battleManager.currentApMax;
                

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

            if (gameManagerExist == true)
            {
                saveData.eventsDoneSave = gameManager.eventsDone;
                saveData.monsterDoneSave = gameManager.monsterDone;
                saveData.fullDeckSave = gameManager.fullDeck;
                saveData.currentMonsterSave = gameManager.currentMonster;
                saveData.currentEventSave = gameManager.currentEvent;
                saveData.slayCountSave = gameManager.slayCount;
                saveData.lostCardsSave = gameManager.lostCards;
            }
            if (navManagerExist == true)
            {
                saveData.firstEventSave = navManager.firstEvent;
            }

            SaveGameData();
        }

        if (Input.GetKeyDown(KeyCode.L) == true)
        {
            LoadGameData();
        }
    }

    public void SaveGameData()
    {
        SaveDataWrapper wrapper = new SaveDataWrapper();
        wrapper.saveData = saveData;
        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(path, contents);
    }

    public void LoadGameData()
    {

        try
        {
            if (System.IO.File.Exists(path))
            {
                gameManager.saveGameFound = true;
                string contents = System.IO.File.ReadAllText(path);
                SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(contents);
                saveData = wrapper.saveData;
                if (battleManagerExist == true)
                {
                    battleManager.currentDeck = saveData.currentDeckSave;
                    battleManager.currentDiscard = saveData.currentDiscardSave;
                    battleManager.currentBanished = saveData.currentBanishedSave;
                    battleManager.currentEffects = saveData.currentEffectsSave;
                    battleManager.currentStage = saveData.currentStageSave;
                    battleManager.currentApUsed = saveData.currentApUsedSave;
                    battleManager.currentUtilityAP = saveData.currentUtilityAPSave;
                    battleManager.currentApMax = saveData.currentApMaxSave;
                    

                    /*for (int i = 0; i < saveData.currentHandObjectsSave.Count; i++)
                    {

                        GameObject newCard = (GameObject)Instantiate(card, new Vector3(10.55f, 2.21f, 3), Quaternion.Euler(0f, 180f, 90f));
                        Sc_Card newCardScript = newCard.GetComponent<Sc_Card>();
                        newCardScript.cardID = saveData.currentHandObjectsSave[i];


                        // Sc_BattleManager.currentHandObjects[i] = saveData.currentHandObjectsSave[i];
                    }*/
                }
            

                if (gameManagerExist == true)
                {
                    gameManager.eventsDone = saveData.eventsDoneSave;
                    gameManager.monsterDone = saveData.monsterDoneSave;
                    gameManager.fullDeck = saveData.fullDeckSave;
                    gameManager.currentMonster = saveData.currentMonsterSave;
                    gameManager.currentEvent = saveData.currentEventSave;
                    gameManager.slayCount = saveData.slayCountSave;
                    gameManager.lostCards = saveData.lostCardsSave;
                }
                if (navManagerExist == true)
                {
                    navManager.firstEvent = saveData.firstEventSave;
                }
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist");
                saveData = new SaveData();
            }
        }
        catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

        
    }
 
}







