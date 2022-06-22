using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;

internal class FunctionHelp : MonoBehaviour {

	public static string BigDigit( float Num, int qENum, bool xFlag = false){
		string    result  =  "";
		if(xFlag) result += 'x';
		result = string.Concat(result, Mathf.Floor(Num).ToString());
		AddPowerE10(ref result, qENum);
		return result;
	}
	private static void AddPowerE10(ref string result, int qENum){
		string postFix = "";
		switch (qENum) {
			case 3:
				postFix = "K";
				break;
			case 6:
				postFix = "M";
				break;
		}
		result = string.Concat(result, postFix);
	}  
	public static string TimerText(float time){
		int Num1 = (int) Mathf.Floor(time);
		int Num2 = (int) Mathf.Floor((time - Num1)*10f); 
		string result = Num1 + "." + Num2 + " sec";
		return result;
	}

	public static Vector2 CalculateBezieDir(Vector2 point1, Vector2 point2, Vector2 point3, float alfa){
		Vector2 t1 = alfa * (point2 - point1) + point1;
    	Vector2 t2 = alfa * (point3 - point2) + point2;
    	return alfa * (t2 - t1) + t1;
	}
	public static Vector3 GetRandomCenterBetweenPoints(Vector3 point1, Vector3 point2){
		return new Vector3( (point1.x + point2.x)/2 + UnityEngine.Random.Range(-0.5f, 0.5f), (point1.y + point2.y)/2 + UnityEngine.Random.Range(-0.5f, 0.5f), 0  );
	}
	public static Vector3 GetCenterBetweenPoints(Vector3 point1, Vector3 point2){
		return new Vector3( (point1.x + point2.x)/2, (point1.y + point2.y)/2, 0  );
	}
}
