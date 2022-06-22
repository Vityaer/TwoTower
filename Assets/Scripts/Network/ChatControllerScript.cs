using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleNetwork;
public class ChatControllerScript : MonoBehaviour{
    public Text textChat;
        public void Awake(){
        	instance = this;
        }

        public void PrintMessage(string message){
            textChat.text = string.Concat(textChat.text, "\n", message);
            message = string.Empty; 
        }
        public static void PrintError(string errorText){
            instance?.PrintMessage(errorText);
        }
        private static ChatControllerScript instance;
        public static ChatControllerScript Instance{get => instance;}
}
