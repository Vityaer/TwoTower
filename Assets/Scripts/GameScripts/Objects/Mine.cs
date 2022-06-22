using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour{
    protected LayerMask whatIsEnemy;
    [SerializeField] protected int damage;
    [SerializeField] protected float radiusExplosion;
	public void SetData(int layer){
    	whatIsEnemy = (layer == 9) ? LayerMask.GetMask("LeftMob") : LayerMask.GetMask("RightMob");
	}
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.GetComponent<MobHP>()){
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusExplosion, whatIsEnemy);
			for(int i = 0; i < colliders.Length; i++){
				colliders[i].GetComponent<MobHP>().GetDamage(damage);
			}
		}
	}
}