using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;

public class MobMovement : MonoBehaviour{
	private Rigidbody2D rb;
    [SerializeField] private float defaultSpeedRun = 4;
    [SerializeField] private float speedRun = 4;
    private Vector3 dir = new Vector3();
    private int currentPoint = -1;
    private Transform tr;
    private WayScript way;
    private Animator anim;
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private bool isRandomWay = false;
    private Transform nextPointMove;
    public bool IsFacingRight{get => isFacingRight;}
    [SerializeField] private StatusMovement statusMovement;
    public StatusMovement Status{get => statusMovement;}
    public bool show = false;
	void Awake(){
		anim = GetComponent<Animator>();
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        speedRun = defaultSpeedRun;
	}
    void Start(){ StartMove(); }
	public void StartMove(){
        way = (isRandomWay == false) ? WaysControllerSctipt.Instance.GetWay(gameObject.layer) : WaysControllerSctipt.Instance.GetRandomWay;
        nextPointMove = way.GetStartPoint(gameObject.layer, ref currentPoint); 
        ContinueMove();
	}
	Coroutine coroutineMove;
    float alfa = 0f;
    IEnumerator IMove(){
        anim.SetBool("Speed", true);
        alfa = 0;
        while((nextPointMove.position - tr.position).sqrMagnitude > 0.35f){
            dir = (nextPointMove.position - tr.position).normalized * speedRun;
            rb.velocity = dir;
            if(show) Debug.Log(dir.ToString());
            if((dir.x > 0) ^ isFacingRight) Flip();
            yield return new WaitForSeconds(0.1f);
        }
        if((dir.sqrMagnitude < 0.05f) || ((nextPointMove.position - tr.position).sqrMagnitude < 0.35f)){
            if(show) Debug.Log("next point");
            nextPointMove = way.GetNextPoint(gameObject.layer, ref currentPoint); 
            ContinueMove();
        }
    }
    public void Jump(){
         // StartCoroutine(IJump());
    }
    public void StopMove(){
    	if(coroutineMove != null){
            statusMovement = StatusMovement.Stop;
    		StopCoroutine(coroutineMove);
    		coroutineMove = null;
	        anim.SetBool("Speed", false);
	        dir.x = 0;
	        dir.y = 0;
	        rb.velocity = dir;
    	}
    }
    private SpriteRenderer sprite;
    public void Flip(){
        isFacingRight = !isFacingRight;
        sprite.flipX = isFacingRight;
    }
    public void ContinueMove(){
        if(way.IsFinishPoint(gameObject.layer, currentPoint) == false){
            if(coroutineMove != null){
                StopCoroutine(coroutineMove);
                coroutineMove = null;
            }
            if(nextPointMove != null){
                statusMovement = StatusMovement.Move;
            	coroutineMove = StartCoroutine(IMove());
            }
        }
    }
    void OnDrawGizmos() {
        if(nextPointMove != null)
            Gizmos.DrawLine(tr.position, nextPointMove.position);
    }
    GameTimer timerRemoveBonusSpeed;
    public void SetBonusSpeed(float bonus, float time){
        RemoveBonus();
        speedRun = defaultSpeedRun * (1f + bonus/100f);
        timerRemoveBonusSpeed = TimerScript.Timer.StartTimer(time, RemoveBonus);
    }
    void RemoveBonus(){
        if(timerRemoveBonusSpeed != null) TimerScript.Timer.StopTimer(timerRemoveBonusSpeed);
        speedRun = defaultSpeedRun;
    }
}
public enum StatusMovement{
    Move,
    Stop
}