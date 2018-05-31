using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_Monster : MonoBehaviour {
	public GameObject[] monsterdatabase;
	public Sc_GameManager gameManager;
	public int monsterID;
	// Use this for initialization
	void Awake () {
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>();
		
	}

	public GameObject FindMonsterByID(int id){
		for (int i = 0; i < monsterdatabase.Length;i++){
			GameObject newmonster = monsterdatabase[i];
			MonsterClass newmonsterScript =  newmonster.GetComponent<MonsterClass>();
			if (newmonsterScript.monsterID == id){
				return newmonster;
			}
		}
		return monsterdatabase[1];
	}
	// Update is called once per frame

}
