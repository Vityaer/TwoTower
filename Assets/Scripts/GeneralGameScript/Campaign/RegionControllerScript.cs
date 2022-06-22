using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class RegionControllerScript : MonoBehaviour{
	public Region region;
	public int ID{get => region.ID;}
	public List<MissionControllerScript> missions = new List<MissionControllerScript>();
	[SerializeField] private Image mainTerritory; 
	[SerializeField] private List<BorderScript> borders = new List<BorderScript>(); 
	[SerializeField] private Baron baron;
	public Baron GetBaron{get => baron;}
	private void СaptureRegion(Baron baron = null){
		this.baron = baron;
		SetBorders();

		ClearTerritory();
	}
	void ClearTerritory(){ mainTerritory.DOColor(Color.white, 1f).OnComplete(FillTerritory); }
	void FillTerritory(){ mainTerritory.DOColor(baron.color, 1f); }

	public void OnCloseMission(){
		if(missions.Find(x => x.isFinish == false) == null){
			СaptureRegion(BaronsControllerScript.Instance.GetBaron(BaronName.Player));
		}
	}
	public void SetBaron(BaronName name){
		СaptureRegion(BaronsControllerScript.Instance.GetBaron(name));
	}
	public void SetRegion(Region region){
		this.region = region;
		this.baron  = BaronsControllerScript.Instance.GetBaron(region.baron);
		SetBorders();
		mainTerritory.color = baron.color; 
	}
	public void SetGameMission(int currentMission){
		for(int i = 0; i < missions.Count; i++){
			missions[i].CheckCurrentMission(currentMission);
		}
	}
	void SetBorders(){
		foreach(BorderScript border in borders){ border.SetBaron(this.baron); }
	}
	void Start(){
		CampaignScript.Instance.AddRegionController(this);
	}
	public Action observerChangeBaron;
	public void RegisterOnChangeBaron(Action d){ observerChangeBaron += d; }  
	public void UnregisterOnChangeBaron(Action d){ observerChangeBaron -= d; }  
	private void OnChangeBaron(){ if(observerChangeBaron != null) observerChangeBaron(); } 
}