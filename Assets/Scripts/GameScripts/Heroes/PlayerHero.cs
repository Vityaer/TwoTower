using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHero{
	public string name;
	private List<Card> _cardsForSale = new List<Card>();
	public List<Card> cardsForSale{get => _cardsForSale;}
	public List<int> listIDCards = new List<int>();  
	public GameObject temporarilyAutoPrefab;
	public ListResource extraRes{get => baron.extraResources;}
	public ListResource extraMines{get => baron.extraMines;}
	public GameObject startAutoPrefab{get => baron.startAutoPrefab;}
	public int timeStartAutoPrefab;
	public ListResource res = new ListResource();
//Constructor
	public PlayerHero(string name){ 
		this.name = name;
	}
// API	
	public void ClearBuff(){
		temporarilyAutoPrefab = null;
		timeStartAutoPrefab = 0;
	}
	private void Pay(ListResource cost){ res.SubtractResource(cost); }
	public void NewCardsForSale(Card newCard, ListResource cost){
		cardsForSale.Add(newCard);
		Pay(cost);
		listIDCards.Add(newCard.ID);
		CampaignScript.Instance?.SaveGame();
	}
	public bool CheckCard(Card cardForCheck){ return listIDCards.Contains(cardForCheck.ID);}
	private bool prepared = false;
	private Baron baron;
	public void PreparePlayer(){
		if(prepared == false){
			_cardsForSale = new List<Card>();
			baron = BaronsControllerScript.Instance.GetBaron(BaronName.Player);
			_cardsForSale.AddRange(baron.defaultCards);
			prepared = true;
			Debug.Log(listIDCards.Count);
			for(int i = 0; i < listIDCards.Count; i++)
				_cardsForSale.Add( CardsScript.FindCardFromID(listIDCards[i] ) );
		}
	}
}
