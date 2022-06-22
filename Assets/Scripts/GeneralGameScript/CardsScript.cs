using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CardsScript : MonoBehaviour{
	private static List<Card> cards = new List<Card>();
	private static bool cardsLoaded = false;
	public static Card FindCardFromID(int id){
		if(cardsLoaded == false) LoadCards();
		return cards.Find(x => x.ID == id);
	}
	public static void LoadCards(){
		Object[] AllCards = Resources.LoadAll("ScriptableObjects", typeof(Card));
		for(int i = 0; i < AllCards.Length; i++){cards.Add((Card) AllCards[i]);}
		cardsLoaded = true;
	}
}