using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using SimpleNetwork;

public class CommandCenterScript : MonoBehaviour{

	Queue<NetworkCommand> commands = new Queue<NetworkCommand>();
	public GameObject playerPrefab;
	public bool isExistCommand = false;
	public List<GameAccount> players = new List<GameAccount>();
	public GameAccount LocalGameAccount;

	private List<GameObject> listPositionSpawn = new List<GameObject>();
	private ISenderMessage _sender;
	public ISenderMessage sender{
		get{
			if(_sender == null) 
			_sender = (GameObject.Find("ServerMenager") != null) ? GameObject.Find("ServerMenager").GetComponent<ServerScript>() as ISenderMessage : GameObject.Find("ClientMenager").GetComponent<ClientScript>() as ISenderMessage; 
			if(_sender == null) Debug.Log("не нашли отправителя");
			return _sender; 
		}
		set{_sender  = value;}
	}
	public FadeInOut fadeInOut;
	int k = 0;
	public void AddCommand(NetworkCommand newCommand){
		isExistCommand = true;
		commands.Enqueue(newCommand);
	}
	public void AddCommand(string message, GameAccount player = null){
		isExistCommand = true;
		try{
			commands.Enqueue(JsonUtility.FromJson<NetworkCommand>(message));
		}
		catch{
			Debug.Log(message);
		}
	}
	Vector3 dir;
	CommandSynchronizationState playerState;
	public void ExecuteCommand(){
		isExistCommand = false;
		NetworkCommand curentCommand = null;
		GameAccount player;
		while(commands.Count > 0){
			curentCommand = commands.Dequeue();
			if(curentCommand == null){Debug.Log("пустой приказ");}
			if(curentCommand != null){
				switch(curentCommand.type){
					case TypeCommand.Action:
						player = players.Find(x => x.ID == curentCommand.listID[0]);
						playerState = JsonUtility.FromJson<CommandSynchronizationState>(curentCommand.info);
						// player?.playerController.ActionWithItemInHand(playerState);
						break;	
					case TypeCommand.Interactive:
						player = players.Find(x => x.ID == curentCommand.listID[0]);
						break;
					case TypeCommand.ChatMessage:
		                ChatControllerScript.Instance.PrintMessage(curentCommand.info);
						break;
					case TypeCommand.PlayerChangeReady:
						ListPlayersScript.Instance.ChangeReady(curentCommand.listID[0]);
						break;
					case TypeCommand.StateRoom:
						Room room = JsonUtility.FromJson<Room>(curentCommand.info);
						ListPlayersScript.Instance.SetRoom(room);
						break;
					case TypeCommand.ChangeScene:
						string NameScene = curentCommand.info;
						Application.LoadLevel(NameScene);
						break;		
					case TypeCommand.CreatePrefabPlayers:
						for(int i = 0; i < players.Count; i++){
							players[i].CreatePlayer();
						}
						break;	
					case TypeCommand.SetID:
						LocalGameAccount = new GameAccount(curentCommand.listID[0]);
						LocalGameAccount.userName = MainMenuScript.Instance.inputFieldName.text;
						LocalGameAccount.isLocalPlayer = true;
				        ListPlayersScript.Instance.CreatePlayer(LocalGameAccount);
						break;
					case TypeCommand.InfoAboutPlayer:
						ListPlayersScript.Instance.SetName(curentCommand.listID[0], curentCommand.info);
						sender.SendCommand(new NetworkCommand(TypeCommand.StateRoom, JsonUtility.ToJson(new Room(ListPlayersScript.Instance.gameAccounts)) ) );
						break; 
					default:
						if(Console.Instance != null) {
				            Console.Instance.Print(curentCommand.info);
				        }else{
							Debug.Log(curentCommand.info);
				        }
						break;		
				}
			}
			
		}
	}

	private static CommandCenterScript instance;
	public  static CommandCenterScript Instance{get => instance;}
	void Update(){
		if(isExistCommand){
			ExecuteCommand();
		}
	}


	void Awake(){
		Application.runInBackground = true;
		if(instance == null){
	        DontDestroyOnLoad(this.gameObject);
			instance = this;
		}else{
			Debug.Log("CommandCenterScript twice");
			Destroy(this);
		}
	}



	public PlayerControllerScript InstantiatePlayer(){
		  if (listPositionSpawn.Count == 0)
            listPositionSpawn = new List<GameObject>(GameObject.FindGameObjectsWithTag("Respawn"));
		GameObject player = Instantiate(playerPrefab, listPositionSpawn[k].transform);
		k++;
		return player.GetComponent<PlayerControllerScript>();
	}
	public void SetPlayers(List<GameAccount> gameAccounts){
		players.Clear();
		gameAccounts.Sort(new PlayerID());
		for(int i=0; i<gameAccounts.Count; i++){
			players.Add((GameAccount) gameAccounts[i].Clone());

		}

	}
}


public class PlayerID : IComparer<GameAccount>{
    public int Compare(GameAccount g1, GameAccount g2){
        if (g1.ID < g2.ID)
            return 1;
        else if (g1.ID > g2.ID)
            return -1;
        else
            return 0;
    }
}  