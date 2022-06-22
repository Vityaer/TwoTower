using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHealth : Mine{
	[SerializeField] private int heal;
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.GetComponent<MobHP>()){
			coll.GetComponent<MobHP>().GetDamage(damage);
			LayerMask whatIsFriend = (gameObject.layer == 9) ? LayerMask.GetMask("RightMob") : LayerMask.GetMask("LeftMob");
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusExplosion, whatIsFriend);
			for(int i = 0; i < colliders.Length; i++){
				colliders[i].GetComponent<MobHP>().Heal(heal);
			}
		}
	}

}