using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerVisualScript : MonoBehaviour{
	[SerializeField] private Transform headTower;
   	private int height;
	[SerializeField] private List<StageTower> stages = new List<StageTower>();
    public void ChangeHeight(int newHeight){
    	for(int i = 0; i < stages.Count; i++){ stages[i].ChangeHeight(newHeight); }
		headTower.position = StageTower.GetHeadPos.position;
		height = newHeight;
    }
}
[System.Serializable]
public class StageTower{
	public static Transform GetHeadPos;
	[SerializeField] private GameObject stage;
	[SerializeField] private int minHeight;
	[SerializeField] private Transform headPos;
	private bool show = true;
	public void ChangeHeight(int height){
		if(height >= minHeight){
			GetHeadPos = headPos;
			if(show == false){
				show = true;
				stage.SetActive(true);
			}
		}else{
			if(show == true){
			show = false;	
			stage.SetActive(false);
			}
		}
	}
}
