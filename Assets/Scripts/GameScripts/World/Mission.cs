using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Mission", menuName = "Custom ScriptableObject/Create Mission", order = 51)]
[System.Serializable]
public class Mission : ScriptableObject{
	public string name;
	public string description;
	public Sprite imageTerritory;
    public BaronName baron{get => hero.Name;}
	public int ID = -1;
	public Hero hero;
	public TypeTerrain typeTerrain;
	public LevelDifficultyMap levelDifficultyMap;
	public ProgressMission progress = ProgressMission.Close;
	public ListResource reward;

	public void OnOpen()  {if(actionOnOpen)  DialogSystemScript.Instance.OpenDialog(beforeOpenDialogue);}
	public void OnFinish(){if(actionOnFinish)  DialogSystemScript.Instance.OpenDialog(afterFinishDialogue);}
	public void Open(){ progress = ProgressMission.Open;}
	public void Finish(){ progress = ProgressMission.Finish; }
	public bool IsFinish{get => progress == ProgressMission.Finish;}
	private bool actionOnOpen{get => (beforeOpenDialogue.getListSpeech.Count > 0);}
	[Header("Before open")]
	public Dialogue beforeOpenDialogue;
	private bool actionOnFinish{get => (afterFinishDialogue.getListSpeech.Count > 0);} 
	[Header("After finish")]
	public Dialogue afterFinishDialogue;
	public Mission Clone(){
		return new Mission{
			hero = this.hero,
			ID = this.ID,
			typeTerrain = this.typeTerrain,
			progress = this.progress,
			reward  = this.reward,
			beforeOpenDialogue = this.beforeOpenDialogue,
			afterFinishDialogue = this.afterFinishDialogue
		};
	}
}
public enum LevelDifficultyMap{
	Easy,
	Medium,
	Hard,
	Impossible
}