using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HelpFuction;

public class MobHP : MonoBehaviour{
    	
    [SerializeField] private int startHP, defaultArmor;    
	public int StartHP{get => startHP; set => startHP = value;}
    private int HP, armor;
    [SerializeField] private SliderScript sliderHP;
	TowerControllerScript tower;
    void Awake(){
    	tower = GetComponent<TowerControllerScript>();
        Refresh();
    }
    public void GetDamage(int damage, GameObject master = null){
        damage = (damage > armor)  ? damage - armor : 0;
        if(damage > 0){
        	HP =  (HP > damage) ? HP - damage : 0;
            OnHit();
        	if(tower != null) { tower.ChangeHeight(- damage); master?.GetComponent<MobHP>()?.Kill(); }
        	if(HP <= 0){ Kill(); }
        }
    }
    public void Heal(int amount){ HP = ((HP + amount) >= startHP) ? startHP : HP + amount; UpdateUI();  }
    public void AddHP(int amount){ HP += amount; UpdateUI();}
    public Action observerHit, observerDeath;
    public void RegisterOnHit(Action d){observerHit += d;}
    public void UnregisterOnHit(Action d){observerHit -= d;}
    private void OnHit(){UpdateUI(); if(observerHit != null) observerHit();}
    public void RegisterOnDeath(Action d){observerDeath += d;}
    public void UnregisterOnDeath(Action d){observerDeath -= d;}
    private void OnDeath(){ if(observerDeath != null) observerDeath();}
    public void Refresh(){HP = startHP; armor = defaultArmor; RemoveBonusArmor(); sliderHP?.Refresh();}
    private UnitScript unitController;
    public void Kill(){
        if(unitController == null) unitController = GetComponent<UnitScript>();
        OnDeath();
        unitController?.Disable();
    }
    private void UpdateUI(){ sliderHP?.Fill((float) HP / (float) startHP); }
    GameTimer timerRemoveBonusArmor;
    public void SetBonusArmor(int bonus, float time){
        RemoveBonusArmor();
        armor = defaultArmor + bonus;
        timerRemoveBonusArmor = TimerScript.Timer.StartTimer(time, RemoveBonusArmor);
    }
    void RemoveBonusArmor(){
        if(timerRemoveBonusArmor != null) TimerScript.Timer.StopTimer(timerRemoveBonusArmor);
        armor = defaultArmor;
    }
}
