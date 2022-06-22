using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Client inheritated class acts like Client using UI elements like buttons and input fields.
/// </summary>
public class ClientScript : Client, ISenderMessage{
    [Header("UI References")]
    [SerializeField] private Button m_StartClientButton = null;
    [SerializeField] private Button m_SendToServerButton = null;
    [SerializeField] private InputField m_SendToServerInputField = null;
    [SerializeField] private Button m_SendCloseButton = null;
    [SerializeField] private Text m_ClientLogger = null;

    //Set UI interactable properties
    private void Awake()
    {
        //Start Client
        m_StartClientButton.onClick.AddListener(StartClient);


        //Populate Client delegates
        OnClientStarted = () =>
        {
            //Set UI interactable properties        
            m_SendToServerButton.interactable = true;
            m_SendToServerButton.onClick.AddListener(SendMessageToServer);
            m_StartClientButton.interactable = false;
        };

        OnClientClosed = () =>
        {
            //Set UI interactable properties        
            m_StartClientButton.interactable = true;
            m_SendToServerButton.onClick.RemoveListener(SendMessageToServer);
            m_SendToServerButton.interactable = false;
        };
    }
    private void StartClient(){
        DontDestroyOnLoad(this.gameObject);
        CommandCenterScript.Instance.sender = this as ISenderMessage;
        ClearRoom();
        base.StartClient();
        m_SendCloseButton.onClick.AddListener(SendCloseToServer);
    }
    private void SendMessageToServer()
    {
        string newMsg = "";
        if(m_SendToServerInputField != null)
        newMsg = m_SendToServerInputField.text;
        base.SendMessageToServer(JsonUtility.ToJson(new NetworkCommand( newMsg )));
    }

    private void SendCloseToServer()
    {
        base.SendMessageToServer("Close");
        m_SendCloseButton?.onClick.RemoveListener(SendCloseToServer);
        //Set UI interactable properties        
    }

    //Custom Client Log
    #region ClientLog
    protected override void ClientLog(string msg, Color color)
    {
        base.ClientLog(msg, color);
        if(m_ClientLogger != null)
            m_ClientLogger.text += '\n' + "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">- " + msg + "</color>";
    }
    protected override void ClientLog(string msg)
    {
        base.ClientLog(msg);
         if(m_ClientLogger != null)
        m_ClientLogger.text += '\n' + "- " + msg;
    }
    #endregion
    private void ClearRoom(){
        m_SendToServerButton.onClick.RemoveAllListeners();
         if(m_ClientLogger != null)
        m_ClientLogger.text = "";
        ListPlayersScript.Instance.Clear();
    }
     //API
    public void SendCommand(NetworkCommand command){
        base.SendMessageToServer( JsonUtility.ToJson(command) );
    }

}