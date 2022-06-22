using UnityEngine;
using System.Collections;


public class RogueScript : UnitScript {
    
    void Update() {
        CheckEnemiesFunction();
		if (isGame && enemyNear && canAttack){ Attack(); }
    }
}
