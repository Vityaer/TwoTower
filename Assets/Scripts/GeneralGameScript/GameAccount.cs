using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleNetwork;

[System.Serializable]
public class GameAccount{
    //General info
    public string userName;
    public bool ready = false;
    public int ID;
    private PlayerControllerScript _playerController;
    public PlayerControllerScript playerController{get => _playerController; set => _playerController =value;}
    private bool _isLocalPlayer = false; 
    public bool isLocalPlayer{get => _isLocalPlayer; set => _isLocalPlayer = value;} 
    ISenderMessage sender;
    public GameAccount(int id){
        ID = id;
    }
    public GameAccount(ISenderMessage sender, int id){
        ID = id;
        this.sender = sender;
    }
    public void CreatePlayer(){
        playerController = CommandCenterScript.Instance.InstantiatePlayer();
        if(isLocalPlayer == true){
            playerController.isLocalPlayer = true;
            playerController.sender = sender;
            playerController.SetID(ID);
        }
    }

    public void SendAction(){

    }
    public GameAccount(){}
    public object Clone(){
        return new GameAccount{
                userName = this.userName,
                ready = this.ready,
                ID = this.ID,
                playerController = this.playerController,
                isLocalPlayer = this.isLocalPlayer,
                sender = this.sender
        };
    }
}
