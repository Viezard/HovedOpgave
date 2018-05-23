using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_DefenceEquipment : MonoBehaviour {

	public int placementInEquipent; 
	public int id;
	public int defence;
	public GameObject defenceTextObject;
	public TextMesh defenceText;
	private Sc_CardDataBase cardDataBase;
	private Sc_BattleManager battleManager;
	private SpriteRenderer render;
	public SO_CardArmor card;

	void Awake()
	{	
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		render = this.gameObject.GetComponent<SpriteRenderer>();
		defenceTextObject = this.gameObject.transform.GetChild(0).gameObject;
		defenceText = defenceTextObject.GetComponent<TextMesh>();
	}
	void Start () {

		if (id < 2000){
			card = cardDataBase.FindArmorCardByID(id);
		}
		render.sprite = card.image;
		if (id != -10){
			defence = card.armorBonus;
		} else {
			defence = battleManager.currentToughness;
		}
		
		
		defenceText.text = defence + "";
		battleManager.currentSpiked += card.spickedBonus;
		battleManager.currentBrawl += card.brawl;

		battleManager.currentEquipmentArmor.Add(this.gameObject); // Add it to the array which holds all card objects 
		placementInEquipent = battleManager.currentEquipmentArmor.Count; // Saves is place in the array 
		Vector3 newPosition = new Vector3 (7.9f + 0.8f * placementInEquipent, 2.8f, 0); // Just places it the right place
		this.transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3 (7.9f + 0.8f * placementInEquipent, 2.8f, 0);
		this.transform.position = newPosition;
	}
	public void UpdateText(){
		defenceText.text = defence + "";
	}
	public void UpdateToughness(){
		defence = battleManager.currentToughness;
		defenceText.text = defence + "";
	}


	void OnDestroy()
	{
		battleManager.currentSpiked -= card.spickedBonus;
		battleManager.currentBrawl -= card.brawl;
	}
}
