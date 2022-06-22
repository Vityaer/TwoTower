using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
public class BaseAIScipt : MonoBehaviour{

	public TowerControllerScript towerController;
	public CardsController cardsController;
	GameTimer restTimer;
	bool flagAction = true;
	List<Card> workList = new List<Card>();
	void Start(){
		towerController.GetListRes.RegisterOnChangeResource(ChangeResource, towerController.GetListRes);
	}
	void ChangeResource(Resource res = null){
		if(flagAction){
			flagAction = false;
			if(cardsController.cards.Count == 0){
				if(towerController.GetListRes.CheckResource(cardsController.costResource)){
					Debug.Log("купил карту");
					cardsController.BuyCard();
				}
			}else{
				Debug.Log("выбираем карту");
				workList = cardsController.cards.FindAll(x => (towerController.GetListRes.CheckResource(x.costResource) == true) );
				if(workList.Count > 0){
					Card selectedCard = workList[Random.Range(0, workList.Count)];
					Debug.Log("использовал карту " + selectedCard.description);
					cardsController.InvokeCard(selectedCard);
				}
			}
			restTimer = TimerScript.Timer.StartTimer(1f, DoneWork);
		}
	}
	void DoneWork(){
		flagAction = true;
	}

}