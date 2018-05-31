using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ApIcon : MonoBehaviour {
	public Sc_BattleManager battleManager;
	public Sc_GameManager gameManager;
	public int placement;
	public bool isStandard = false;
	public bool isUtility = false;
	public bool isOneTime = false;
	private float modifier = 0.7f;
	public Sprite standard;
	public Sprite used;
	public Sprite utility;
	public Sprite oneTime;
	public SpriteRenderer spriteR;
	// Use this for initialization
	void Start () {
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		gameManager = GameObject.FindObjectOfType<Sc_GameManager>(); 
		spriteR = this.gameObject.GetComponent<SpriteRenderer>();
		battleManager.apObjects.Add(this.gameObject);
		placement = battleManager.apObjects.Count - 1;
		// set starting spirte
		if (isStandard){
			spriteR.sprite = standard;
		} else if (isUtility){
			spriteR.sprite = utility;
		} else if (oneTime){
			spriteR.sprite = oneTime;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager == null){
			gameManager = GameObject.FindObjectOfType<Sc_GameManager>(); 
		}
		float newX = (placement * modifier) - (battleManager.apObjects.Count + (-1 + ((battleManager.apObjects.Count -1)*(-0.5f + ((modifier -1)* 0.5f))))) - 0.5f;
		Vector3 newPosition = new Vector3 (newX, -3.2f, 5);
		this.gameObject.transform.position = newPosition;
		if (isStandard){
			if (placement >= (battleManager.currentApMax - battleManager.currentApUsed)){
				spriteR.sprite = used;
			} else if (spriteR.sprite != standard){
				spriteR.sprite = standard;
			}
		} else if (isUtility){
			
			if(placement - battleManager.currentApMax >= battleManager.currentUtilityAP ){
				print("spam");
				spriteR.sprite = used;
			}else if (spriteR.sprite != utility){
				spriteR.sprite = utility;
			}
		} else if (isOneTime){
			if (placement >= (battleManager.currentApMax - battleManager.currentApUsed + gameManager.utilityAP)){
				battleManager.apObjects.RemoveAt(placement);
				Destroy(this.gameObject);
			}
		}
	}
}
