using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
public class UnitScript : RefreshObjectBehaviour{
    protected bool isGame = true;
    [SerializeField] protected bool enemyNear = false;
    [SerializeField] protected bool ismove = true;
    [SerializeField] protected bool canAttack = true;
    [SerializeField] protected int damage;
    [SerializeField] protected Transform punch;
    [SerializeField] protected float RestTime = 1.5f;
    [SerializeField] protected float RadiusAttack = 1f;
    protected LayerMask whatIsEnemy;
    protected MobMovement movement;
    protected MobHP hpController;
    protected static TimerScript Timer;
    void Awake () {
        hpController = GetComponent<MobHP>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        movement = GetComponent<MobMovement>();
        if(Timer == null) Timer = TimerScript.Timer;
        hpController.RegisterOnDeath(OnDeath);
        hpController.RegisterOnDeath(movement.StopMove);
    }
    public override void SetData(int layer,  Vector3 position, Quaternion rotation){
        ChangeData(layer, position, rotation);
        hpController.Refresh();
    }
    public void ChangeData(int layer, Vector3 position, Quaternion rotation){
        gameObject.layer = layer;
        if(layer == 9){
            whatIsEnemy = LayerMask.GetMask("LeftMob");
        }else{
            movement.Flip();
            whatIsEnemy = LayerMask.GetMask("RightMob");
        }
        tr.position = position;
        tr.rotation = rotation;
        isDisable = false;
        isGame = true;
        movement.StartMove();
    }
    protected virtual void AttackStop(){
        anim.SetBool("Attack", false);
    }
    protected virtual void AttackGo(){
        MobFight2D.Action(punch.position, RadiusAttack, whatIsEnemy, damage, movement.IsFacingRight, gameObject);
    }
    protected void CheckEnemiesFunction(){
        CheckEnemies();
    }
    protected virtual void CheckEnemies(){
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
    protected GameTimer timerRestAttack;
    protected virtual void Attack(){
        anim?.SetBool("Speed", false);
        anim?.SetBool("Attack", true);
        anim?.Play("Attack");
        timerRestAttack = Timer.StartTimer(RestTime, RestAttack);
        canAttack = false;
    }
    public override void Disable(){
        movement.StopMove();
        transform.position = disablePosition;
        Timer.StopTimer(timerRestAttack);
        isDisable = true;
        RestAttack();
    }
    private void OnDeath(){
        RestAttack();
        isGame = false;
    }
    protected void RestAttack(){
        canAttack = true;
    }
    void OnDestroy(){}
}
