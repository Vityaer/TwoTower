using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUIControllerScript : MonoBehaviour{
	[SerializeField] private GameObject prefabReward;
	[SerializeField] private Transform content;
	public void SetReward(ListResource resourses){
		gameObject.SetActive(true);
		for(int i = 0; i < resourses.Count; i++){
			Instantiate(prefabReward, content).GetComponent<RewardScript>().SetData(resourses.List[i]);
		}
	}
	void Awake(){instance = this;}
	private static RewardUIControllerScript instance;
	public static RewardUIControllerScript Instance{get => instance;}
}