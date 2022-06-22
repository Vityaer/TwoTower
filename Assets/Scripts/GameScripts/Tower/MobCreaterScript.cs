using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;

public class MobCreaterScript : MonoBehaviour{
    public GameObject prefabMob;
    public int currentMob = 0;
    public bool autoCreate;
    public float timeCreate = 10f;
    GameTimer timerAutoCreate;
    TimerScript Timer;
    [SerializeField] private Transform pointCreate;
    public void Start(){
        Timer = TimerScript.Timer;
        if(autoCreate == true){ timerAutoCreate = Timer.StartTimer(timeCreate, AutoCreate); }
        WinLoseControllerScript.Instance.RegisterOnFinishGame(StopTimers);
    }
    public void CreateMob(){CreateMob(prefabMob);}
    public void CreateMob(GameObject prefab){
        PoolScript.Instance.GetRefreshUnit(prefab)?.SetData(gameObject.layer, pointCreate.position, pointCreate.rotation);	
 }
    void AutoCreate(){
        CreateMob();
        timerAutoCreate = Timer.StartTimer(timeCreate, AutoCreate);
    }
    void OnDestroy(){
        StopTimers();
    }
//API
    private GameObject previousWarrior;
    GameTimer timerReplacement;
    public void SetNewAutoCreateWarrior(GameObject newPrefab, int amountSecond = 0){
        if(newPrefab != null){
            if(Timer == null) Debug.Log("timer not found");
            if(timerReplacement!= null) Timer.StopTimer(timerReplacement);
            if(amountSecond > 0){
                previousWarrior  = prefabMob;
                timerReplacement = Timer.StartTimer(amountSecond, ReturnBaseWarrior); 
            }
            prefabMob = newPrefab;
        }else{Debug.Log("new prefab == null");}
    }
    public void ReturnBaseWarrior(){
        Timer.StopTimer(timerReplacement);
        if(previousWarrior != null)
            prefabMob = previousWarrior;
    }
    private void StopTimers(){
        if(timerAutoCreate != null) Timer.StopTimer(timerAutoCreate);
        if(timerReplacement != null) Timer.StopTimer(timerReplacement);
    }

}
