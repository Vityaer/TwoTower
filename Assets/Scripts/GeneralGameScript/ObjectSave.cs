using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game{
    public PlayerHero playerHero; 
	public List<Region> regions = new List<Region>(); 
	public int currentMission = 0;
}

[System.Serializable]
public class Region{
	public int ID;
	public BaronName baron;
}



public enum ProgressMission{
	Close,
	Open,
	Finish
}
public enum Difficulty{
	Training = 0,
	Easy = 1,
	Medium = 2,
	Hard = 3,
	VeryHard = 4,
	Impossiple = 5 
}
