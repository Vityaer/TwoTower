using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour{
   private ISenderMessage _sender = null;
	public ISenderMessage sender{
        get{
            if(_sender == null) _sender = CommandCenterScript.Instance.sender;
            return _sender;
        }
        set {_sender = value;}
    }
	public bool isLocalPlayer = false;
	public int ID;
	public void SetID(int ID){
    	this.ID = ID;
    }
  
}
