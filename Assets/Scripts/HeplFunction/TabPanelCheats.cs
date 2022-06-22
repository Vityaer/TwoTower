using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
#if UNITY_EDITOR_WIN
using UnityEditor;
public class TabPanelCheats : EditorWindow{

	[MenuItem("Tools/My develop/TabPanel Cheat")]
	static void Init(){
        // Get existing open window or if none, make a new one:
        TabPanelCheats window = (TabPanelCheats)EditorWindow.GetWindow(typeof(TabPanelCheats));
        window.Show();
    }

    Vector2 scrollPos;
    int IDsplinter, IDItem;
    void OnGUI(){
    	EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.Space();
        GUILayout.Label("Cheats", EditorStyles.boldLabel);
        if (GUILayout.Button("+ 50 resources")){ Add50Resources(); }
        if (GUILayout.Button("+ 500 resources campaign")){ Add50ResourcesCampaign(); }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }


    private static string NameListHeroFile    = "FileListHero";
    private static string NameGameFile        = "FileGame";
    private static string NameInventoryFile   = "FileInventory";
    void Add50Resources(){
    	ListResource res = new ListResource();
    	res.SetResource(new Resource(TypeResource.Gold, 50, 0));
    	res.SetResource(new Resource(TypeResource.ManaPoint, 50, 0));
    	res.SetResource(new Resource(TypeResource.Animals, 50, 0));
    	TowerControllerScript myTower = GameControllerScript.Instance.GetTower();
    	GameControllerScript.Instance.IncreaseResource(myTower, Side.Myself, res);
    }
    void Add50ResourcesCampaign(){
        ListResource res = new ListResource();
        res.SetResource(new Resource(TypeResource.Jewerly, 500, 0));
        res.SetResource(new Resource(TypeResource.Silk, 500, 0));
        Information.Instance.playerHero.res.AddResource(res);
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
		List<string> ListResult = new List<string>(); 
		try{
			ListResult = new List<string>(File.ReadAllLines(GetPrefix() + "/" + NameFile + ".data"));
		}catch{}
		return ListResult;
	}
	private static StreamWriter CreateStream(string NameFile, bool AppendFlag){
    	return new StreamWriter(GetPrefix() + "/" + NameFile + ".data", append: AppendFlag);
    	
    }
    public static void CheckFile(string NameFile){
    	if(!File.Exists(GetPrefix() + "/" + NameFile + ".data")){
    		CreateFile(NameFile);
    	}
    }
    public static void CreateFile(string NameFile){
        StreamWriter sw = CreateStream(NameFile, false);
        sw.Close();
    }
}
#endif