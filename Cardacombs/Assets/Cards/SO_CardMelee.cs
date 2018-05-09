using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Melee Card", menuName = "MeleeCard")]
public class SO_CardMelee: ScriptableObject{
	public int id;
	public new string name;
	public Sprite image;
	public int apCost;
	public int normalDamage;
	public int bluntDamage;
	public int poisonDamage;
	public int piercingDamage ;
	
}
