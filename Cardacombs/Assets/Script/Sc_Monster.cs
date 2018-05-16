using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_Monster : MonoBehaviour {
	public int health;
	public int maxHealth;
	public Text healthText;
	public int defence;
	public Text defenceText;
	private Sc_BattleManager battleManager;

	// Use this for initialization
	void Start () {
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
		health = 8;
		maxHealth = 8;
		defence = 1;
		healthText.text = "" + health;
		defenceText.text = "" + defence;
	}

	public void MonsterTurn () {
		battleManager.DamageCalc(target: 1, damage: battleManager.currentSpiked);
		battleManager.DamageCalc(target: 0, damage: 2, poison: 0);
		battleManager.currentStage = 0;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
