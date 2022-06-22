using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Replica{
	public List<DialogAction> beforeActions = new List<DialogAction>(); 
	public List<DialogAction>  afterActions = new List<DialogAction>(); 
	public string text;
	public void OnStartMessage(){
		for(int i = 0; i < beforeActions.Count; i++) 
			beforeActions[i].Start();
	}
	public void OnFinishMessage(){
		for(int i = 0; i < afterActions.Count; i++) 
			afterActions[i].Start();
	}
}