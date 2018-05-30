﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardUtility : ScriptableObject {

	public int id;
	public new string name;
	public Sprite image;
	public int apCost;
	[TextArea]
	public string decription;
	
	public abstract void PlayedFunction ();

}
