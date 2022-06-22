using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerRoomPanelScript : MonoBehaviour{

	public Color colorReady, colorNotReady;
	public Image panelPlayer;
	public Text textUserName;
	public void ChangeReady(){
		gameAccount.ready = !gameAccount.ready;
		panelPlayer.color = gameAccount.ready ? colorReady : colorNotReady;
		ListPlayersScript.Instance.CheckRoom();
	}
	public GameAccount gameAccount = new GameAccount(-1);	
	public void SetGameAccount(GameAccount gameAccount){
		this.gameAccount = gameAccount;
		textUserName.text = gameAccount.userName;
		gameObject.SetActive(true);
		if(gameAccount.isLocalPlayer){
			ListPlayersScript.Instance.buttonReady.onClick.RemoveAllListeners();
			ListPlayersScript.Instance.buttonReady.onClick.AddListener(ChangeReady);
			ListPlayersScript.Instance.buttonReady.onClick.AddListener(SendChangeReady);
		}
	}
	public void SetName(string name){
		gameAccount.userName = name;
		textUserName.text = gameAccount.userName;
	}
	public void SendChangeReady(){
    	CommandCenterScript.Instance.sender?.SendCommand(new NetworkCommand(TypeCommand.PlayerChangeReady, gameAccount));
	} 
	public void Clear(){
		gameAccount = new GameAccount(-1);
		gameObject.SetActive(false);
	}
	public bool ready{get => gameAccount.ready;}
}
