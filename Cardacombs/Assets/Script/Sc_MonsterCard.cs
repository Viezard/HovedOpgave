using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sc_MonsterCard : MonoBehaviour {
	private float positionX = 400f;
	private float positionY = 640;
	private float currentScale = 1;
	private float SetScale = 4; 
	float timer = 0.0f;
	private bool destroyOnDone = false;
	public MonsterClass monster;
	public string monsterName = "bite";
	public string monsterDiscription = "Deals 2 Damage";
	// Use this for initialization
	void Start () {
		monster.monsterStage +=1;
		this.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
		GameObject discription  = this.gameObject.transform.GetChild(3).gameObject;
		Text discriptionText = discription.GetComponent<UnityEngine.UI.Text>();
		discriptionText.text = monsterDiscription;
		GameObject name  = this.gameObject.transform.GetChild(4).gameObject;
		Text nameText = name.GetComponent<UnityEngine.UI.Text>();
		nameText.text = monsterName;
	}
	
	// Update is called once per frame
	void Update () {
		PositionCard();
		if(Input.GetMouseButtonDown(0)){
			positionY = 1400;
			destroyOnDone = true;
			currentScale = 1;
			SetScale = 0.25f;

			monster.doStuff = true;
		}
	}

	
	void PositionCard() {
		if (timer < 1 && (this.gameObject.transform.position.x != positionX || this.gameObject.transform.position.y != positionY)){
			timer +=  0.01f;

			float newScale = Mathf.Lerp(currentScale, SetScale, timer);
			Vector3 newVector = new Vector3(newScale, newScale, 1);
			transform.localScale = newVector;

			float newPostionX = Mathf.Lerp(this.gameObject.transform.position.x, positionX, timer);
			float newPostionY = Mathf.Lerp(this.gameObject.transform.position.y, positionY, timer);
			this.gameObject.transform.position = new Vector3(newPostionX, newPostionY, -5);
		} else {
			timer = 0;
			this.gameObject.transform.position = new Vector3(positionX, positionY, -5);
			if (destroyOnDone == true){
				Destroy(this.gameObject);
			}
		}
	}
}
