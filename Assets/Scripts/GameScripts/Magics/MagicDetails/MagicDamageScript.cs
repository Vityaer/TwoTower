using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamageScript : RefreshObjectBehaviour{
	[SerializeField] private int damage = 1;
	void Awake(){
		tr = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}
    public override void SetData(Transform target){
		tr.position = target.position;
		tr.parent = target;
		anim?.Play("Action");
		target.GetComponent<MobHP>().GetDamage(damage);
	}
}
