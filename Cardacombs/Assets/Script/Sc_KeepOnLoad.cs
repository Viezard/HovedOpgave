using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_KeepOnLoad : MonoBehaviour {

	static int exists = 0; 
	void Awake()	{
		GameObject.DontDestroyOnLoad(gameObject);
		if (exists == 0){
		} else {
			Destroy(gameObject);
		}
	}
}
