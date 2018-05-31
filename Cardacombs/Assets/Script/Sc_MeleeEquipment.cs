using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MeleeEquipment : MonoBehaviour {
	public int placementInEquipent; 
	public int id;
	private Sc_CardDataBase cardDataBase;
	private Sc_BattleManager battleManager;
    private GameObject DiscardPile; 
	public SO_CardMelee card;
    public float endPointDiscardX = 0;
    public float endPointDiscardY = 0;
    float timer = 0.0f;
    public bool moveToDiscard = false;
    public Sprite greenBanner;
	public Sprite grayBanner;
	public Sprite yellowBanner;
    // Use this for initialization
    void Start () {
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		SpriteRenderer render = this.gameObject.GetComponent<SpriteRenderer>();
        DiscardPile = GameObject.Find("DiscardPile");
        endPointDiscardX = DiscardPile.transform.position.x;
        endPointDiscardY = DiscardPile.transform.position.y;


        if (id < 1000){
			card = cardDataBase.FindMeleeCardByID(id);
		}
		render.sprite = card.image;
		battleManager.currentEquipmentMelee.Add(this.gameObject); // Add it to the array which holds all card objects 
		placementInEquipent = battleManager.currentEquipmentMelee.Count; // Saves is place in the array 
		Vector3 newPosition = new Vector3 (-2f - 0.8f * (placementInEquipent - 1), -1.5f, 0); // Just places it the right place
		this.transform.position = newPosition;

		// Set the left Icon 
		GameObject leftIconText = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
		TextMesh leftIconTextR = leftIconText.GetComponent<TextMesh>();
		leftIconTextR.text = card.normalDamage + ""; 
		// Set the left bonus one icon 
		bool isset = false;
		if (card.poisonDamage > 0){
			isset = true;
			SpriteRenderer leftBonusIconOneR = this.gameObject.transform.transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>();
			leftBonusIconOneR.sprite = greenBanner;
			GameObject leftBonusIconOneText =this.gameObject.transform.transform.GetChild(0).GetChild(1).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
			TextMesh leftBonusIconOneTextR = leftBonusIconOneText.GetComponent<TextMesh>();
			leftBonusIconOneTextR.text = card.poisonDamage + ""; 
		} else if (card.bluntDamage > 0){
			isset = true;
			SpriteRenderer leftBonusIconOneR = this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>();
			leftBonusIconOneR.sprite = grayBanner;
			GameObject leftBonusIconOneText = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject;
			TextMesh leftBonusIconOneTextR = leftBonusIconOneText.GetComponent<TextMesh>();
			leftBonusIconOneTextR.text = card.bluntDamage + ""; 
		} else {
			Destroy (this.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject);
		}
		// Set the left bonus two icon
		if (card.bluntDamage > 0 && isset == false ){
			isset = true;
			SpriteRenderer leftBonusIconOneR = this.gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>();
			leftBonusIconOneR.sprite = grayBanner;
			GameObject leftBonusIconOneText = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
			TextMesh leftBonusIconOneTextR = leftBonusIconOneText.GetComponent<TextMesh>();
			leftBonusIconOneTextR.text = card.bluntDamage + ""; 
		} else {
			Destroy (this.gameObject.transform.transform.GetChild(0).GetChild(2).gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {

        if (moveToDiscard == true)
        {
            EquipmentMoved();
            // if (this.transform.position.x == endPointDiscardX && this.transform.position.y != endPointDiscardY)
            // {
               // EquipmentDestroy();
            // }
        }
        
	}


    public void EquipmentMoved()
    {
        if (this.transform.position.x != endPointDiscardX || this.transform.position.y != endPointDiscardY)
        {

            if (timer < 1)
            {
                timer += 0.5f;
                float newCurrentX = Mathf.Lerp(this.transform.position.x, endPointDiscardX, timer);
                float newCurrentY = Mathf.Lerp(this.transform.position.y, endPointDiscardY, timer);
                Vector3 newVector = new Vector3(newCurrentX, newCurrentY, 0);
                transform.position = newVector;
                

            }
            else
            {
                Vector3 newVector = new Vector3(endPointDiscardX, endPointDiscardY, 0);
                transform.position = newVector;
                

            }
        }
        else
        {
            timer = 0;
            EquipmentDestroy();
           
        }
       
    }


    public void EquipmentDestroy()
    {
        Destroy(this.gameObject);
    }
}
