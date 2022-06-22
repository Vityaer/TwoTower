using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowcaseControllerScript : ShopSheetScript{
	[SerializeField] List<ShopCard> shopCards = new List<ShopCard>();
	[SerializeField] GameObject prefabCard;
	List<ShopCardScript> products = new List<ShopCardScript>();
	public bool IsOpen = false;
	void Awake(){CreateCards();}
	
	void Start(){if(IsOpen) currentShowcase = this;}
	public void OnClickButton(){
		currentShowcase?.Close();
		currentShowcase = this;
		Open();
	}
	private void CreateCards(){
		ShopCardScript shopCardScript = null;
		for(int i = 0; i < shopCards.Count; i++){
			shopCardScript = Instantiate(prefabCard, content).GetComponent<ShopCardScript>();
			products.Add(shopCardScript);
			shopCardScript.SetData(shopCards[i]);
		}
	}
}