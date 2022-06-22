using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshObjectBehaviour : MonoBehaviour{
	protected Transform tr;
	protected Animator anim;
	protected Vector3 disablePosition = new Vector3(1000, 1000, 0);
	protected bool isDisable = false;
	public bool IsDisable{get => isDisable;}
	public virtual void Disable(){
		transform.position = disablePosition;
		isDisable = true;
	}
	public virtual void SetData(int layer, Vector3 position, Quaternion rotation){}
	public virtual void SetData(Transform target){}
}