using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero{
	public BaronName Name;
	public List<Card> extraCards = new List<Card>();
	public List<Card> inHandStartCards = new List<Card>();
	public GameObject startAutoPrefab;
	public int timeStartAutoPrefab;
}