using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour{

	private static Information instance;
	public static Information Instance{get => instance;}
    void Awake(){
    	if(instance == null){
	        instance = this;
	    	DontDestroyOnLoad(gameObject);
            CardsScript.LoadCards();
    	}else{
    		Destroy(gameObject);
    	}
    }
    public Mission level;
    private PlayerHero _playerHero = null;
    public PlayerHero playerHero{get => _playerHero; set => _playerHero = value;} 
    public void FinishLevel(){
        if(playerHero == null) Debug.Log("player null");
        playerHero.res.AddResource(level.reward);
        playerHero.ClearBuff();
        CampaignScript.Instance.FinishCurrentLevel();
    }
    public LevelDifficultyMap GetLevelDifficult{get => level.levelDifficultyMap;}
    public void AddCardInReserve(Card card, ListResource cost){ playerHero.NewCardsForSale(card, cost); }
}

