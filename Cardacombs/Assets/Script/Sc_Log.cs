using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_Log : MonoBehaviour {
	Sc_BattleManager battleManager;
	public int placement;
	// Use this for initialization
	void Start () {
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
		Update ();
		placement = 0;
		for (int i = 0;  i < battleManager.logBook.Count; i ++){
			Sc_Log newScript =  battleManager.logBook[i].GetComponent<Sc_Log>();
			newScript.placement += 1;
		}
		Text thisText =  this.gameObject.GetComponent<UnityEngine.UI.Text>();
		battleManager.logBook.Add(thisText);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3 (950f, 500f - (placement * 30), 0f);
		this.transform.position = newPosition;
		if (placement >= 12){
			battleManager.logBook.RemoveAt(0);
			Destroy(this.gameObject);
		}
	}
}
