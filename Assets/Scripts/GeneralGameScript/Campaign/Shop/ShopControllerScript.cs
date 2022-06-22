using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControllerScript : MonoBehaviour{
	[SerializeField] private ObserverResourceScript jewerlyObserver, silkObserver;

	public void CloseShop(){gameObject.SetActive(false);}
	public void OpenShop(){gameObject.SetActive(true);}
}
