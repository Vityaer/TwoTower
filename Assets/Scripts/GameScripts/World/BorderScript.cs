using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BorderScript : MonoBehaviour{
	private Baron baron;
	[SerializeField] private RegionControllerScript neiborthRegion;
	[SerializeField] private Image image;
	public void SetBaron(Baron baron){
		if(baron.name == neiborthRegion.GetBaron.name){
			HideBorder();
		}else{
			if(this.baron != null){
				ClearTerritory();
			}else{
				this.baron = baron;
				FillTerritory();
			}
		}
		this.baron = baron;
	}
	public void RecalculateBorder(){SetBaron(this.baron);}
	void ClearTerritory(){ image.DOColor(Color.white, 1f).OnComplete(FillTerritory); }
	void FillTerritory(){ image.DOColor(baron.color, 1f); }
	void HideBorder(){ image.DOColor(new Color(0,0,0,0), 1f); }
	void Start(){ neiborthRegion.RegisterOnChangeBaron(RecalculateBorder); }
}