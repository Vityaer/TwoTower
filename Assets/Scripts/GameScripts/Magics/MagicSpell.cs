using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : RefreshObjectBehaviour{
   	[SerializeField] protected TypeTarget typeTarget; 
   	protected List<Transform> targets = new List<Transform>();
   	protected Transform target {get => targets[0];}
   	protected bool IsExistTargets{get => (targets.Count > 0);}
   	protected Vector3 posTarget;
	protected void Awake(){
		tr = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}
	public virtual void SetTarget(Transform target){}
	public virtual void SetTarget(Vector3 targetPos){}
	public override void SetData(int layer, Vector3 position, Quaternion rotation){
		RegisterObserve();
		transform.position = position;
		transform.rotation = rotation;
	}
	public override void Disable(){
		UnregisterObserve();
		transform.position = disablePosition;
		isDisable = true;
	}
	protected virtual void RegisterObserve(){
		switch(typeTarget){
			case TypeTarget.Targets:
				Debug.Log("regist");
				CameraMoveControllerScript.Instance.RegisterOnSelectMob(SetTarget);
				break;
			case TypeTarget.Field:
				CameraMoveControllerScript.Instance.RegisterOnTouchMap(SetTarget);
				break;
			case TypeTarget.AllMobs:
				GameObject[] list = GameObject.FindGameObjectsWithTag("Mob");
				for(int i = 0; i < list.Length; i++)
					SetTarget(list[i].transform);	
				break;
			default:
				Debug.Log("here");
				break;	
		}
	}
	protected void UnregisterObserve(){
		switch(typeTarget){
			case TypeTarget.Targets:
				CameraMoveControllerScript.Instance.UnRegisterOnSelecMob(SetTarget);
				break;
			case TypeTarget.Field:
				CameraMoveControllerScript.Instance.UnRegisterOnTouchMap(SetTarget);
				break;
			default:
				break;	
		}
	}
	protected virtual void LoseTarget(){
        target.GetComponent<MobHP>().UnregisterOnDeath(LoseTarget);
    	this.targets.Remove(target);
	}
}
public enum TypeTarget{
	Targets,
	Field,
	AllMobs,
	FriendMobs,
	EnemyMobs
}