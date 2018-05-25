using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckPilesDisplay : MonoBehaviour {

    [SerializeField]
    private int DeckType;
    private SpriteRenderer SpriteRenderer1;
    private SpriteRenderer SpriteRenderer2;
    private SpriteRenderer SpriteRenderer3;
    private Sc_BattleManager battleManager;

    

    public Sprite sprite; // Drag your first sprite here
    

    // Use this for initialization
    void Start () {

        battleManager = GameObject.FindObjectOfType<Sc_BattleManager>();
        SpriteRenderer1 = GameObject.Find("DeckPile").GetComponent<SpriteRenderer>();
        SpriteRenderer2 = GameObject.Find("BanishPile").GetComponent<SpriteRenderer>();
        SpriteRenderer3 = GameObject.Find("DiscardPile").GetComponent<SpriteRenderer>();

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
		
        if (DeckType == 1)
        {
           if (battleManager.currentDiscard.Capacity == 0)
            {
                //is card back display
            }
            if (battleManager.currentDiscard.Capacity > 0)
            {
                //is last card display
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
