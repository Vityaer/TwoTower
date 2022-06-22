using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
public class MinerScript : UnitScript{

    private GameTimer timerSetMine;
   	[SerializeField] private Transform posMine; 
    [SerializeField] private GameObject prefabMine;
    [SerializeField] private float timeRestSetMine;
    void Start(){
        timerRestAttack = Timer.StartTimer(timeRestSetMine, SetMine);
    }
    void SetMine(){
		if(ismove){
			GameObject mine = Instantiate(prefabMine, posMine.position, tr.rotation);
			mine.GetComponent<Mine>().SetData(gameObject.layer);
		}    	
    }
    void Update() {
        CheckEnemiesFunction();
		if (isGame && ismove && enemyNear && canAttack)
			Attack(); 
    }
}
