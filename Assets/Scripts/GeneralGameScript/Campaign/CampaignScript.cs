using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignScript : MonoBehaviour{
	[SerializeField] int countRegions = 2;
	[SerializeField] List<RegionControllerScript> regions = new List<RegionControllerScript>();
	private Game game = new Game();
	private bool saveWasLoaded = false;
	void Start(){
		LoadGame();
	}
	private void LoadGame(){
		SaveLoadScript.LoadGame(game);
		if(game != null){
			saveWasLoaded = true;
			Information.Instance.playerHero = game.playerHero;
			game.playerHero.PreparePlayer();
		}else{
			game = new Game();
			game.playerHero = new PlayerHero("Test");
		}
		if(regions.Count == countRegions){ LoadMapOnStart(); }
	}
	public void LoadMapOnStart(){
		if(saveWasLoaded){
			for(int i = 0; i < game.regions.Count; i++){
				regions.Find(x => x.ID == game.regions[i].ID)?.SetRegion(game.regions[i]);
			}
			for(int i = 0; i < regions.Count; i++){
				regions[i].SetGameMission(game.currentMission);
			}
		}
	}
	void Awake(){
		if(instance == null){
	        instance = this;
	    	DontDestroyOnLoad(gameObject);
    	}else{
    		Destroy(gameObject);
    	}
	}

	public void AddRegionController(RegionControllerScript regionController){
		regions.Add(regionController);
		if(regions.Count == countRegions){
			LoadMapOnStart();
		}
	}
	public void CaptureRegion(CaptureRegion captureRegion){
		(regions.Find(x => x.region.ID == captureRegion.ID)).SetBaron(captureRegion.baronName);
	}
	private static CampaignScript instance;
	public static CampaignScript Instance{get => instance;}
	public void OpenNextMission(){
		game.currentMission++;
		LoadMapOnStart();
	}
	public void FinishCurrentLevel(){
		game.currentMission++;
		SaveGame();
	}
	public void SaveGame(){ SaveLoadScript.SaveGame(game); }
	public void ClearMap(){
		regions.Clear();
	}
}