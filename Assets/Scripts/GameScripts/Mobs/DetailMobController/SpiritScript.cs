using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : UnitScript{
    void Update() {
        CheckEnemiesFunction();
		if (isGame && ismove && enemyNear && canAttack){ Attack(); }
    }
  

    protected override void AttackGo(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, RadiusAttack, whatIsEnemy);
		GameObject enemy = MobFight2D.NearTarget(tr.position, colliders);
		enemy.GetComponent<UnitScript>().ChangeData(gameObject.layer, enemy.transform.position, enemy.transform.rotation);
    	hpController.Kill();
    }
}
