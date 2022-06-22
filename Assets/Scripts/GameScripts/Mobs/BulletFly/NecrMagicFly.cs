using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrMagicFly : BulletFly{
    public GameObject prefabSceleton; 
    public int layerMaster;
    public override void StartFly(Transform target ,GameObject master){
        GetComponents();
        this.master = master;
        this.target = target;
        ChangeDir();
        target.GetComponent<MobHP>().RegisterOnDeath(OnDeathTarget);
    }

    Vector3 posTarget;
    protected override void OnDamage(){ posTarget = target.position; }

    protected override void OnDeathTarget(){
        PoolScript.Instance.GetRefreshUnit(prefabSceleton).SetData(layerMaster, posTarget, Quaternion.identity);  
    }

}
