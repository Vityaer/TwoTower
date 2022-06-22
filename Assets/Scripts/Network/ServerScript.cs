using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleNetwork;

/// <summary>
/// This Server inheritated class acts like Server using UI elements like buttons and input fields.
/// </summary>
public class ServerScript : Server, ISenderMessage{
    [Header("UI References")]
    [SerializeField] private Button m_StartServerButton = null;
    [SerializeField] private Button m_SendToClientButton = null;
    [SerializeField] private InputField m_SendToClientInputField = null;
    [SerializeField] private Button m_CloseServerButton = null;
    [SerializeField] private Text m_ServerLogger = null;
    [Header("UI custom")]
    public Button m_StartGameButton = null;  
    //Set UI interactable properties
    protected virtual void Awake()
    {
        //Start Server
        m_StartServerButton.onClick.AddListener(StartServer);
    }

    //Start server and wait for clients
    protected override void StartServer()
    {
        DontDestroyOnLoad(this.gameObject);
        ClearRoom();
        base.StartServer();
        gameAccountServer = new GameAccount(0);
        gameAccountServer.isLocalPlayer = true;
        gameAccountServer.userName = MainMenuScript.Instance.inputFieldName.text;
        ListPlayersScript.Instance.CreatePlayer(gameAccountServer);
        CommandCenterScript.Instance.sender = this as ISenderMessage;
        //Set UI interactable properties
        m_SendToClientButton.onClick.AddListener(SendMessageToClient);
        OnClientConnected = () => { m_SendToClientButton.interactable = true; };
        //Close Server
        m_CloseServerButton.onClick.AddListener(CloseServer);
        m_StartGameButton.gameObject.SetActive(true);
        m_StartGameButton.onClick.AddListener(GoToLevel);
    }

    //Get input field text and send it to client
    private void SendMessageToClient()
    {
        string newMsg = m_SendToClientInputField.text;
        base.SendMessageToClient(JsonUtility.ToJson(new NetworkCommand( newMsg )));
    }

    //Close connection with the client
    protected override void CloseClientConnection()
    {
        base.CloseClientConnection();
    }

    //Close client connection and disables the server
    protected override void CloseServer()
    {
        base.CloseServer();
        if(m_CloseServerButton != null)
        m_CloseServerButton.onClick.RemoveListener(CloseServer);
        if(m_SendToClientButton != null)
        m_SendToClientButton.onClick.RemoveListener(SendMessageToClient);
    }

    //Custom Server Log
    #region ServerLog
    //With Text Color
    protected override void ServerLog(string msg)
    {
        base.ServerLog(msg);
        if(m_ServerLogger != null)
            m_ServerLogger.text += '\n' + "- " + msg;
    }
    //Without Text Color
    protected override void ServerLog(string msg, Color color)
    {
        base.ServerLog(msg, color);
        if(m_ServerLogger != null)
            m_ServerLogger.text += '\n' + "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">- " + msg + "</color>";
    }
    #endregion

    public void StartGame(){
        Debug.Log("start game");
        NetworkCommand createPlayers = new NetworkCommand(TypeCommand.CreatePrefabPlayers, clients);
        createPlayers.AddPlayer(gameAccountServer);
        SendCommandAllClient(createPlayers);
        CommandCenterScript.Instance.AddCommand(createPlayers);
    } 
    private void SendCommandAllClient(NetworkCommand command){
        base.SendCommandToAllClient( command );
    }
    public void SendCommand(NetworkCommand command){
        SendCommandAllClient(command);
    }
    public void GoToLevel(){
        NetworkCommand command = new NetworkCommand(TypeCommand.ChangeScene, "Level");
        SendCommandAllClient(command);
        CommandCenterScript.Instance.AddCommand(command);
    }
    private void ClearRoom(){
        OnClientConnected = null;
        if(m_SendToClientButton != null) m_SendToClientButton.onClick.RemoveAllListeners();
        if(m_ServerLogger != null) m_ServerLogger.text = "";
        ListPlayersScript.Instance.Clear();
    }
}