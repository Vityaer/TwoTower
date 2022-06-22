using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStarScript : MagicSpell{
	private Rigidbody2D rb;
	public float speed = 1f;
	public int damage = 2;
	Vector3 dir;
	ParticleSystem ps;
	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		ps = GetComponent<ParticleSystem>();
		base.Awake();
	} 
	Coroutine coroutineMove;
	public override void SetTarget(Transform target){
		Debug.Log("выбрали цель");
		UnregisterObserve();
        var vel = ps.velocityOverLifetime;
        vel.enabled = false;
        this.targets.Add(target);
        target.GetComponent<MobHP>().RegisterOnDeath(LoseTarget);
       	StopMove();
       	if(IsExistTargets) coroutineMove = StartCoroutine(IFollowToTarget(target));
	}
	IEnumerator IFollowToTarget(Transform target){
		bool move = true;
		while(move){
			if(IsExistTargets){
				move = ((target.position - tr.position).sqrMagnitude > 0.35f);
				dir = (target.position - tr.position).normalized * speed;
				rb.AddForce(dir);
			}else{move = false;}
			yield return new WaitForSeconds(0.25f); 
		}
	}
	public void StopMove(){
		if(coroutineMove != null){
        	StopCoroutine(coroutineMove);
        	coroutineMove = null;
        }
	}
	void OnTriggerEnter2D(Collider2D coll){
		if(IsExistTargets){
			if(coll.transform == target){
		        target.GetComponent<MobHP>().UnregisterOnDeath(LoseTarget);
				coll.transform.GetComponent<MobHP>().GetDamage(damage);
				StopMove();
	        	this.targets.Remove(target);
				Disable();
			}
		}
	}
	protected override void LoseTarget(){
		base.LoseTarget();
		StopMove();
		Disable();
	}
}