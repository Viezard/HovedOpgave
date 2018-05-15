using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveDataManager : MonoBehaviour
{
    string filename = "SaveData.json";
    string path;

    public SaveData saveData = new SaveData();

    // Use this for initialization
    void Start()
    {

        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            saveData.date = System.DateTime.Now.ToShortDateString();
            saveData.time = System.DateTime.Now.ToShortTimeString();
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
                string contents = System.IO.File.ReadAllText(path);
                SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(contents);
                saveData = wrapper.saveData;
                Debug.Log(saveData.date + ", " + saveData.time);
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







