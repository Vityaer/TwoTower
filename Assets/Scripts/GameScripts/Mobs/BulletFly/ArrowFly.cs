using UnityEngine;
using System.Collections;

public class ArrowFly : BulletFly {
    public override void StartFly(Transform target, GameObject master){
        GetComponents();
        this.master = master;
        this.target = target;
        ChangeDir();
        target.GetComponent<MobHP>().RegisterOnDeath(OnDeathTarget);
    }
}
