using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
namespace SimpleNetwork{
	

[System.Serializable]
	public class NetworkCommand{
		public TypeCommand type;
		public Vector3 dir;
		public string info;

		public NetworkCommand(string message){
			this.type = TypeCommand.ChatMessage;
			this.info = message; 
		}
		public NetworkCommand(TypeCommand type, Vector3 dir){
			this.type = type;
			this.dir  = dir; 
		}
		public NetworkCommand(Vector3 dir){
			this.type = TypeCommand.Move;
			this.dir  = dir; 
		}
		public NetworkCommand(){
			this.type = TypeCommand.Move;
			this.dir = new Vector3();
		}
	}

    public struct UdpState{
	    public UdpClient u;
	    public IPEndPoint e;
	}
	public enum TypeCommand{
		Move,
		Shoot,
		Death,
		ChatMessage,
		CreatePlayer
	}
}
