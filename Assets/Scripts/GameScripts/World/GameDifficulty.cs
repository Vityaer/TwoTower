using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDifficulty{
	[SerializeField] private LevelDifficultyMap levelDifficultyMap;
	[SerializeField] private int maxHeight, maxAmountResource, minAmountResource, defaultLeftTowerHeight, defaultRightTowerHeight, defaultCountCard;
	[SerializeField] private ListResource defaultLeftTowerResource, defaultRightTowerResource;
	public LevelDifficultyMap GetLevelDifficultyMap{get => levelDifficultyMap;}
	public int MaxHeight{get => maxHeight;} 
	public int MaxAmountResource{get => maxAmountResource;}
	public int MinAmountResource{get => minAmountResource;}
	public int DefaultLeftTowerHeight{get => defaultLeftTowerHeight;}
	public int DefaultRightTowerHeight{get => defaultRightTowerHeight;}
	public int DefaultCountCard{get => defaultCountCard;}
	public ListResource DefaultRightTowerResource{get => defaultRightTowerResource;}
	public ListResource DefaultLeftTowerResource{get => defaultLeftTowerResource;}
}
