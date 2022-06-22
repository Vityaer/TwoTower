
using UnityEngine;
using System.Collections;

internal class MobFight2D : MonoBehaviour{
    // point - точка контакта
    // radius - радиус поражения
    // layerMask - номер слоя, с которым будет взаимодействие
    // damage - наносимый урон
    // allTargets - должны-ли получить урон все цели, попавшие в зону поражения
    public static void Action(Vector2 point, float radius, LayerMask layerMask, int damage, bool isFacingLeft, GameObject master){
        Action(point, radius, layerMask, damage, isFacingLeft, false, master);
    }
    public static void Action(Vector2 point, float radius, LayerMask layerMask, int damage, bool isFacingLeft, bool stunning, GameObject master){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);
        GameObject obj = NearTarget(point, colliders);
        if(obj != null){
            obj.GetComponent<MobHP>()?.GetDamage(damage, master);
            if (stunning){stun();}
        }
    }
    public static void StrikeAll(Vector2 point, float radius, LayerMask layerMask, int damage, bool isFacingLeft, bool stunning, GameObject master = null){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);
        for(int i = 0; i < colliders.Length; i++){
            colliders[i].GetComponent<MobHP>()?.GetDamage(damage, master);
            if (stunning){stun();}
        }
    }
    public static void stun() {
        
    }

    public static GameObject NearTarget(Vector3 position, Collider2D[] array){
        Collider2D current = null;
        GameObject result = null;
        float dist = Mathf.Infinity;
        foreach (Collider2D coll in array){
            if(coll.GetComponent<MobHP>()){
                float curDist = Vector3.Distance(position, coll.transform.position);
                if (curDist < dist){
                    current = coll;
                    dist = curDist;
                }
            }
        }
        if(current != null) result = current.gameObject;
        return result;
    }
    public static GameObject NearTargetWithIgnore(Vector3 position, Collider2D[] array, Transform transformIngone){
        Collider2D current = null;
        GameObject result = null;
        float dist = Mathf.Infinity;
        foreach (Collider2D coll in array){
            if(coll.GetComponent<MobHP>() && coll.transform != transformIngone){
                float curDist = Vector3.Distance(position, coll.transform.position);
                if (curDist < dist){
                    current = coll;
                    dist = curDist;
                }
            }
        }
        if(current != null) result = current.gameObject;
        return result;
    }
}