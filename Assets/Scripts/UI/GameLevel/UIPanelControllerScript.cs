using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIPanelControllerScript : MonoBehaviour{
	[SerializeField] private RectTransform panel;
	[SerializeField] private GameObject closeBtn; 
	public void Open(){
		gameObject.SetActive(true);
		panel.DOAnchorPos(new Vector2(0f, 0f), 0.25f).OnComplete(OnFinishOpen);
	}
	public void Close(){
		closeBtn.SetActive(false);
		panel.DOAnchorPos(new Vector2(0f, -Screen.height * 0.2f), 0.05f).OnComplete(OnFinishClose);
	}
	private void OnFinishClose(){
		gameObject.SetActive(false);
	}
	private void OnFinishOpen(){
		closeBtn.SetActive(true);
	}
}