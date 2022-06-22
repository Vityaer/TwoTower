using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListPlayersScript : MonoBehaviour{

	// public List<>
	public GameObject panelPrefab;
	public Transform placeForPlayers;
	public Button buttonReady;
	public List<PlayerRoomPanelScript> players = new List<PlayerRoomPanelScript>();
	public List<GameAccount> gameAccounts = new List<GameAccount>();
	public void CreatePlayer(GameAccount gameAccount){
		gameAccounts.Add(gameAccount);
		PlayerRoomPanelScript newPanel = Instantiate(panelPrefab, placeForPlayers).GetComponent<PlayerRoomPanelScript>();
		newPanel.SetGameAccount(gameAccount);
		if(gameAccount.isLocalPlayer == true){
			CommandCenterScript.Instance?.sender?.SendCommand(new NetworkCommand(gameAccount.ID,  TypeCommand.InfoAboutPlayer, gameAccount.userName));
		}
		players.Add(newPanel);
	}
	public void ChangeReady(int ID){
		players.Find(x => (x.gameAccount.ID == ID))?.ChangeReady();
		CheckRoom();
	}
	public void CheckRoom(){
		if(players.Find(x => (x.ready == false) ) == null){
			CommandCenterScript.Instance.SetPlayers(gameAccounts);
			MainMenuScript.Instance.GoToGame();
		}
	}
	private static ListPlayersScript instance;
	public static ListPlayersScript Instance{get => instance;}
	void Awake(){
		instance = this;
	}
	public void Clear(){
		for(int i=0; i<players.Count; i++){
			players[i].Clear();
		}
		gameAccounts.Clear();
	}
	public void SetName(int ID, string name){
		players.Find(x => (x.gameAccount.ID == ID))?.SetName(name);
	}
	public void SetRoom(Room room){
		Debug.Log(room.listPlayers.Count.ToString());
		for(int i=0; i < room.listPlayers.Count; i++){
			if(gameAccounts.Find(x => (x.ID == room.listPlayers[i].ID)) == null){
				CreatePlayer(room.listPlayers[i]);
			}
		}
	}
}
