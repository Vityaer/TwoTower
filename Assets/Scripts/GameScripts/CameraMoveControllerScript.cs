using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraMoveControllerScript : MonoBehaviour{
	private Transform tr;
	private static CameraMoveControllerScript instance;
	public static CameraMoveControllerScript Instance{get => instance;}
	List<ListTransform> matrixPosition = new List<ListTransform>();
	[SerializeField] List<LevelListTransform> matrixPositionLevels = new List<LevelListTransform>();
	private CurrentPos currentPos = new CurrentPos();
	void Awake(){
		instance = this;
		tr = GetComponent<Transform>();
	}
	private Vector3 startPosition, endPosition;
	public float speed = 1f;
	private Vector2 dragDistance = new Vector2(), dir = new Vector2();
	void Start(){
		dragDistance.x = Screen.height * 0.3f;
		dragDistance.y = Screen.width * 0.2f;
		ChangeLevelHighCamera(currentLevel);
	}
	RaycastHit2D hit;
	void Update(){
		if (Input.GetMouseButtonDown(0)){
			startPosition  = Input.mousePosition;
	        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	hit = Physics2D.Raycast(CurMousePos, Vector2.zero);
			if (hit != null){
				if(hit.transform != null){
					SelectBehavior(hit.transform);
	            	Debug.Log("попали в объект " + hit.transform.name);
				}else{
	            	OnTouchMap(CurMousePos);
	            	Debug.Log("нет transform");
				}
            }else{
            	OnTouchMap(CurMousePos);
	            	Debug.Log("нет transform");
            }
		}
		if (Input.GetMouseButtonUp(0)){
			dir = Vector2.zero;
			endPosition  = Input.mousePosition;
			if(Mathf.Abs(endPosition.x - startPosition.x) > dragDistance.x)
				if(endPosition.x > startPosition.x){currentPos.ToLeft();}else{currentPos.ToRight();}
			if(Mathf.Abs(endPosition.y - startPosition.y) > dragDistance.y)
				if(endPosition.y > startPosition.y){currentPos.ToDown();}else{currentPos.ToUp();}

			tr.position = (matrixPosition[currentPos.y].list[currentPos.x].position);	
		}
	}

	private void SelectBehavior(Transform tr){
		if(tr.CompareTag("Mob")) OnSelectMob(tr);
		if(tr.CompareTag("LeftWay")) WaysControllerSctipt.Instance.SelectWay(TypeWay.Left);
		if(tr.CompareTag("MiddleWay")) WaysControllerSctipt.Instance.SelectWay(TypeWay.Middle);
		if(tr.CompareTag("RightWay")) WaysControllerSctipt.Instance.SelectWay(TypeWay.Right);
	}
	public Action<Transform> observerSelectMob;
	public void RegisterOnSelectMob(Action<Transform> d){Debug.Log("add "); observerSelectMob += d;}
	public void UnRegisterOnSelecMob(Action<Transform> d){observerSelectMob -= d;}
	private void OnSelectMob(Transform tr){ if(observerSelectMob != null){
			AudioControllerScript.Instance.PlaySoundClickOnButton();
			observerSelectMob(tr); 
		}
	}	

	public Action<Vector3> observerTouchMap;
	public void RegisterOnTouchMap(Action<Vector3> d){observerTouchMap += d;}
	public void UnRegisterOnTouchMap(Action<Vector3> d){observerTouchMap -= d;}
	private void OnTouchMap(Vector3 pos){ if(observerTouchMap != null){ 
			AudioControllerScript.Instance.PlaySoundClickOnButton();
			observerTouchMap(pos);
		} 
	}



	[SerializeField] int currentLevel = 1;
	private void ChangeLevelHighCamera(int newLevelHigh){
		matrixPositionLevels[currentLevel].StopCamera();
		currentLevel = newLevelHigh;
		matrixPositionLevels[currentLevel].StartCamera();
		matrixPosition = matrixPositionLevels[currentLevel].list;
		currentPos.MaxY = matrixPosition.Count;
		currentPos.MaxX = matrixPosition[0].list.Count;
		tr.position = (matrixPosition[currentPos.y].list[currentPos.x].position);	
	}
	public void ZoomDown(){
		if(currentLevel > 0){
			AudioControllerScript.Instance.PlaySoundClickOnButton();
			ChangeLevelHighCamera(currentLevel - 1);
		}
	}
	public void ZoomUp(){
		if(currentLevel < matrixPositionLevels.Count - 1){
			AudioControllerScript.Instance.PlaySoundClickOnButton();
			ChangeLevelHighCamera(currentLevel + 1);
		}

	}

}
public class CurrentPos{
	private int maxX, maxY;
	public int MaxX{get => maxX; set{ maxX = value; if(_x >= maxX) _x = maxX - 1;}}
	public int MaxY{get => maxY; set{ maxY = value; if(_y >= maxY) _y = maxY - 1;}}
	private int _x, _y;
	public int x{get => _x;}
	public int y{get => _y;}
	public void ToLeft(){if(_x > 0) _x -= 1;}
	public void ToRight(){if(_x < maxX - 1) _x += 1;}
	public void ToDown(){ if(_y > 0) _y -= 1;}
	public void ToUp(){ if(_y < maxY - 1) _y += 1;}

}
[System.Serializable]
public class ListTransform{
	[SerializeField] private List<Transform> _list = new List<Transform>();
	public List<Transform> list{get => _list;} 
}
[System.Serializable]
public class LevelListTransform{
	[SerializeField] private List<ListTransform> _list = new List<ListTransform>();
	public List<ListTransform> list{get => _list;} 
	[SerializeField] private GameObject camera;
	public void StartCamera(){camera.SetActive(true);}
	public void StopCamera(){camera.SetActive(false);}
}