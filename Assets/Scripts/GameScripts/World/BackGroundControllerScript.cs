using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BackGroundControllerScript : MonoBehaviour{

	private static BackGroundControllerScript instance;
	public  static BackGroundControllerScript Instance{get => instance;}
	public int indexCurrentTerrain = 0;
	public List<BackgroundTerrain> terrains = new List<BackgroundTerrain>();
	void Awake(){
		instance = this;
		if(Information.Instance != null)
			OpenBackground(Information.Instance.level.typeTerrain);
	}
   	public void OpenBackground(TypeTerrain type){
   		int index = 0;
   		for(int i = 0; i < terrains.Count; i++){
   			if(type == terrains[i].type){
   				index = i;
   				break;
   			}
   		}
		if(index != indexCurrentTerrain){
			terrains[indexCurrentTerrain].background.SetActive(false);
			indexCurrentTerrain = index;
			terrains[indexCurrentTerrain].background.SetActive(true);
		}
	}
	private Action observerChangeBackground;
	public void RegisterOnChangeBackground(Action d){observerChangeBackground += d;}
	public void UnregisterOnChangeBackground(Action d){observerChangeBackground -= d;}
	private void OnChangeBackground(){if(observerChangeBackground != null) observerChangeBackground();}
}

[System.Serializable]
public class BackgroundTerrain{
	public TypeTerrain type;
	public GameObject background;
	public BackgroundTerrain(TypeTerrain type, GameObject background){
		this.type       = type;
		this.background = background;
	}
}
public enum TypeTerrain{
	Forest,
	Swamp,
	Mountains
	// River,
	// TwoRiver,
	// MonkenGole,
	// OneWay,
	// BigLake,
	// Capital,
	// Tunnels,
	// LoopWays,
	// Desert,
	// FogPlace,
	// DeathEarth,
	// FireEarth,
	// LavaField,
	// MiniTowers,
	// Teleports
}