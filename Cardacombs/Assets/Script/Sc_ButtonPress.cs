using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class Sc_ButtonPress : MonoBehaviour, IPointerDownHandler {
	public string nyScene; 
	private Sc_SoundManager soundManager; 
	private Sc_LevelManager levelManager;
    private SaveDataManager saveManager;
    public Button thisButton;
    public GameObject continueButton;
 
	
	// Use this for initialization
	void Start () {
		Button btn = thisButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
		soundManager = GameObject.FindObjectOfType<Sc_SoundManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();
        saveManager = GameObject.FindObjectOfType<SaveDataManager>();
        saveManager.LoadGameData();
        if (this.gameObject == GameObject.Find("Continue"))
        {
            if(saveManager.saveData.currentScene != null)
            {
                nyScene = saveManager.saveData.currentScene;
            }
            
        }
        
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	void TaskOnClick()
    {	
		StartCoroutine(ExecuteAfterTime(0.1f));
    }
	public void OnPointerDown(PointerEventData eventData)
    {	
        soundManager.clicked();
    }

	 IEnumerator ExecuteAfterTime(float time)
 	{
    	 yield return new WaitForSeconds(time);
        if ( nyScene == "Battle")
        {
            FileUtil.DeleteFileOrDirectory(saveManager.path);
            
        }
        else if (nyScene == "Navigation")
        {
            
            saveManager.saveData.currentScene = "Navigation";
        }
        levelManager.ChangeSceneTo(nyScene);
    
    }
    
}
