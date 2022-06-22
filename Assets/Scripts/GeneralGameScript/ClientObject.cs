using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

[System.Serializable]
public class ClientObject{
    
    public TcpClient tcpClient;
    private NetworkStream stream;
    public NetworkStream Stream{get => stream; set => stream = value;}
    public byte[] buffer = new byte[49152];
    public string message;
    public int BytesReceived; 
    public ClientObject(TcpClient tcpClient, int ID){
        this.tcpClient = tcpClient;
        this.stream = this.tcpClient.GetStream();
        gameAccount = new GameAccount(ID);
    }


    public GameAccount gameAccount;
}
