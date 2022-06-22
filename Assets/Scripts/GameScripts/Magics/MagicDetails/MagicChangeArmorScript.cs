using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicChangeArmorScript : RefreshObjectBehaviour{
	[SerializeField] private int amount = 1;
	[SerializeField] private float time = 10f;
	void Awake(){
		tr = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}
    public override void SetData(Transform target){
		tr.position = target.position;
		tr.parent = target;
		anim?.Play("Action");
		target.GetComponent<MobHP>().SetBonusArmor(amount, time);
	}
}
