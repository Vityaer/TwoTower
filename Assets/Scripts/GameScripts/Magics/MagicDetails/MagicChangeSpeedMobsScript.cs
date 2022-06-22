using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicChangeSpeedMobsScript : RefreshObjectBehaviour{
	[SerializeField] private float amount = 0f;
	[SerializeField] private float time = 0f;
	void Awake(){
		tr = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}
	public virtual void SetData(Transform target){
		tr.position = target.position;
		anim.Play("Action");
		target.GetComponent<MobMovement>().SetBonusSpeed(amount, time);
		Disable();
	}
}