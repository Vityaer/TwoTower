using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections.Generic;


public class Server : MonoBehaviour{
    #region Public Variables
    [Header("Network")]
    public string ipAdress = "127.0.0.1";
    public int port = 54010;
    public float waitingMessagesFrequency = 2;
    #endregion

    #region  Private m_Variables
    private TcpListener m_Server = null;
    private List<Coroutine> m_ListenClientMsgsCoroutine = new List<Coroutine>();
    #endregion

    #region Delegate Variables
    protected Action OnServerStarted = null;    //Delegate triggered when server start
    protected Action OnServerClosed = null;     //Delegate triggered when server close
    protected Action OnClientConnected = null;  //Delegate triggered when the server stablish connection with client
    #endregion

    //My variable
    public int limitPlayers = 8; 
    public List<ClientObject> clients = new List<ClientObject>();
    protected GameAccount gameAccountServer;
    //Start server and wait for clients
    protected virtual void StartServer(){        
        //Set and enable Server 
        IPAddress ip = IPAddress.Parse(ipAdress);
        m_Server = new TcpListener(ip, port);
        m_Server.Start();
        ServerLog("Server Started", Color.green);
        //Wait for async client connection 
        StartListenNewConnect();
        OnServerStarted?.Invoke();
    }
    private void StartListenNewConnect(){
        Debug.Log("ждём новых подключений");
        if(clients.Count < limitPlayers){
            m_Server.BeginAcceptTcpClient(ClientConnected, null);
        }
    }

    public static bool isNewConnected = false;
    //Callback called when "BeginAcceptTcpClient" detects new client connection
    private TcpClient m_Client = null;
    private ClientObject newClient;
    private void ClientConnected(IAsyncResult res){
        //set the client reference
        m_Client = m_Server.EndAcceptTcpClient(res);
        Debug.Log("произошло подключение");
        newClient = new ClientObject(m_Client, clients.Count + 1); 
        clients.Add(newClient);
        isNewConnected = true;
        
        StartListenNewConnect();
        OnClientConnected?.Invoke();
    }
    void Update(){
        if(isNewConnected){
            CreateNewPlayerInRoom();
        }
    }
    void CreateNewPlayerInRoom(){
        Debug.Log("отправляем id");
        isNewConnected = false;
        ListPlayersScript.Instance.CreatePlayer(newClient.gameAccount);
        m_ListenClientMsgsCoroutine.Add( StartCoroutine(ListenClientMessages( newClient ) ) );
        Room room = new Room(ListPlayersScript.Instance.gameAccounts);
        SendCommandToClient(new NetworkCommand(TypeCommand.SetID, newClient.gameAccount), newClient); 
    }
    #region Communication Server<->Client
    //Coroutine waiting client messages while client is connected to the server
    private IEnumerator ListenClientMessages(ClientObject client){      

        if(client.Stream != null) StartListenAsync(client);
        while (client.Stream != null){   
            //If there is any msg, do something
            if (client.BytesReceived > 0){
                OnMessageReceived(client.message);
                client.BytesReceived = 0;
                StartListenAsync(client);
            }
            yield return null;
        }    
        //The communication is over
        //CloseClientConnection();
    }
    private void StartListenAsync(ClientObject client){
        client.Stream.BeginRead(client.buffer, 0, client.buffer.Length, MessageReceived,  client);
    }
    //What to do with the received message on server
    protected virtual void OnMessageReceived(string receivedMessage){
        CommandCenterScript.Instance.AddCommand(receivedMessage);
        switch (receivedMessage){
            case "Close":
                //In this case we send "Close" to shut down client
                SendMessageToClient("Close");
                //Close client connection
                CloseClientConnection();
                break;
            default:
                // ServerLog("Received message " + receivedMessage);
                break;
        }
    }

    //Send custom string msg to client
    protected void SendMessageToClient(string sendMsg){
        //early out if there is nothing connected     
        for(int i = 0; i < clients.Count; i++){
            if (clients[i].Stream == null){
                ServerLog("Socket Error: Start at least one client first", Color.red);
                return;
            }
            //Build message to client        
            byte[] msgOut = Encoding.UTF32.GetBytes(sendMsg); //Encode message as bytes
            //Start Sync Writing
            clients[i].Stream.Write(msgOut, 0, msgOut.Length);
        }  
        ServerLog("Msg sended to Client: " + "<b>" + sendMsg + "</b>", Color.blue);
    }
    protected void SendCommandToClient(NetworkCommand command, ClientObject client){
        string sendMsg = JsonUtility.ToJson(command);
        //early out if there is nothing connected     
        if (client.Stream == null){
            ServerLog("Socket Error: Start at least one client first", Color.red);
            return;
        }
        //Build message to client        
        byte[] msgOut = Encoding.UTF32.GetBytes(sendMsg); //Encode message as bytes
        //Start Sync Writing
        client.Stream.Write(msgOut, 0, msgOut.Length);
        ServerLog("Msg sended to Client: " + "<b>" + sendMsg + "</b>", Color.blue);
    }
    protected void SendCommandToAllClient(NetworkCommand command){
        string message = JsonUtility.ToJson(command);
         for(int i = 0; i < clients.Count; i++){
            if (clients[i].Stream == null){
                ServerLog("Socket Error: Start at least one client first", Color.red);
                return;
            }
            //Build message to client        
            byte[] msgOut = Encoding.UTF32.GetBytes(message); //Encode message as bytes
            //Start Sync Writing
            clients[i].Stream.Write(msgOut, 0, msgOut.Length);
        }  
    }

    //AsyncCallback called when "BeginRead" is ended, waiting the message response from client
    ClientObject client = null;
    private void MessageReceived(IAsyncResult result){
        client = clients.Find(x => x == result.AsyncState);
        if(client != null){
            if (result.IsCompleted && client.tcpClient.Connected){
                //build message received from client
                client.BytesReceived = client.Stream.EndRead(result); 
                client.message = Encoding.UTF32.GetString(client.buffer, 0, client.BytesReceived); //De-encode message as string
            }
        }
    }
    #endregion    

    #region Close Server/ClientConnection
    //Close client connection and disables the server
    protected virtual void CloseServer(){
        ServerLog("Server Closed", Color.red);
        //Close client connection
        for(int i = 0; i < clients.Count; i++){
            if (clients[i] != null){
                clients[i].Stream.Close();
                clients[i].Stream = null;
                clients[i].tcpClient.Close();
                clients[i] = null;
            }
        }
        clients.Clear();
        //Close server connection
        if (m_Server != null)
        {
            m_Server.Stop();
            m_Server = null;
        }

        OnServerClosed?.Invoke();
    }

    //Close connection with the client
    protected virtual void CloseClientConnection()
    {
        ServerLog("Close Connection with Client", Color.red);
        //Reset everything to defaults
        for(int i = 0; i < m_ListenClientMsgsCoroutine.Count; i++){
            StopCoroutine(m_ListenClientMsgsCoroutine[i]);
            m_ListenClientMsgsCoroutine[i] = null;
            clients[i].tcpClient.Close();
            clients[i] = null;
        }

        //Waiting to Accept a new Client
        StartListenNewConnect();
    }
    #endregion
   
    #region ServerLog
    //Custom Server Log - With Text Color
    protected virtual void ServerLog(string msg, Color color)
    {
    }
    //Custom Server Log - Without Text Color
    protected virtual void ServerLog(string msg)
    {
    }
    #endregion
    void OnApplicationPause(bool pauseStatus){
        CloseServer();
    }
}