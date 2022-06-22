using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelpFuction;
public class CardController : MonoBehaviour{

	public Card data;
	public Text textCost;
	public Image imageCost;
	public Text textCard;
	private Button btnComponent;
	private TowerControllerScript TowerController;
	void Start(){
		UpdateUI();
		btnComponent = GetComponent<Button>();
		TowerController = GameControllerScript.Instance.GetTower();
		TowerController.GetListRes.RegisterOnChangeResource(ChangeResource, data.costResource);
		ChangeResource(null);
	}
	public void ChangeResource(Resource res = null){
		btnComponent.interactable = TowerController.GetListRes.CheckResource(data.costResource); 
	}
	void Invoke(){
		data.Invoke(TowerController);
		Destroy(gameObject);
	}

	void DeleteCard(){
		TowerController.GetCardsController.DeleteCard(data);
		Destroy(gameObject);
	}
	public void SetData(Card data){
		this.data = data;
		UpdateUI();
	}
	private void UpdateUI(){
		if((data.costResource.GetResource(0)) != null ){
			textCost.text    = data.costResource.GetResource(0).ToString();
			imageCost.sprite = data.costResource.GetResource(0).sprite;
		}else{
			imageCost.enabled = false;
			textCost.text = string.Empty;
		}
		textCard.text    = data.description;
	}
	void OnDestroy(){
		TowerController.GetListRes.UnRegisterOnChangeResource(ChangeResource, data.costResource);
	}
	int countClick = 0;
	GameTimer timerDoubleClick = null;
	public void ClickOnCard(){
		AudioControllerScript.Instance.PlaySoundClickOnButton();
		countClick++;
		if(timerDoubleClick == null)
			timerDoubleClick = TimerScript.Timer.StartTimer(0.3f, ClearDoubleClick);
		if(countClick == 2){
			DeleteCard();
		}
	}
	private void ClearDoubleClick(){
		countClick = 0;
		timerDoubleClick = null;
	}
	public void StartInvoke(){
		if(btnComponent.interactable){
			if(coroutineInvoke != null) StopCoroutine(coroutineInvoke);
			coroutineInvoke = null;
			coroutineInvoke = StartCoroutine(StartFillCircleInvoke());	
		}
	}
	public void BreakInvoke(){
		if(coroutineInvoke != null){
			StopCoroutine(coroutineInvoke);
		}
		circleInvoke.fillAmount = 0f;
		coroutineInvoke = null;
	}
	Coroutine coroutineInvoke;
	public Image circleInvoke;
	IEnumerator StartFillCircleInvoke(){
		float fill = 0f;
		while(fill <= 1f){
			circleInvoke.fillAmount = fill;
			fill += Time.deltaTime;
			yield return null;
		}
		Invoke();
	}
}