using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class DeckPilesDisplay : MonoBehaviour {

    [SerializeField]
    private int DeckType;
    private SpriteRenderer SpriteRenderer1;
    private SpriteRenderer SpriteRenderer2;
    private SpriteRenderer SpriteRenderer3;
    private Sc_BattleManager battleManager;
    private Sc_CardDataBase cardDataBase;
    private int cardDiscardID;
    private int cardBanishID;

    public Sprite sprite; // Drag your first sprite here
    

    // Use this for initialization
    void Start () {
        cardDataBase = GameObject.FindObjectOfType<Sc_CardDataBase>();
        battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
        SpriteRenderer1 = GameObject.Find("DeckPile").GetComponent<SpriteRenderer>();
        SpriteRenderer3 = GameObject.Find("BanishPile").GetComponent<SpriteRenderer>();
        SpriteRenderer2 = GameObject.Find("DiscardPile").GetComponent<SpriteRenderer>();
        
        
          
        
        {

        }
        if (DeckType == 0)
        {
            SpriteRenderer1.sprite = sprite;
        }
        else if (DeckType == 1)
        {
            SpriteRenderer2.sprite = sprite;
        }
        else if (DeckType == 2)
        {
            SpriteRenderer3.sprite = sprite;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(battleManager.currentDiscard.Count != 0)
        {
            cardDiscardID = battleManager.currentDiscard[battleManager.currentDiscard.Count - 1];
        }
        if (battleManager.currentBanished.Count != 0)
        {
            cardBanishID = battleManager.currentBanished[battleManager.currentBanished.Count - 1];
        }

        if (DeckType == 1)
        {
           if (battleManager.currentDiscard.Capacity == 0)
            {
                //is card back display
                // Set the background of the card
                
              
            }

            if (battleManager.currentDiscard.Capacity > 0)
            {
                //is last card display
                if (cardDiscardID < 1000)
                {
                    SO_CardMelee card = cardDataBase.FindMeleeCardByID(cardDiscardID);

                    SpriteRenderer2.sprite = card.image;
                }
                else if (cardDiscardID < 2000)
                {
                    SO_CardArmor card = cardDataBase.FindArmorCardByID(cardDiscardID);

                    SpriteRenderer2.sprite = card.image;
                }
                else if (cardDiscardID < 3000)
                {
                    CardUtility card = cardDataBase.FindUtilityCardByID(cardDiscardID);

                    SpriteRenderer2.sprite = card.image;
                }

            }
        }
        else if (DeckType == 2)
        {
            if (battleManager.currentBanished.Capacity == 0)
            {
                //is card back display

            }
            if (battleManager.currentBanished.Capacity > 0)
            {
                //is last card display
                if (cardBanishID < 1000)
                {
                    SO_CardMelee card = cardDataBase.FindMeleeCardByID(cardBanishID);

                    SpriteRenderer3.sprite = card.image;
                }
                else if (cardBanishID < 2000)
                {
                    SO_CardArmor card = cardDataBase.FindArmorCardByID(cardBanishID);

                    SpriteRenderer3.sprite = card.image;
                }
                else if (cardBanishID < 3000)
                {
                    CardUtility card = cardDataBase.FindUtilityCardByID(cardBanishID);

                    SpriteRenderer3.sprite = card.image;
                }
            }
        }
    }

    public void DeckVisuals()
    {

    }

    public void BanishVisuals()
    {

    }

    public void DiscardVisuals()
    {

    }
}
