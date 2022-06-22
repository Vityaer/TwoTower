using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScript : MonoBehaviour{
	
	[SerializeField] private int speed = 5;
	[Header("UI")]
	[SerializeField] private Image imageResource;
	[SerializeField] private Image background;
	[SerializeField] private Text countResource;
	public void SetData(Resource resource){
		if(background != null) background.enabled = true;
		imageResource.sprite = resource.sprite;
		countResource.text = resource.ToString();
		gameObject.SetActive(true);
	}
	public void Clear(){
		if(background != null) background.enabled = false;
		countResource.text = string.Empty;
		gameObject.SetActive(false);
	}
}