using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour{

	public TowerControllerScript leftTower, rightTower;
	public bool leftPlayer = true;

	private static GameControllerScript instance;
	public static GameControllerScript Instance{get => instance;}
	void Awake(){
		instance = this;
	 }

	public TowerControllerScript GetTower(){
		if(leftPlayer == true){
			return leftTower;
		}else{
			return rightTower;
		}
	}
//API
	public void SubstractResource(TowerControllerScript tower, ListResource res){
		tower.DecreaseResource(res);
	}
	public void CreateWarrior(TowerControllerScript tower, Side side, GameObject prefab){
		tower = DefineTower(tower, side);
		tower.GetMobCreater.CreateMob(prefab);
	}
	public void CreateMagic(TowerControllerScript tower, GameObject prefab){
		tower.CreateMagic(prefab);
	}
	public void ChangeHeigtTower(TowerControllerScript tower, Side side, int amount){
		tower = DefineTower(tower, side);
		tower.ChangeHeight(amount);
	}
	public void ChangeHeigtWall(TowerControllerScript tower, Side side, int amount){
		tower = DefineTower(tower, side);
		if(side == Side.Myself){
			// leftTower.ChangeHeight(amount);
		}else{
			// rightTower.ChangeHeight(amount);
		}	
	}
	public void ChangeIncomeGold(TowerControllerScript tower, Side side, int amount){
		tower = DefineTower(tower, side);
		tower.ChangeIncome(TypeResource.Gold, amount);
	}
	public void ChangeIncomeManaPoint(TowerControllerScript tower, Side side, int amount){
		tower = DefineTower(tower, side);
		tower.ChangeIncome(TypeResource.ManaPoint, amount);
	} 
	public void ChangeIncomeAnimals(TowerControllerScript tower, Side side, int amount){
		tower = DefineTower(tower, side);
		tower.ChangeIncome(TypeResource.Animals, amount);
	} 
	public void DecreaseResource(TowerControllerScript tower, Side side, ListResource res){
		tower = DefineTower(tower, side);
		tower.DecreaseResource(res);
	}
	public void IncreaseResource(TowerControllerScript tower, Side side, ListResource res){
		tower = DefineTower(tower, side);
		tower.IncreaseResource(res);
	}
	public void CreateCards(TowerControllerScript tower, Side side, int amount){
		tower = DefineTower(tower, side);
		tower.CreateCards(amount);
	}
	public void SetDataInBarraks(TowerControllerScript tower, Side side, GameObject prefab){
		Debug.Log("set data in barraks");
		tower = DefineTower(tower, side);
		tower.SetDataInBarraks(prefab);
	}
	public void SetNewAutoCreate(TowerControllerScript tower, Side side, GameObject prefab, int amountSecond){
		tower = DefineTower(tower, side);
		tower.GetMobCreater.SetNewAutoCreateWarrior(prefab, amountSecond);
	}
//Define Tower	
	private TowerControllerScript DefineTower(TowerControllerScript tower, Side side){
		if(side != Side.Myself){if(tower == leftTower) {tower = rightTower;} else {tower = leftTower;}}
		return tower;
	}
}
