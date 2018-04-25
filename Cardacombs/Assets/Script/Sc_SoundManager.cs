using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_SoundManager : MonoBehaviour {
	public AudioClip click; 
	// Make sure it does not get destroied on level load 
	static int exists = 0; 
	void Awake()	{
		GameObject.DontDestroyOnLoad(gameObject);

		if (exists == 0){
			exists = 1;
			AudioSource background = GetComponent<AudioSource>();
			background.loop = true;
        	background.Play();
			print("Woken up before you gog go");
		} else {
			Destroy(gameObject);
			print("destroyed Level Manager");
		}
	}

	public void clicked(){
		Vector3 newvector = new Vector3 (0f, 0f, -10f);
		AudioSource.PlayClipAtPoint (click, newvector);
	}
}
