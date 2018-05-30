using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Sc_LevelManager : MonoBehaviour {
	public AudioClip click;
   

    // Make sure it does not get destroied on level load 
    static int exists = 0; 
	void Awake()	{
		GameObject.DontDestroyOnLoad(gameObject);
       

        if (exists == 0){
			exists = 1;
		} else {
			Destroy(gameObject);
		}
	}
	public void clicked(){
		GetComponent<AudioSource>().Play();
	}
	public void ChangeSceneTo (string name){
        
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void quiting (string name){
	}
}
