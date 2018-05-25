using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MeleeEquipment : MonoBehaviour {
	public int placementInEquipent; 
	public int id;
	private Sc_CardDataBase cardDataBase;
	private Sc_BattleManager battleManager;
	public SO_CardMelee card;
	// Use this for initialization
	void Start () {
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		SpriteRenderer render = this.gameObject.GetComponent<SpriteRenderer>();
		
		if (id < 1000){
			card = cardDataBase.FindMeleeCardByID(id);
		}
		render.sprite = card.image;
		battleManager.currentEquipmentMelee.Add(this.gameObject); // Add it to the array which holds all card objects 
		placementInEquipent = battleManager.currentEquipmentMelee.Count; // Saves is place in the array 
		Vector3 newPosition = new Vector3 (7.1f - 0.8f * placementInEquipent, 2.8f, 0); // Just places it the right place
		this.transform.position = newPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
