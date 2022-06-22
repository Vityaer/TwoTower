using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHealScript : RefreshObjectBehaviour{
	[SerializeField] private int amountHeal;
	void Awake(){
		anim = GetComponent<Animator>();
		tr = GetComponent<Transform>();
	}
	public override void SetData(Transform target){
		tr.position = target.position;
		tr.parent = target;
		anim?.Play("Action");
		target.GetComponent<MobHP>().Heal(amountHeal);
		Disable();
	}
}
