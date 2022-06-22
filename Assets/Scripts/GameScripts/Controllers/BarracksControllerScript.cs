using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksControllerScript : MonoBehaviour{
   	
 	public List<LevelBarraks> levels = new List<LevelBarraks>();
 	[SerializeField] private int currentLevel;
    [SerializeField] private List<BarraksActionScript> barrakcsWarriorAction = new List<BarraksActionScript>();  
    [SerializeField] private List<BarraksActionScript> barrakcsMagicAction = new List<BarraksActionScript>();  
    private List<BarraksActionScript> barrakcsAction = new List<BarraksActionScript>();  
 	private TowerControllerScript TowerController;
    void Start(){
        TowerController = GameControllerScript.Instance.GetTower();
        TowerController.RegisterOnHeightTower(OnChangeHeightTower);
        barrakcsAction.AddRange(barrakcsWarriorAction);
        barrakcsAction.AddRange(barrakcsMagicAction);
    }
    public void SetDataInBarraks(GameObject prefab){
        Debug.Log("set data in barraks controller" + prefab.name);
        FillCell(prefab.GetComponent<Data>().type, prefab);
    }
    public void OnChangeHeightTower(int height){
      	if((height < 50) &&(currentLevel != 0)){
      		ChangeCountAction(0);
      	}
      	if((height >= 50) && (height < 75) && (currentLevel != 1)){
      		ChangeCountAction(1);
      	}
      	if((height >= 75) && (currentLevel != 2)){
      		ChangeCountAction(2);
      	}
    }
    private void ChangeCountAction(int newLevel){
        int deltaWarrior = levels[newLevel].countWarrion - levels[currentLevel].countWarrion; 
        int deltaMagic   = levels[newLevel].countMagic - levels[currentLevel].countMagic; 
        ChangeCountCell(deltaWarrior, barrakcsWarriorAction);
        ChangeCountCell(deltaMagic, barrakcsMagicAction);
    }
    private void ChangeCountCell(int delta, List<BarraksActionScript> listAction){
        int num = 0;
        if(delta < 0){
            delta = -delta;
            for(int i = 0; i < listAction.Count; i++){
                if(listAction[i].IsEmptyWarrior){
                    listAction[i].Close(); 
                    num += 1;
                    if(num == delta) break;
                }
            }
            if(num < delta){
                for(int i = listAction.Count - 1; i >= 0; i--){
                    if(listAction[i].IsOpen){
                        listAction[i].Close();
                        num += 1;
                        if(num == delta) break;
                    }
                }
            }
        }else{
            for(int i = 0; i < listAction.Count; i++){
                if(listAction[i].IsOpen == false){
                    listAction[i].Open();
                    num += 1;
                    if(num == delta) break;
                }
            }
        }
    } 
//Fill
    private void FillCell(TypeBarrakcsAction typeCell, GameObject data){
        BarraksActionScript cell = null;
        switch(typeCell){
            case TypeBarrakcsAction.CreateWarrior:
                cell = barrakcsWarriorAction.Find(x => (x.IsEmpty == true));
                break;
            case TypeBarrakcsAction.Magic:
                cell = barrakcsWarriorAction.Find(x => (x.IsEmpty == true));
                break;  
            default:
                Debug.Log("none");
                break;
        }
        if(cell == null) Debug.Log("not found cell");
        cell?.SetData(data);
    } 
}
[System.Serializable]
public class LevelBarraks{
  public int countWarrion;
  public int countMagic;
  public int countAction{get => (countMagic + countWarrion);}
}