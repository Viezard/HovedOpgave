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

	public void TakeDamage(int damage, int type){ // type 0 = normal, 1 = blunt, 2 = piercing. 
		if (type == 0){
			for(int i = 0; i < damage; i++){
				if (defence > 0){
					defence -=1;
				} else if (health > 0) {
					health -=1;
				} else {
					print ("You Won!!!");
				}
			}
		} else if (type == 1){
			for(int i = 0; i < damage; i++){
				if (defence > 0){
					defence -=1;
				} else {
					print ("No more Armor");
				}
			}
		} else if (type == 2){
			for(int i = 0; i < damage; i++){
				if (health > 0) {
					health -=1;
				} else {
					print ("You Won!!!");
				}
			}
		}
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
