using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveDataManager : MonoBehaviour
{
    string filename = "SaveData.json";
    string path;

    public Sc_BattleManager battleManager;
    public Sc_GameManager gameManager;
    public Sc_NavigationManager navManager;

    public SaveData saveData = new SaveData();

    // Use this for initialization
    void Start()
    {
        battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
        gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
        navManager = GameObject.FindObjectOfType<Sc_NavigationManager>();
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            saveData.currentDeckSave = battleManager.currentDeck;
            
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
            if (System.IO.File.Exists(path) == true)
            {
                /*string contents = System.IO.File.ReadAllText(path);
                SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(contents);
                saveData = wrapper.saveData;*/
                
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist");
            }
        }
        catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
        
    }
}







