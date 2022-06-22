using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayScript : MonoBehaviour{
	[SerializeField] private bool canUse = false;
	[SerializeField] private Transform tr;
	private List<Transform> listPoint = new List<Transform>();
	[SerializeField] private TypeWay typeWay;
	public TypeWay GetTypeWay{get => typeWay;}
	void Start(){
		tr = GetComponent<Transform>();
		CheckLisPoint();
		WaysControllerSctipt.Instance.AddWay(this);
	}
	void CheckLisPoint(){
		listPoint.Clear();
		foreach(Transform child in tr) listPoint.Add(child);
	}
	Transform nexPoint = null;
	public Transform GetNextPoint(int sideMob, ref int currentPoint){
		nexPoint = null;
		if(sideMob == 8){
			if(currentPoint + 1 < listPoint.Count){
				nexPoint = listPoint[++currentPoint];
			}else if((currentPoint + 1) == listPoint.Count){
				currentPoint++;
				nexPoint = TowerControllerScript.RightTower.TowerTransform;
			}else{
				nexPoint = null;
			}
		}else if(sideMob == 9){
			if(currentPoint - 1 >= 0){
				nexPoint = listPoint[--currentPoint];
			}else if((currentPoint - 1) == -1){
				currentPoint--;
				nexPoint = TowerControllerScript.LeftTower.TowerTransform;
			}else{
				nexPoint = null;
			}
		}
		return nexPoint;
	}
	public Transform GetStartPoint(int sideMob, ref int currentPoint){
		switch(sideMob){
			case 8:
				currentPoint = -1;
				break;
			case 9:
				currentPoint = listPoint.Count;
				break;
			default:
				Debug.Log("problem with side");
				break;	
		}
		return GetNextPoint(sideMob, ref currentPoint);
	}
	public bool IsFinishPoint(int sideMob, int currentPoint){
		if(sideMob == 8){
			if(currentPoint == (listPoint.Count + 1)){
				return true;
			}else{
				return false;
			}
		}else{
			if(currentPoint == -1){
				return true;
			}else{
				return false;
			}
		}
	}
}
public enum TypeWay{
	Left   = 0,
	Middle = 1,
	Right  = 2
}