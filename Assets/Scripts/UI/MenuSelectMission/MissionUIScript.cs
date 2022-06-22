using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUIScript : MonoBehaviour{
	[SerializeField] private Text nameMission, descriptionMission;
	[SerializeField] private Image imageMission;
	[SerializeField] private RewardScript jewerlyReward,silkReward;
	[SerializeField] private Canvas canvas;
	private Mission workLevel;
	public void ShowDataAboutMission(Mission mission){
		canvas.enabled = true;
		Debug.Log("open mission");
        workLevel = mission;
		nameMission.text = mission.name;
		descriptionMission.text = mission.description;
		imageMission.sprite = mission.imageTerritory;
		jewerlyReward.SetData(mission.reward.GetResource(TypeResource.Jewerly));
		silkReward.SetData(mission.reward.GetResource(TypeResource.Silk));
	}
	public void ClosePanel(){
		Debug.Log("close mission");
		canvas.enabled = false;
	}
	public void PlayMission(){
    	Information.Instance.level = workLevel;
    	FadeInOut.Instance.EndScene("Level");
	}
	void Awake(){ instance = this; }
	private static MissionUIScript instance;
	public static MissionUIScript Instance{get => instance;}
}