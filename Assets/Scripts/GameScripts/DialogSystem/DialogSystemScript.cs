using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogSystemScript : MonoBehaviour{
	[SerializeField] private Text textMessage;
	[SerializeField] private Canvas canvasDialog;
	List<Speech> listSpeechs = new List<Speech>();
	int currentSpeech = 0;
	Replica currentReplica;
	public void OpenDialog(Dialogue dialogue){
		this.listSpeechs = dialogue.getListSpeech;
		canvasDialog.enabled = true;
		NextLine();
	}
	Coroutine coroutineWriteMessage;
	public void NextLine(){
		if(coroutineWriteMessage != null){
			StopCoroutine(coroutineWriteMessage);
			coroutineWriteMessage = null;
		}
		if(!listSpeechs[currentSpeech].IsFinish){
			currentReplica = listSpeechs[currentSpeech].NextReplica ;
			coroutineWriteMessage = StartCoroutine( WriteMessage( currentReplica.text ) );
		}else{
			if( currentSpeech < (listSpeechs.Count - 1) ){
				currentSpeech++;
				NextLine();
			}else{
				FinishDialog();
			}
		}
	}
	private void FinishDialog(){
		canvasDialog.enabled = false;
	}
    IEnumerator WriteMessage(string Message){
    	currentReplica.OnStartMessage();
        string currentText = string.Empty;
        textMessage.text = currentText;
        char[] sentense = Message.ToCharArray();
        for(int i = 0; i < sentense.Length; i++){
        	currentText = string.Concat(currentText, sentense[i]);
            textMessage.text = currentText;
            yield return null;
        }
    	currentReplica.OnFinishMessage();
    }
    void Awake(){
    	instance = this;
    }
    private static DialogSystemScript instance;
    public static DialogSystemScript Instance{get => instance;}
}
