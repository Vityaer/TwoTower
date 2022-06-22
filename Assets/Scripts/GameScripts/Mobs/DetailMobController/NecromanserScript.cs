using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanserScript : DistanceAttackScript{
    public override void AttackGo(){
    	target = MobFight2D.NearTarget(tr.position, Physics2D.OverlapCircleAll(punch.position, RadiusAttack, whatIsEnemy))?.GetComponent<Transform>();
        if(target != null){
	    	curArrow = Instantiate(arrow, tr.position + position * (movement.IsFacingRight ? -1f : 1f), tr.rotation);
	    	NecrMagicFly scriptBullet = curArrow.GetComponent<NecrMagicFly>();
            scriptBullet.StartFly(target, gameObject);
            scriptBullet.layerMaster = gameObject.layer;

        }
    }
}
