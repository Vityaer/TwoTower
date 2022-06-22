using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour{
    public LayerMask layerTeleportation;
	public Transform point;
	public SignControllerScript signSript;
    public void SetPoint(Transform point){
    	if(point != null){
    		gameObject.SetActive(true);
	    	this.point = point;
    	}else{
    		gameObject.SetActive(false);
    	}
    } 

   	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.GetComponent<UnitScript>()){
			if((layerTeleportation.value & (1 << other.gameObject.layer)) != 0){
				other.gameObject.transform.position = point.position;
			}
		}
	}
}
