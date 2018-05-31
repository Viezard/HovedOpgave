using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_background : MonoBehaviour {
	public bool isCreated = false;
	void Awake()
	{
        
        if (isCreated == false){
			GameObject.DontDestroyOnLoad(gameObject);
			isCreated = true;
		} else {
			Destroy(this.gameObject);
			print("Destoy GameManager");
		}
	}
}
