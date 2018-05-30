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
	public int monsterStage = 0;
	[HideInInspector] public int chanceToMiss = 0;
	[HideInInspector] public int chanceToHitSelf = 0;
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
	public Sprite[] monsterCards;
	public GameObject card;
	public bool doStuff = true;

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
	public int FindTarget () {
		// Does it attack 
		int random = Random.Range(0, 100);
		if (random > chanceToMiss){
			random = Random.Range(0, 100);
			if (random > chanceToHitSelf){
				return 0;
			} else {
				return 1;
			}
		} else {
			return -2;
		}
	}
	public void UpdateText(){
		healthText.text = "" + health;
		defenceText.text = "" + defence;
	}
	public void PlayingCard(int sprite){
		Vector3 position = new Vector3 (this.transform.position.x , 1400, -1);
		GameObject newCard = (GameObject)Instantiate (card, position, transform.rotation);
		Sc_MonsterCard newScript = newCard.GetComponent<Sc_MonsterCard>();
		newScript.monster = this;
		Image backgroundSR = newCard.GetComponent<Image>();
		backgroundSR.sprite = monsterCards[sprite];
	}
}
