using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Events : ScriptableObject {
	public int eventID;
	public int numberOfEvents;
	public string eventName;
	public string eventDecription;
	[TextArea]
	public string decriptionFirstEvent;
	public abstract void FirstEventFunction ();
	public List<int> cardsFirstEvent = new List<int>();
	[TextArea]
	public string decriptionSecondEvent;
	public abstract void SecondEventFunction ();
	public List<int> cardsSecondEvent = new List<int>();
	[TextArea]
	public string decriptionThirdEvent;
	public abstract void ThirdEventFunction ();
	public List<int> cardsThirdEvent = new List<int>();

}
