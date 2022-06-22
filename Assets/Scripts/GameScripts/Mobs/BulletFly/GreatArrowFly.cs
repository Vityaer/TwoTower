using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatArrowFly : BulletFly {
	[SerializeField] private int countTarget = 5;
    public override void StartFly(Transform target, GameObject master){
        GetComponents();
        this.master = master;
        this.target = target;
        ChangeDir();
        target.GetComponent<MobHP>().RegisterOnDeath(OnDeathTarget);
    }
    protected override void OnTriggerEnter2D(Collider2D coll){
        if (coll.transform == target){
	        OnDamage();
			bool leftDamage = (dir.x < 0) ? true : false;
			coll.gameObject.GetComponent<MobHP>().GetDamage(damage, master);
        }
    }
    protected override void OnDamage(){
    	countTarget -= 1;
    	if(countTarget > 0){
            LayerMask whatIsEnemy = (target.gameObject.layer == 9) ? LayerMask.GetMask("LeftMob") : LayerMask.GetMask("RightMob");
    		target = MobFight2D.NearTargetWithIgnore(tr.position, Physics2D.OverlapCircleAll(transform.position, 30f, whatIsEnemy), target)?.GetComponent<Transform>();
			if(target != null){
				StartFly(target, this.master);
			}else{ Disable(); }	
		}else{ Disable(); }
    }
}
