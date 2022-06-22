using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;

public class HilerScript : UnitScript{
	LayerMask whatIsFriend;
	[SerializeField] private int heal = 1;
	[SerializeField] private float RestTimeHeal = 5f;
	[SerializeField] private float RadiusHeal = 5f;
	bool canHeal = false;
	GameTimer timerRestHeal;
	public override void SetData(int layer,  Vector3 position, Quaternion rotation){
		whatIsFriend = (layer == 9) ? LayerMask.GetMask("RightMob") : LayerMask.GetMask("LeftMob");
		base.SetData(layer, position, rotation);
	}
	bool friendNear = false;
	void Update() {
        CheckEnemiesFunction();
        CheckFriendsFunction();
		if (isGame && ismove && enemyNear && canAttack){ Attack(); }
		if (isGame && !enemyNear && friendNear && canHeal){ Heal(); }
    }
    void CheckFriendsFunction(){
	    if(friendNear != Physics2D.OverlapCircle(punch.position, RadiusHeal, whatIsFriend)){
            friendNear = !friendNear;
        } 
    }
    void Heal(){
        movement.StopMove();
    	anim.SetBool("Speed", false);
        anim.SetBool("Heal", true);
        anim.Play("Heal");
        timerRestHeal = Timer.StartTimer(RestTimeHeal, RestHeal);
        canHeal = false;
        ismove = false;
    }
    void HealGo(){
    	Collider2D[] colliders = Physics2D.OverlapCircleAll(punch.position, RadiusHeal, whatIsFriend);
		colliders[Random.Range(0, colliders.Length)].GetComponent<MobHP>().Heal(heal);
    }
    void HealStop(){
    	anim.SetBool("Heal", false);
    	if(enemyNear == false)
	        movement.ContinueMove();
    }
    void RestHeal(){
    	ismove = true;
    	canHeal = true;
    }

}