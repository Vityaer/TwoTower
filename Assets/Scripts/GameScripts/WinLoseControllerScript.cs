using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class WinLoseControllerScript : MonoBehaviour{
	[SerializeField] private RewardUIControllerScript rewardUI;
	[SerializeField] private List<GameDifficulty> listDifficulty = new List<GameDifficulty>();
	private GameDifficulty selectLevelDifficulty;
	public GameDifficulty SelectLevelDifficulty{get => (selectLevelDifficulty != null) ? selectLevelDifficulty : listDifficulty[listDifficulty.Count - 1]; }
	public TowerControllerScript leftTower, rightTower;
	public Canvas panelCanvas;
	public Text textResult;
	private static WinLoseControllerScript instance;
	public static WinLoseControllerScript Instance{get => instance;}
	void Awake(){ instance = this;  SetLevelDifficulty();}
	void Start(){
		leftTower.RegisterOnHeightTower(ChangeHeightLeftTower);
		rightTower.RegisterOnHeightTower(ChangeHeightRightTower);
	}
//Height	
	public void ChangeHeightLeftTower(int height){
		if(height >= SelectLevelDifficulty.MaxHeight){
			OpenPanelResults(win: true);
		}else if(height <= 0){
			OpenPanelResults(win: false);
		}
	}
	public void ChangeHeightRightTower(int height){
		if(height >= SelectLevelDifficulty.MaxHeight){
			OpenPanelResults(win: false);
		}else if(height <= 0){
			OpenPanelResults(win: true);
		}
	}
//Resouce
	public void ChangeResourceLeftTower(Resource res){
		if(res.Count >= SelectLevelDifficulty.MaxAmountResource){
			OpenPanelResults(win: true);
		}else if(res.Count <= selectLevelDifficulty.MinAmountResource){
			OpenPanelResults(win: false);
		}
	}
	public void ChangeResourceRightTower(Resource res){
		if(res.Count >= SelectLevelDifficulty.MaxAmountResource){
			OpenPanelResults(win: false);
		}else if(res.Count <= SelectLevelDifficulty.MinAmountResource){
			OpenPanelResults(win: true);
		}
	}

	public void OpenPanelResults(bool win){
		UnregisterAll();
		OnFinishGame();
		textResult.text     = win ? "Победа!" : "Поражение!";
		panelCanvas.enabled = true;
		if(win){
			if(Information.Instance){
				rewardUI.SetReward(Information.Instance.level.reward);
				Information.Instance.FinishLevel();
			}
		}

	}
	private void UnregisterAll(){
		leftTower.UnregisterOnHeightTower(ChangeHeightLeftTower);
		rightTower.UnregisterOnHeightTower(ChangeHeightRightTower);
		leftTower.UnregiterOnChangeResource(ChangeResourceLeftTower);
		rightTower.UnregiterOnChangeResource(ChangeResourceRightTower);


	}
	private void SetLevelDifficulty(){
		if(Information.Instance != null ){
			selectLevelDifficulty = listDifficulty.Find(x => x.GetLevelDifficultyMap == Information.Instance.GetLevelDifficult);
		}else{
			selectLevelDifficulty = listDifficulty[listDifficulty.Count - 1];
		}
	}

//API
	public int GetDefaultCountCard{ get => SelectLevelDifficulty.DefaultCountCard;}
	public int GetDefaultHeight(bool flagLeftTower){ return (flagLeftTower) ? SelectLevelDifficulty.DefaultLeftTowerHeight : SelectLevelDifficulty.DefaultRightTowerHeight;}
	public ListResource GetDefaultTowerResourse(bool flagLeftTower){ return (flagLeftTower) ? SelectLevelDifficulty.DefaultLeftTowerResource : SelectLevelDifficulty.DefaultRightTowerResource;}
	public void CreateObserverResource(bool flagLeftTower){
		if(flagLeftTower){
			leftTower.RegiterOnChangeResource(ChangeResourceLeftTower);
		}else{
			rightTower.RegiterOnChangeResource(ChangeResourceRightTower);
		}
	}
//Finish game
	public Action observerFinishGame;
	public void RegisterOnFinishGame(Action d){observerFinishGame += d;}
	public void UnRegisterOnFinishGame(Action d){observerFinishGame -= d;}
	private void OnFinishGame(){if(observerFinishGame != null) observerFinishGame();}

}