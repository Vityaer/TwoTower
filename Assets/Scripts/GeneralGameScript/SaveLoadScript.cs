using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using UnityEditor;
internal class SaveLoadScript : MonoBehaviour{
	private static string NameFileGame    = "FileGame.data";
	public static void SaveGame(Game game){
			Debug.Log("save...");
	    	StreamWriter sw = CreateStream(NameFileGame, false);
		    sw.WriteLine( JsonUtility.ToJson(game) );
			sw.Close();
			Debug.Log("save finish");
		}

	public static void ClearFile(){
		Debug.Log("clear");
    	StreamWriter sw = CreateStream(NameFileGame, false);
		sw.Close();
	}
	public static void LoadGame(Game game){
		List<string> rows = ReadFile(NameFileGame);
		if(rows.Count > 0) JsonUtility.FromJsonOverwrite(rows[0], game);
	}

//Core	
	private static string GetPrefix(){
			string prefixNameFile;
		#if UNITY_EDITOR_WIN
			prefixNameFile = Application.dataPath;	
	    #endif
	    #if UNITY_ANDROID && !UNITY_EDITOR
			prefixNameFile = Application.persistentDataPath;	
	    #endif
			return prefixNameFile;
	}
	private static List<string> ReadFile(string NameFile){
		CheckFile(NameFile);
		List<string> ListResult = new List<string>(); 
		try{
			ListResult = new List<string>(File.ReadAllLines(Path.Combine (GetPrefix(), NameFile) ));
		}catch{}
		return ListResult;
	}
	private static StreamWriter CreateStream(string NameFile, bool AppendFlag){
    	return new StreamWriter(Path.Combine(GetPrefix(), NameFile), append: AppendFlag);
    }
    public static void CheckFile(string NameFile){
    	if(!File.Exists(Path.Combine (GetPrefix(), NameFile) ))
    		CreateFile(NameFile);
    }
    public static void CreateFile(string NameFile){
        StreamWriter sw = CreateStream(NameFile, false);
        sw.Close();
    }
}
