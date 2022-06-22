using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using HelpFuction;
using System;
public class TowerControllerScript : MonoBehaviour{

	[SerializeField] private Text textHeigthTower, textGoldCount, textMPCount, textAnimalsCount, textGoldIncome, textMPIncome, textAnimalsIncome;
	int startHeight = 30;
	private int height = 30;
	[SerializeField] private ListResource listRes = new ListResource (
		new Resource(TypeResource.Gold, 15),
		new Resource(TypeResource.ManaPoint, 5),
		new Resource(TypeResource.Animals, 5)
	);
	public ListResource GetListRes{get => listRes;}
	[SerializeField] private ListResource listIncomeRes = new ListResource(
		new Resource(TypeResource.Gold, 1),
		new Resource(TypeResource.ManaPoint, 1),
		new Resource(TypeResource.Animals, 1)
	);
	[SerializeField] private MobCreaterScript MobCreater;
	public MobCreaterScript GetMobCreater{get => MobCreater;}
	GameTimer timerIncome;
	TimerScript Timer;
	[SerializeField] private CardsController cardsController; 
	public CardsController GetCardsController{get => cardsController;}
	[SerializeField] private MobHP hpController;
	[SerializeField] private Transform trTower;
	public Transform TowerTransform{get => trTower; }
	[SerializeField] private TowerVisualScript towerVisual;
	void Awake(){
		if(gameObject.layer == 8){ leftTower = this; }else{ rightTower = this;}
	}
	public void Start(){
        WinLoseControllerScript.Instance.RegisterOnFinishGame(StopGame);
		if(gameObject.layer == 8){ PrepareLeftTower(); }else{ PrepareRightTower(); }
		towerVisual.ChangeHeight(startHeight);
		hpController.StartHP = startHeight;
		height = startHeight;
		UpdateUI();
		Timer = TimerScript.Timer;
		timerIncome = Timer.StartTimer(timeIncome, PlayIncome);
	}

	public void ChangeHeight(int amount){
		height += amount;
		hpController.AddHP(amount);
		OnChangeHeightTower();
		UpdateUI();
	}
	private void UpdateUI(){
		try{
			textHeigthTower.text   = height.ToString();
			//Amount res
			textGoldCount.text     = listRes.GetResource(TypeResource.Gold).ToString();
			textMPCount.text       = listRes.GetResource(TypeResource.ManaPoint).ToString();
			textAnimalsCount.text  = listRes.GetResource(TypeResource.Animals).ToString();

			//Income
			textGoldIncome.text    = listIncomeRes.GetResource(TypeResource.Gold).ToString(); 
			textMPIncome.text      = listIncomeRes.GetResource(TypeResource.ManaPoint).ToString();
			textAnimalsIncome.text = listIncomeRes.GetResource(TypeResource.Animals).ToString();
		}catch{
			ChatControllerScript.PrintError("UI with problem");
		}
	}
	public void ChangeIncome(TypeResource type, int amount){
		listIncomeRes.GetResource(type).AddResource(amount);
		UpdateUI();
	}
	public void DecreaseResource(ListResource res){
		this.listRes.SubtractResource(res);
		UpdateUI();
	}
	public void IncreaseResource(ListResource res){
		this.listRes.AddResource(res);
		UpdateUI();	
	}
	[SerializeField] private float timeIncome = 12f;
	void PlayIncome(){
		listRes.AddResource(listIncomeRes);
		timerIncome = Timer.StartTimer(timeIncome, PlayIncome);
		UpdateUI();
	}
	public void CreateCards(int amount){ cardsController.CreateCard(amount); }
	[SerializeField] private BarracksControllerScript barracksController;
	public void SetDataInBarraks(GameObject prefab){Debug.Log("set data in barraks"); barracksController?.SetDataInBarraks(prefab);}
	public Action<int> observerHeight;
	public void RegisterOnHeightTower(Action<int> d){observerHeight += d;}
	public void UnregisterOnHeightTower(Action<int> d){observerHeight -= d;}
	private void OnChangeHeightTower(){if(observerHeight != null) observerHeight(height);}

	[Header("Magic Controller")]
	[SerializeField] private MagicControllerScript magicController;
	public void CreateMagic(GameObject prefab){ magicController.CreateMagic(prefab); }
	public void SetNewPosition(Vector3 pos){trTower.position = pos;}
	private static TowerControllerScript leftTower, rightTower;
	public static TowerControllerScript LeftTower{get => leftTower;}
	public static TowerControllerScript RightTower{get => rightTower;}


	public void RegiterOnChangeResource(Action<Resource> d){listRes.RegisterOnChangeResource(d, listRes);}
	public void UnregiterOnChangeResource(Action<Resource> d){listRes.UnRegisterOnChangeResource(d, listRes);}
//Prepare
	// Right tower
	private Baron baron;
	public void PrepareRightTower(){
		startHeight = WinLoseControllerScript.Instance.GetDefaultHeight(flagLeftTower: false);
		listRes        = WinLoseControllerScript.Instance.GetDefaultTowerResourse(flagLeftTower: false); 
		WinLoseControllerScript.Instance.CreateObserverResource(flagLeftTower: false);
		if(Information.Instance != null){
			Mission mision = Information.Instance.level;
			baron = BaronsControllerScript.Instance.GetBaron(mision.baron);
			GetMobCreater.SetNewAutoCreateWarrior(baron.startAutoPrefab);
			if(mision.hero.startAutoPrefab != null) GetMobCreater.SetNewAutoCreateWarrior(mision.hero.startAutoPrefab, mision.hero.timeStartAutoPrefab);
			cardsController.NewListCardForSale(baron.defaultCards);
			cardsController.AddExtraCards(mision.hero.extraCards);
			cardsController.AddInHand(mision.hero.inHandStartCards);
		}
		cardsController.GetStartCards(WinLoseControllerScript.Instance.GetDefaultCountCard);
	}
	//Left tower
	private PlayerHero playerHero;
	public void PrepareLeftTower(){
		startHeight = WinLoseControllerScript.Instance.GetDefaultHeight(flagLeftTower: true);
		listRes     = WinLoseControllerScript.Instance.GetDefaultTowerResourse(flagLeftTower: true); 
		WinLoseControllerScript.Instance.CreateObserverResource(flagLeftTower: true);
		if(Information.Instance != null){
			playerHero = Information.Instance.playerHero;
			cardsController.NewListCardForSale(playerHero.cardsForSale);
			GetMobCreater.SetNewAutoCreateWarrior(playerHero.startAutoPrefab);
			if(playerHero.temporarilyAutoPrefab != null) GetMobCreater.SetNewAutoCreateWarrior(playerHero.temporarilyAutoPrefab, playerHero.timeStartAutoPrefab);
		}
		cardsController.GetStartCards(WinLoseControllerScript.Instance.GetDefaultCountCard);
	}
	private void StopGame(){
		Timer.StopTimer(timerIncome);
	}
}