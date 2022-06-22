using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour{

	public InputField inputFieldName;
	public Button btnServerCreate, btnConnectToServer;

	public void ChangeName(){
		if(inputFieldName.text.Length > 0){
			btnServerCreate.interactable = true;
			btnConnectToServer.interactable = true;
		}else{
			btnServerCreate.interactable = false;
			btnConnectToServer.interactable = false;
		}	
	}
	public void GoToGame(){
		FadeInOut.Instance.EndScene("SelectLevel");
	}
	public void ExitGame(){
		 Application.Quit();
	}
	public void ClearGame(){SaveLoadScript.ClearFile();}
	private static MainMenuScript instance; 
	public static MainMenuScript Instance{get => instance;} 
	void Awake(){
		instance = this;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	} 
}
