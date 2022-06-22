using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IroncladeScript : UnitScript{
    
    void Update() {
        CheckEnemiesFunction();
		if (isGame && enemyNear && canAttack){ Attack(); }
    }

    protected void OnTriggerEnter2D(Collider2D coll){
        if (coll.TryGetComponent(out BulletFly componentBulletFly)){
        	componentBulletFly.RevertMove(gameObject);
        }
    }
}
