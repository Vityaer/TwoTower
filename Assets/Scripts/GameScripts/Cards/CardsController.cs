using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelpFuction;
public class CardsController : MonoBehaviour{

	public List<Card> cardForSale = new List<Card>();

	public ListResource costResource;
	public List<Card> cards = new List<Card>();
	public GameObject prefabCard;
	public Button btnComponent;
	public TowerControllerScript TowerController;
	GameTimer timerCreateCard;
	public int maxCardCount = 8;
	void Start(){
		timerCreateCard = TimerScript.Timer.StartTimer(20f, AutoCreateCards);
		if(btnComponent != null){
			TowerController.GetListRes.RegisterOnChangeResource(ChangeResource, costResource);
			ChangeResource(null);
		}
	}
	void AutoCreateCards(){
		if(cards.Count < maxCardCount){
			CreateCard(1);
			timerCreateCard = TimerScript.Timer.StartTimer(20f, AutoCreateCards);
		}
	}
	public void ChangeResource(Resource res = null){ btnComponent.interactable = TowerController.GetListRes.CheckResource(costResource); }
	public void BuyCard(){
		TowerController.DecreaseResource(costResource);
		CreateCard(1);
	}
	public void DeleteCard(Card card){cards.Remove(card);}
	public void InvokeCard(int num){
		cards[num].Invoke(TowerController);
		cards.RemoveAt(num);
	}
	public void InvokeCard(Card card){
		card.Invoke(TowerController);
		cards.Remove(card);
	}
	public void CreateCard(int count = 1){
		for(int i= 1; i <= count; i++){
			Card newCard = cardForSale[Random.Range(0, cardForSale.Count)]; 
			cards.Add(newCard);
			if(prefabCard != null){
				GameObject gameObjectCard = Instantiate(prefabCard, transform);
				gameObjectCard.GetComponent<CardController>().SetData(newCard);
			}
		}
	}
	public void AddExtraCards(List<Card> extraCards){cardForSale.AddRange(extraCards);}
	public void NewListCardForSale(List<Card> newCards){Debug.Log(newCards.Count); if(newCards.Count > 0) {cardForSale.Clear(); AddExtraCards(newCards);} }
	public void AddInHand(List<Card> newCards){cards.AddRange(newCards);}
	public void GetStartCards(int count = 3){if(count > 0) CreateCard(count);}
}