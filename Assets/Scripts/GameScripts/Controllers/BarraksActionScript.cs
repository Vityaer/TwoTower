using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraksActionScript : MonoBehaviour{
	[Header("Data")]
    [SerializeField] private TypeBarrakcsAction typeCell;
	private ListResource cost = null; 
	private GameObject prefab;
	[SerializeField] private TowerControllerScript TowerController;
	[Header("UI")]
	[SerializeField] private RewardScript costScript;
    [SerializeField] private Image imagePrefab; 
	[SerializeField] private Button btnComponent;
    [SerializeField] private Sprite spriteWarriorEmpty, spriteMagicEmpty;
    void Start(){
    	TowerController = GameControllerScript.Instance.GetTower();
        DefaultUI();
    	if(prefab != null) {
	    	TowerController.GetListRes.RegisterOnChangeResource(ChangeResource, cost);   
    		UpdateUI();
    	}
    }

    public void ChangeResource(Resource res = null){
		btnComponent.interactable = TowerController.GetListRes.CheckResource(cost); 
    }
    public void Invoke(){
        if(prefab != null){
        	switch(typeCell){
        		case TypeBarrakcsAction.CreateWarrior:
        			GameControllerScript.Instance.CreateWarrior(TowerController, Side.Myself, prefab);
        			break;
        		case TypeBarrakcsAction.Magic:
    	    		Debug.Log("magic spell!");
        			break;	
        		case TypeBarrakcsAction.None:
        			Debug.Log("Empty");
        			break;	
        	}
        	if(typeCell != TypeBarrakcsAction.None)
    	    	GameControllerScript.Instance.SubstractResource(TowerController, cost);
        }
    }
    public void SetData(GameObject prefab){
        Debug.Log("Set data");
    	Clear();
    	this.prefab     = prefab;
    	this.typeCell = prefab.GetComponent<Data>().type;
    	this.cost       = (ListResource) prefab?.GetComponent<Data>().res?.Clone();
    	if(TowerController == null) TowerController = GameControllerScript.Instance.GetTower();
		TowerController.GetListRes.RegisterOnChangeResource(ChangeResource, cost);        

    	UpdateUI();
    }
    void UpdateUI(){
    	if((cost?.GetResource(0)) != null ){
            costScript.SetData(cost.GetResource(0));
			imagePrefab.sprite = prefab?.GetComponent<Data>().sprite;
		}
    }
    public void Clear(){
        if(cost != null) TowerController?.GetListRes.UnRegisterOnChangeResource(ChangeResource, cost);
        DefaultUI();
	}
	public void Close(){
		Clear();
        isOpen = false;
		gameObject.SetActive(false);
	}
    private void DefaultUI(){
        costScript.Clear();
        switch(typeCell){
            case TypeBarrakcsAction.CreateWarrior:
                Debug.Log("warrior");   
                imagePrefab.sprite = spriteWarriorEmpty;
                break;
            case TypeBarrakcsAction.Magic:
                Debug.Log("magic");   
                imagePrefab.sprite = spriteMagicEmpty;
                break; 
            default:  
                Debug.Log("default");   
                break;  
        }
    }
    private bool isOpen;
	public void Open(){isOpen = true; gameObject.SetActive(true);}
    public bool IsOpen{get => isOpen;}
    public bool IsEmptyWarrior{get => (IsEmpty && IsCellWarrior);}
    public bool IsEmptyMagic{get => (IsEmpty && IsCellMagic);}
    public bool IsEmpty{ get => (prefab == null); }
    public bool IsCellWarrior{ get => (typeCell == TypeBarrakcsAction.CreateWarrior); }
    public bool IsCellMagic{ get => (typeCell == TypeBarrakcsAction.Magic); }
}
public enum TypeBarrakcsAction{
	CreateWarrior,
	Magic,
	None
}