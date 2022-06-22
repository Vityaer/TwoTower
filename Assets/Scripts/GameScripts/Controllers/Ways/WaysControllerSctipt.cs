using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaysControllerSctipt : MonoBehaviour{
	void Awake(){
		instance = this;
	}
	void Start(){
		BackGroundControllerScript.Instance.RegisterOnChangeBackground(ClearAllWay);
	}
	[SerializeField] private List<WayScript> ways = new List<WayScript>();

	[SerializeField] WayScript selectedLeftTowerWay;
	[SerializeField] WayScript selectedRightTowerWay;
	public void SelectWay(TypeWay type, int side = 8){
		if(side == 8){
			selectedLeftTowerWay = ways.Find(x => x.GetTypeWay == type);
		}else{
			selectedRightTowerWay = ways.Find(x => x.GetTypeWay == type);
		}
	}
	public WayScript GetWay(int sideMob){
		if (sideMob == 8){
			return (selectedLeftTowerWay != null) ? selectedLeftTowerWay : GetRandomWay;
		}else{
			return (selectedRightTowerWay != null) ? selectedRightTowerWay : GetRandomWay;
		}
	}

	public void AddWay(WayScript newWay){ ways.Add(newWay); }
	public WayScript GetRandomWay{ get{return ways[Random.Range(0, ways.Count)]; } }

	public void NewPositionTower(Vector3 pos, bool left){
		if(left){
			TowerControllerScript.LeftTower.SetNewPosition(pos);
		}else{
			TowerControllerScript.RightTower.SetNewPosition(pos);
		}
	}
	public void ClearAllWay(){
		ways.Clear();
	}
	private static WaysControllerSctipt instance; 
	public static WaysControllerSctipt Instance{get => instance;} 

}
