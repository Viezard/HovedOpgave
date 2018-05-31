using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;



public class Sc_ButtonPress : MonoBehaviour, IPointerDownHandler {
	public string nyScene; 
	private Sc_SoundManager soundManager; 
	private Sc_LevelManager levelManager;
    private Sc_GameManager gameManager;
    private SaveDataManager saveManager;


    private Scene scene;

    public Button thisButton;
 
	
	// Use this for initialization
	void Start () {
        scene = SceneManager.GetActiveScene();
        Button btn = thisButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
		soundManager = GameObject.FindObjectOfType<Sc_SoundManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();
        saveManager = GameObject.FindObjectOfType<SaveDataManager>();
        gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
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


        if(scene.name == "MainMenu")
        {
            if (this.gameObject.name == "New Game")

            {
                System.IO.File.Delete(saveManager.path);
                saveManager.LoadGameData();

                levelManager.ChangeSceneTo(nyScene);
            }
            else if (this.gameObject.name == "Continue")
            {
                saveManager.LoadGameData();
                gameManager.isLoading = true;
                levelManager.ChangeSceneTo(saveManager.saveData.currentScene);
            }
        }
        else
        {
            levelManager.ChangeSceneTo(nyScene);
        }




    }
}
