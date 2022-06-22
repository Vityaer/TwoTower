using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : UnitScript{
    [SerializeField] private bool doubleAttack = true;
    void Update() {
        CheckEnemiesFunction();
		if (isGame && enemyNear && canAttack){ Attack(); }
    }
    protected override void Attack(){
    	anim.SetBool("Speed", false);
        anim.SetBool("Attack", true);
        anim.Play("Attack");
        timerRestAttack = Timer.StartTimer(doubleAttack ? 0.75f : RestTime, RestAttack);
        doubleAttack = !doubleAttack;
        canAttack = false;
        ismove = false;
    }
}
