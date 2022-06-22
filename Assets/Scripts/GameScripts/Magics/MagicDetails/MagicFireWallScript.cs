using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireWallScript : MonoBehaviour{
	[SerializeField] private int damage = 1;
    void OnTriggerEnter2D(Collider2D coll){
    	if(coll.transform.TryGetComponent(out MobHP mobHP)){
    		Target curTarget = targets.Find(x => x.mobHP == mobHP); 
			if(curTarget == null){
				curTarget = new Target(mobHP); 
				targets.Add(curTarget);
				curTarget.mobHP.GetDamage(damage);
				curTarget.OffDamage();
			}else{
				if(curTarget.canSetDamage == true){
					curTarget.mobHP.GetDamage(damage);
					curTarget.OffDamage();
				}
			}
			if(coroutineDecreseTimer == null)
				coroutineDecreseTimer = StartCoroutine(IDecreaseTimers()); 
    	}
	}

	void OnTriggerExit2D(Collider2D coll){
    	if(coll.transform.TryGetComponent(out MobHP mobHP)){
			targets.Remove(targets.Find(x => x.mobHP == mobHP));
		}
	}

	public class Target{
		public MobHP mobHP;
		public bool canSetDamage = true;
		public float timer = 0.5f;
		public Target(MobHP mobHP){ this.mobHP = mobHP; }
		public void OffDamage(){
			canSetDamage = false;
			timer = 0.5f;
		}
		public void DeacreseTimer(float delta){
			timer -= delta;
			if(timer <= 0f) canSetDamage = true;
		}
	}
	private List<Target> targets = new List<Target>(); 
	Coroutine coroutineDecreseTimer;
	IEnumerator IDecreaseTimers(){
		int i = 0;
		float delta = 0f;
		while(targets.Count > 0){
			delta = Time.deltaTime;
			for(i = 0; i < targets.Count; i++){
				if(targets[i].canSetDamage == false)
					targets[i].DeacreseTimer(delta);
			}
			yield return null;
		}
		coroutineDecreseTimer = null;		
	}
}
