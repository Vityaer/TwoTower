using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicControllerScript : MonoBehaviour{
    
    public Transform createPoint;
    
    public void CreateMagic(GameObject prefab){
    	PoolScript.Instance.GetRefreshOther(prefab).SetData(gameObject.layer,  createPoint.position, createPoint.rotation);	
    }

}
