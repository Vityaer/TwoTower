using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSheetScript : MonoBehaviour{
	public static ShopSheetScript currentShowcase;
	[SerializeField] protected GameObject showcase;
	[SerializeField] protected Transform content;
	public virtual void Open(){showcase.SetActive(true);}
	public virtual void Close(){ showcase.SetActive(false);}
}