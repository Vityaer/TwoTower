using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogAction{
	public TypeDialogAction type;
	public List<CaptureRegion> regions = new List<CaptureRegion>();
	public ListResource resources = new ListResource();
	public void Start(){
		switch(type){
			case TypeDialogAction.CaptureRegion:
				for(int i = 0; i < regions.Count; i++){
					CampaignScript.Instance.CaptureRegion(regions[i]);
				}
				break;
			case TypeDialogAction.AddResource:
				break;
			case TypeDialogAction.SubstactResource:
				break;		
		}
	}
}
public enum TypeDialogAction{
	CaptureRegion,
	AddResource,
	SubstactResource
}
