using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;

public class BulletFly : RefreshObjectBehaviour{
    protected Rigidbody2D rb;
    public float speed = 7.5f; 
    protected Vector3 dir;
    public Vector2 miss = new Vector2(0,0);
    public int damage;
    protected Transform target;
    [SerializeField] protected float timeForChangeDir = 0.2f;
    protected GameTimer timerChangeDir;
    protected static TimerScript Timer;
    // Use this for initialization
    void Awake () {
        GetComponents();
    }
    protected void GetComponents(){
        if(tr == null){
            anim = GetComponent<Animator>();
            tr = GetComponent<Transform>();
            rb = gameObject.GetComponent<Rigidbody2D>();
            if(Timer == null) Timer = TimerScript.Timer;
        }
    }
    [SerializeField] protected GameObject master;
    public virtual void StartFly(Transform target, GameObject master){
        this.master = master;
        target.GetComponent<MobHP>().RegisterOnHit(OnDamage);
        target.GetComponent<MobHP>().RegisterOnDeath(OnDeathTarget);
    }
    public virtual void RevertMove(GameObject newMaster){
        target?.GetComponent<MobHP>().UnregisterOnDeath(OnDeathTarget);
        target?.GetComponent<MobHP>().UnregisterOnHit(OnDamage);
        StartFly(master.transform, newMaster); 
    }
    protected virtual void OnTriggerEnter2D(Collider2D coll){
        if (coll.transform == target){
            OnDamage();
			bool leftDamage = (dir.x < 0) ? true : false;
			coll.gameObject.GetComponent<MobHP>().GetDamage(damage, master);
            Disable();
        }
    }
    protected virtual void OnDamage(){}
    protected virtual void OnDeathTarget(){Disable();}
    protected void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Update(){
        tr.rotation = Quaternion.Euler(0, 0, 180+Mathf.Atan2(rb.velocity.y, rb.velocity.x)*(180/Mathf.PI));    
    }
    void OnDestroy(){ Timer?.StopTimer(timerChangeDir); }
    public override void SetData(int layer,  Vector3 position, Quaternion rotation){
        tr.position = position;
        tr.rotation = rotation;
        isDisable = false;
    }
    public override void Disable(){
        Timer.StopTimer(timerChangeDir);
        transform.position = disablePosition;
        rb.velocity = new Vector2();
        isDisable = true;
        target?.GetComponent<MobHP>().UnregisterOnDeath(OnDeathTarget);
        target?.GetComponent<MobHP>().UnregisterOnHit(OnDamage);
    }
    protected void ChangeDir(){
        if(target != null){
            dir = (target.position - tr.position).normalized;
            tr.rotation = Quaternion.Euler(0, 0, ((dir.x < 0) ? 0 : 180) + Mathf.Atan2(dir.y, dir.x)*(180/Mathf.PI));    
            rb.velocity = dir * speed;
            timerChangeDir = Timer.StartTimer(timeForChangeDir, ChangeDir);
        }
    }
}
