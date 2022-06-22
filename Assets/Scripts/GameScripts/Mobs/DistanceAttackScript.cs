using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAttackScript : UnitScript{
	public GameObject arrow;
    void Update() {
        CheckEnemiesFunction();
		if (isGame && ismove && canAttack && enemyNear){ Attack(); }
    }
    protected Vector3 position = new Vector3(1f, 0, 0);
    protected Transform target = null;
    protected GameObject curArrow;
    RefreshObjectBehaviour bullet = null;
    public virtual void AttackGo(){
    	target = MobFight2D.NearTarget(tr.position, Physics2D.OverlapCircleAll(punch.position, RadiusAttack, whatIsEnemy))?.GetComponent<Transform>();
        if(target != null){
            bullet = PoolScript.Instance.GetRefreshBullet(arrow);
            bullet.SetData(gameObject.layer, tr.position + position * (movement.IsFacingRight ? -1f : 1f), tr.rotation);  
	    	(bullet as BulletFly).StartFly(target, gameObject);
        }else{
            RestAttack();
        }
    }
    protected override void CheckEnemies(){
        if(enemyNear != Physics2D.OverlapCircle(punch.position, RadiusAttack, whatIsEnemy)){
            enemyNear = !enemyNear;
            if(enemyNear){
                movement.StopMove();
            }else{
                movement.ContinueMove();
            } 
        }
        if((movement.Status == StatusMovement.Move) && (enemyNear == true)){
            movement.StopMove();
        }
    }

}
