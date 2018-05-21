using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Armor Card", menuName = "ArmorCard")]
public class SO_CardArmor : ScriptableObject {
	public int id;
	public new string name;
	public Sprite image;
	public int apCost;
	public int armorBonus;
	public int spickedBonus;
	public int brawl;

}
