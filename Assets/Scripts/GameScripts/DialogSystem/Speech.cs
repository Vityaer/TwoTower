using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Speech{
	[SerializeField] private List<Replica> line = new List<Replica>();
	private int currentLine = -1;
	public Replica NextReplica{ get{
			currentLine++;
			return line[currentLine]; 
		}
	}  
	[SerializeField] private Speecher speecher;
	public string GetSpeecher{get => speecher.ToString();}
	public bool IsFinish{get{ return ((currentLine + 1) == line.Count) ;}}
}
public enum Speecher{
	Rooster = 0,
	Rogue  = 1,
	Teacher  = 2,
	TeacherLeft = 3,
	Guy = 4,
	GirlWorry = 5,
	GirlIdle = 6,
	RoosterFlipX = 7,
	Archi = 8,
	Archer = 9,
	Shaman = 10,
	Berserk = 11,
	Dragon = 12,
	Smith = 13,
	Leo = 14,
	Sceleton = 15,
	Necromancer = 16,
	Hanger = 17,
	DeadKing = 18,
	DeadPriest = 19,
	CapitanShooters = 20,
	King = 21,
	Princess = 22,
	Warrior = 23 
}