using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardScript : MonoBehaviour{
	[Header("UI")]
	private ShopCard shopCard;
	[SerializeField] private Text textNameCard, textJewerlyCost, textSilkCost;
	[SerializeField] private Button btnComponent;
	[SerializeField] private GameObject showWasBuy;
	private bool wasBuy = false;
	public void SetData(ShopCard shopCard){
		this.shopCard = shopCard;
		wasBuy = CheckProduct();
		if(wasBuy == false){
			Information.Instance.playerHero.res.RegisterOnChangeResource(ChangeResource, shopCard.cost);
			ChangeResource();
		}else{
			showWasBuy.SetActive(true);
			btnComponent.interactable = false;
		}
		UpdateUI();
	}
	private void ChangeResource(Resource res = null){
		btnComponent.interactable = Information.Instance.playerHero.res.CheckResource(shopCard.cost); 
	}
	public void Buy(){
		if(Information.Instance.playerHero.res.CheckResource(shopCard.cost)){
			Information.Instance.playerHero.res.UnRegisterOnChangeResource(ChangeResource, shopCard.cost);
			AudioControllerScript.Instance?.PlaySoundClickOnButton();
			Information.Instance.AddCardInReserve(shopCard.product, shopCard.cost);
			btnComponent.interactable = false;
			showWasBuy.SetActive(true);
		}
	}
	private void UpdateUI(){
		textNameCard.text    = shopCard.name;
		textJewerlyCost.text = shopCard.cost.GetResource(TypeResource.Jewerly).ToString();
		textSilkCost.text    = shopCard.cost.GetResource(TypeResource.Silk).ToString();
	}
	bool CheckProduct(){ return Information.Instance.playerHero.CheckCard(shopCard.product); }
	void OnDestroy(){
		Information.Instance.playerHero.res.UnRegisterOnChangeResource(ChangeResource, shopCard.cost);
	}
}
