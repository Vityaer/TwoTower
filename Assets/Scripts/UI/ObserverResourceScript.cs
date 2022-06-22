using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverResourceScript : MonoBehaviour{
	[Header("General")]
	[SerializeField] private TypeResource typeResource;
	private Resource resource;

	[Header("UI")]
	[SerializeField] private Image imageResource;
	[SerializeField] private Text countResource;
	void Start(){
		resource             = new Resource(typeResource);
		imageResource.sprite = resource.sprite;
		Information.Instance.playerHero.res.RegisterOnChangeResource(UpdateUI, typeResource);
		UpdateUI(Information.Instance.playerHero.res.GetResource(typeResource));
	}

	public void UpdateUI(Resource res){
		if(res != null){
			resource = res;
			countResource.text = resource.ToString();
		}
	}
	void OnDestroy(){
		Information.Instance.playerHero.res.UnRegisterOnChangeResource(UpdateUI, typeResource);
	}
}
