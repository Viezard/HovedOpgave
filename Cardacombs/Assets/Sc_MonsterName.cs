using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MonsterName : MonoBehaviour {
	Sc_BattleManager battleManager;
	public bool hasName = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hasName == false){
			battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
			TextMesh text = this.GetComponent<TextMesh>();
			text.text = battleManager.monster.name;
			hasName = true;
		}
	}
}
