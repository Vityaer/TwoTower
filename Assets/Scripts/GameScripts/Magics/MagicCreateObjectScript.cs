using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCreateObjectScript : MagicSpell{
	[SerializeField] private GameObject prefab;
	[SerializeField] private int countTarget = 1;
	void Awake(){
		tr = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}
	public override void SetTarget(Transform target){
		if(targets.Count < countTarget) targets.Add(target);
		if(targets.Count == countTarget) CreateObjects();
	}
	public override void SetTarget(Vector3 pos){
	    PoolScript.Instance.GetRefreshOther(prefab).SetData(gameObject.layer, pos, transform.rotation);	
	}
	void CreateObjects(){
		for(int i = 0; i < targets.Count; i++){
		    PoolScript.Instance.GetRefreshOther(prefab).SetData(targets[i]);	
		}
	}
}