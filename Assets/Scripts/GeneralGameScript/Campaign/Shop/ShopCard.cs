using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopCard", menuName = "Custom ScriptableObject/ShopCard", order = 52)]
[System.Serializable]
public class ShopCard : ScriptableObject{
	public string name;
	public Card product;
	public ListResource cost = new ListResource();

}