using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PoolScript : MonoBehaviour{
	[SerializeField]private List<RefreshObjectBehaviour> refreshUnits = new List<RefreshObjectBehaviour>();
	[SerializeField]private List<RefreshObjectBehaviour> refreshBullets = new List<RefreshObjectBehaviour>();
	[SerializeField]private List<RefreshObjectBehaviour> refreshOther = new List<RefreshObjectBehaviour>();
	private List<RefreshObjectBehaviour> workList;
	bool find = false;
	RefreshObjectBehaviour result = null;
	private RefreshObjectBehaviour FindObject(GameObject prefab, TypeRefreshObject type){
		if(prefab != null){
			find = false;
			switch(type){
				case TypeRefreshObject.Unit:
					workList = refreshUnits;
					break;
				case TypeRefreshObject.Bullet:
					workList = refreshBullets;
					break;
				case TypeRefreshObject.Other:
					workList = refreshOther;
					break;
			}
			for(int i = 0; i < workList.Count; i++){
				if(workList[i].gameObject.name.IndexOf(prefab.name) >= 0){
					if(workList[i].IsDisable == true){
						find = true;
						result = workList[i];
						break;
					}
				}
			}
			if(find == false){
				result = Instantiate(prefab, tr.position, tr.rotation).GetComponent<RefreshObjectBehaviour>();
				workList.Add(result);
			}
			result.transform.parent = transform;
		}else{Debug.Log("не могу создать пустой объект");}
		return result;
	}
//API	
	public RefreshObjectBehaviour GetRefreshUnit(GameObject prefab)  { return FindObject(prefab, TypeRefreshObject.Unit); }
	public RefreshObjectBehaviour GetRefreshBullet(GameObject prefab){ return FindObject(prefab, TypeRefreshObject.Bullet); }
	public RefreshObjectBehaviour GetRefreshOther(GameObject prefab) { return FindObject(prefab, TypeRefreshObject.Other); }

	public enum TypeRefreshObject{
		Unit,
		Bullet,
		Other
	}
	Transform tr;
	void Awake(){instance = this; tr = transform;}
	void Start(){
        WinLoseControllerScript.Instance.RegisterOnFinishGame(StopGame);
	}
	private TypeRefreshObject typeRefreshObject;
	private static PoolScript instance;
	public static PoolScript Instance{get => instance;}

//API
	private void StopGame(){
		foreach(RefreshObjectBehaviour refreshObject in refreshUnits){refreshObject.Disable();}
		foreach(RefreshObjectBehaviour refreshObject in refreshBullets){refreshObject.Disable();}
		foreach(RefreshObjectBehaviour refreshObject in refreshOther){refreshObject.Disable();}
	}
}
