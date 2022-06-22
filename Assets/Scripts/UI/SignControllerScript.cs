using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignControllerScript : MonoBehaviour{
	private Animator anim;
	public int dir = 1;
	public int dirMax = 3;
	public Transform coldPoint, hotPoint;
	public GameObject portal;

	public PortalScript portalScript;
    void Start(){
    	anim = GetComponent<Animator>();   
    }

    public void NextDirection(){
    	dir++;
    	if(dir == dirMax) dir = 0;
    	switch(dir){
    		case 0:
    			anim.Play("DirDown");
    			portalScript.SetPoint(hotPoint);
    			break;
    		case 1:
    			anim.Play("DirForward");
    			portalScript.SetPoint(null);
    			break;
    		case 2:
    			anim.Play("DirUp");
    			portalScript.SetPoint(coldPoint);
    			break;
    	}

    }
}
