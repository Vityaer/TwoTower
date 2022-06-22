using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaronsControllerScript : MonoBehaviour{
	[SerializeField] private List<Baron> barons = new List<Baron>();
	public Baron GetBaron(BaronName name){ return barons.Find(x => x.name == name); }
	private static BaronsControllerScript instance;
	public static BaronsControllerScript Instance{get => instance;}
	void Awake(){ instance = this; }
}

[System.Serializable]
public class Baron{
	public BaronName name;
	public Sprite avatar;
	public Color color;
	public Difficulty difficulty;
	public List<Card> defaultCards = new List<Card>();
	public GameObject startAutoPrefab;
	public ListResource extraResources, extraMines;
}
public enum BaronName{
	Player = 0,
	Assasin = 1,
	Knight = 2,
	Daemon = 3,
	Necromant = 4,
	Barbarian = 5,
	BarbarianWoman = 6,
	Druid = 7,
	Mechanic = 8,
	Boar = 9,
	Snake = 10,
	Priest = 11
}