using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class MonsterClass : MonoBehaviour {
	public int monsterID;
	public new string name; 
	public int tier;
	public int health = 0;
	public int maxHealth = 0;
	[HideInInspector] public Text healthText;
	public int defence = 0;
	[HideInInspector] public Text defenceText;
	[HideInInspector] public int debuffed = 0;
	[HideInInspector] public int damage = 0;
	[HideInInspector] public int poison = 0;
	[HideInInspector] public int lifeDrain = 0;
	[HideInInspector] public int blunt = 0;
	[HideInInspector] public int piercing = 0;

	[HideInInspector] public Sc_BattleManager battleManager;

	// Use this for initialization
	void Start () {
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
		healthText = GameObject.Find("Monster'sHealth").GetComponent<Text>();
		defenceText = GameObject.Find("Monster'sDefence").GetComponent<Text>();  

		healthText.text = "" + health;
		defenceText.text = "" + defence;
		MonsterStart();
	}

	public abstract void MonsterTurn ();
	public abstract void MonsterStart ();
	// Update is called once per frame
	void Update () {
		
	}

	
}
