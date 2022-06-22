using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue{
	[SerializeField] private List<Speech> russianSpeechs = new List<Speech>();
	[SerializeField] private List<Speech> englishSpeechs = new List<Speech>();
	private int currentSpeech = 0;
	private bool isFinish = false;
	public List<Speech> getListSpeech{get => russianSpeechs;}

	private void GetDialogueOnLanguage(ref List<Speech> workList, int LanguageNumber){
		switch(LanguageNumber){
			case 0: 
				workList = russianSpeechs;
				break;
			case 1:
				workList = englishSpeechs;
				break;
			default:
				workList = englishSpeechs;
				break;
		}
	}
	public bool IsFinish{get => isFinish;}
}