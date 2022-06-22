using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MissionControllerScript : MonoBehaviour{
	private RectTransform btnSprite;
	private Vector2 startSize;
    [Header("Data")]
    [SerializeField] private Mission level;
    private Mission workLevel;
    public bool isFinish{get => level.IsFinish;}
    [Header("UI")]
    [SerializeField] private Button btn;
    [SerializeField] private RegionControllerScript region;
    void Start(){
        workLevel = Instantiate(level);
    	btnSprite = GetComponent<RectTransform>();
    	startSize = btnSprite.sizeDelta;
    }
    public void FinishMission(){
        workLevel.OnFinish();
        workLevel.Finish();
        gameObject.SetActive(false);
        region.OnCloseMission();
    } 
    public void Select(){ btnSprite.DOSizeDelta(startSize * 1.1f, 0.25f, true).OnComplete(UnSelect); OpenDetails(); }
    private void UnSelect(){ btnSprite.DOSizeDelta(startSize, 0.25f, true); }
    private void OpenDetails(){
    	MissionUIScript.Instance.ShowDataAboutMission(workLevel);
    }
    public void CheckCurrentMission(int ID){
        if(workLevel == null) workLevel = Instantiate(level);
        if(workLevel.ID == ID){
            if(workLevel.IsFinish){
                gameObject.SetActive(false);
                CampaignScript.Instance.OpenNextMission();
            }else{
                workLevel.OnOpen();
                workLevel.Open();
                gameObject.SetActive(true);
            }
        }else{
            gameObject.SetActive(false);
        }
    }
}
