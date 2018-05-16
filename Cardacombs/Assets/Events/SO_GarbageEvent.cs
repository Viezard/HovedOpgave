using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Garbage Event", menuName = "event/Garbagevent")]
public class SO_GarbageEvent : Events {
	public int firstNumberRestore = 0;
	public int secondNumberRestore = 0;
	public int thirdNumberRestore = 0;
	
	public override void FirstEventFunction (){
		if (firstNumberRestore != 0){
			Sc_NavigationManager navigationManager = GameObject.FindObjectOfType<Sc_NavigationManager>();
			navigationManager.Restore(firstNumberRestore);
		}
	}
	public override void SecondEventFunction (){
		if (secondNumberRestore != 0){
			Sc_NavigationManager navigationManager = GameObject.FindObjectOfType<Sc_NavigationManager>();
			navigationManager.Restore(secondNumberRestore);
		}
	}
	public override void ThirdEventFunction (){
		if (thirdNumberRestore != 0){
			Sc_NavigationManager navigationManager = GameObject.FindObjectOfType<Sc_NavigationManager>();
			navigationManager.Restore(thirdNumberRestore);
		}
	}
}
