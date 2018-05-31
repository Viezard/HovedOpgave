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
    private GameObject DiscardPile;
    public float endPointDiscardX = 0;
    public float endPointDiscardY = 0;
    float timer = 0.0f;
    public bool moveToDiscard = false;

    void Awake()
	{	
		cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>(); 
		battleManager = GameObject.FindObjectOfType<Sc_BattleManager>(); 
		render = this.gameObject.GetComponent<SpriteRenderer>();
		defenceTextObject = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
		defenceText = defenceTextObject.GetComponent<TextMesh>();
       
    }
	void Start () {
        DiscardPile = GameObject.Find("DiscardPile");
        endPointDiscardX = DiscardPile.transform.position.x;
        endPointDiscardY = DiscardPile.transform.position.y;

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
		Vector3 newPosition = new Vector3 (0.8f + 0.8f * (placementInEquipent -1), -1.5f, 0); // Just places it the right place
		this.transform.position = newPosition;
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
        else
        {
            Vector3 newPosition = new Vector3(0.8f + 0.8f *  (placementInEquipent -1), -1.5f, 0);
            this.transform.position = newPosition;
        }
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
