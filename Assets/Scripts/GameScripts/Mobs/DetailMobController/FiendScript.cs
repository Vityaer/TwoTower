using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiendScript : UnitScript{
    private bool canJump = true;

	void Update() {
        CheckEnemiesFunction();
		if (isGame && ismove && enemyNear){
        	if(canAttack){ Attack(); }else if(canJump){ Jump(); }
        }
    }
  
    private void Jump(){
    	timerRestAttack = Timer.StartTimer(RestTime, RestAttack);
    	canJump = false;
        canAttack = false;
        ismove = false;
		movement.Jump();
    }
}