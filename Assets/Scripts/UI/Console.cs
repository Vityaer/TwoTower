using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Console : MonoBehaviour{

	public Text textComponent;
	private static Console instance;
	public static Console Instance{get => instance;}
	void Awake(){
		instance = this;
	}
	public void Print(string message){
		Debug.Log(message);
		textComponent.text = string.Concat(textComponent.text, "\n", message);
	}

}
