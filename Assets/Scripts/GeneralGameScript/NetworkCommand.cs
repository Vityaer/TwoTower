using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class NetworkCommand{
	public TypeCommand type = TypeCommand.ChatMessage;
	public string info;
	public List<int> listID = new List<int>();
	public NetworkCommand(int ID, TypeCommand type){
		this.type = type;
		SetPlayer(ID);
	}
	public NetworkCommand(TypeCommand type, List<ClientObject> clients){
		this.type = type;
		for(int i=0; i < clients.Count; i++) listID.Add(clients[i].gameAccount.ID);
	}
	public NetworkCommand(string message){
		this.type = TypeCommand.ChatMessage;
		this.info = message; 
	}
	public NetworkCommand(TypeCommand type){
		this.type = type;
	}
	public NetworkCommand(TypeCommand type, string info){
		this.type = type;
		this.info = info;
	}
	public NetworkCommand(int ID, TypeCommand type, string info){
		this.type = type;
		this.info = info;
		SetPlayer(ID);
	}
	public NetworkCommand(TypeCommand type, GameAccount account){
		this.type = type;
		AddPlayer(account);
	}
	public void AddPlayer(GameAccount account){ listID.Add(account.ID); }
	public void AddPlayer(int ID){ listID.Add(ID); }
	public void SetPlayer(int ID){ listID.Clear(); listID.Add(ID);}
}

public enum TypeCommand{
	Move = 0,
	Action = 1,
	Death = 2,
	ChatMessage = 3 ,
	CreatePlayers = 4,
	SetID = 5 ,
	SynchronizationState =6,
	ChangeItemInHand =7,
	Interactive =8,
	ItemChangePositionBox = 9,
	ItemDrop = 10,
	PlayerChangeReady =11,
	AddPlayerRoom = 12,
	RemovePlayerRoom = 13, 
	StateRoom = 14,
	ChangeScene = 15,
	InfoAboutPlayer = 16,
	CreatePrefabPlayers = 17,
	CreateBonus = 18,
	KillMonster = 19
}


//Classes command
	public class CommandSynchronizationState{
		public Vector3    dirMove;
		public Vector3    pos;
		public Quaternion dirFace;
		public CommandSynchronizationState(){}
		public CommandSynchronizationState(Vector3 pos, Vector3 dirMove, Quaternion dirFace){
			this.pos     = pos;
			this.dirMove = dirMove;
			this.dirFace = dirFace;
		}
		public void SetData(Vector3 pos, Vector3 dirMove, Quaternion dirFace){
			this.pos     = pos;
			this.dirMove = dirMove;
			this.dirFace = dirFace;
		}
	}
	public class CommandItemChangePositionBox{
		public int num;
		public int IDHome, IDBox;
		public bool isPickUp = true;
		public CommandItemChangePositionBox(int num, int IDHome, int IDBox, bool isPickUp){
			this.num      = num;
			this.IDHome   = IDHome;
			this.IDBox    = IDBox;
			this.isPickUp = isPickUp;
		}
	}
	public class Room{
		public List<GameAccount> listPlayers = new List<GameAccount>();
		public Room(List<GameAccount> list){
			listPlayers = list;
		} 
	}
