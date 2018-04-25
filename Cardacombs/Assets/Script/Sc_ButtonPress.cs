using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sc_ButtonPress : MonoBehaviour, IPointerDownHandler {
	public string nyScene; 
	private Sc_SoundManager soundManager; 
	private Sc_LevelManager levelManager; 
	public Button thisButton;
	
	// Use this for initialization
	void Start () {
		Button btn = thisButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
		soundManager = GameObject.FindObjectOfType<Sc_SoundManager>();
		levelManager = GameObject.FindObjectOfType<Sc_LevelManager>();
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
		levelManager.ChangeSceneTo(nyScene);
 	}
}
