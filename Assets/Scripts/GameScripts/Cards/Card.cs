using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Custom ScriptableObject/Card", order = 51)]
[System.Serializable]
public class Card : ScriptableObject{
	public string name;
	public int ID;
	public ListResource costResource;
	public string description;
	public List<CardAction> actions = new List<CardAction>();
	public void Invoke(TowerControllerScript TowerController){
		foreach(CardAction action in actions){
			action.Invoke(TowerController);
		}
		GameControllerScript.Instance.SubstractResource(TowerController, costResource);
	}
}

[System.Serializable]
public class CardAction{
	public Side whom;
	public TypeActionCard typeAction;
	public GameObject prefab;
	public int amount;
	public ListResource res;
	public void Invoke(TowerControllerScript TowerController){
		switch(typeAction){
			case TypeActionCard.CreateWarrior:
				GameControllerScript.Instance.CreateWarrior(TowerController, whom, prefab);
				break;
			case TypeActionCard.ChangeHeigtTower:
				GameControllerScript.Instance.ChangeHeigtTower(TowerController, whom, amount);
				break;	
			case TypeActionCard.ChangeHeigtWall:
				GameControllerScript.Instance.ChangeHeigtWall(TowerController, whom, amount);
				break;	
			case TypeActionCard.ChangeIncomeGold:
				GameControllerScript.Instance.ChangeIncomeGold(TowerController, whom, amount);
				break;	
			case TypeActionCard.ChangeIncomeManaPoint:
				GameControllerScript.Instance.ChangeIncomeManaPoint(TowerController, whom, amount);
				break;	
			case TypeActionCard.ChangeIncomeAnimals:
				GameControllerScript.Instance.ChangeIncomeAnimals(TowerController, whom, amount);
				break;
			case TypeActionCard.DecreaseResource:
				GameControllerScript.Instance.DecreaseResource(TowerController, whom, res);
				break;
			case TypeActionCard.IncreaseResource:
				GameControllerScript.Instance.IncreaseResource(TowerController, whom, res);
				break;
			case TypeActionCard.GetCards:
				GameControllerScript.Instance.CreateCards(TowerController, whom, amount);		
				break;
			case TypeActionCard.SetItemInBarraks:
				GameControllerScript.Instance.SetDataInBarraks(TowerController, whom, prefab);
				break;	
			case TypeActionCard.CreateMagic:
				GameControllerScript.Instance.CreateMagic(TowerController, prefab);
				break;
			case TypeActionCard.SetNewAutoCreate:
				GameControllerScript.Instance.SetNewAutoCreate(TowerController, whom, prefab, amount);
				break;
			case TypeActionCard.EqualMagic:
				break;						
		}
	}

}
public enum TypeActionCard{
	CreateWarrior = 0,
	ChangeHeigtTower  = 1,
	ChangeHeigtWall = 2,
	ChangeIncomeGold = 3,
	ChangeIncomeManaPoint = 4,
	ChangeIncomeAnimals = 5,
	DecreaseResource = 6,
	IncreaseResource = 7,
	GetCards = 8,
	SetItemInBarraks = 9,
	CreateMagic = 10,
	SetNewAutoCreate = 11,
	EqualMagic = 12
}
public enum Side{
	Myself,
	Opponent
}