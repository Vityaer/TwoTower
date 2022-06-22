using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikadzeScript : UnitScript{
   void Update() {
        CheckEnemiesFunction();
		if (isGame && ismove && enemyNear && canAttack){ Attack(); }
    }
    void Explosion(){
        MobFight2D.StrikeAll(punch.position, RadiusAttack, LayerMask.GetMask("LeftMob"), damage, movement.IsFacingRight, gameObject);
        MobFight2D.StrikeAll(punch.position, RadiusAttack, LayerMask.GetMask("RightMob"), damage, movement.IsFacingRight, gameObject);
    }
}
