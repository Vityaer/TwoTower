using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(Card))]
[CanEditMultipleObjects]
public class CardEditor : Editor{
	Card cardScript = null;
	public override void OnInspectorGUI(){
		if(cardScript == null) cardScript = (Card)target;
		EditorGUILayout.LabelField("Card");
		EditorGUILayout.PropertyField(serializedObject.FindProperty("name"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("ID"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("costResource"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("description"));
        ShowListAction(serializedObject.FindProperty("actions"));
        serializedObject.ApplyModifiedProperties();
    }
    private void ShowListAction(SerializedProperty list){
	    EditorGUILayout.PropertyField(list, false);
	    EditorGUI.indentLevel += 1;
	    if (list.isExpanded) {
		    EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
		    for (int i = 0; i < list.arraySize; i++) {
		        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), false);
		        if (list.GetArrayElementAtIndex(i).isExpanded) {
			        EditorGUI.indentLevel += 1;
			        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("whom"));
			        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("typeAction"));
					switch(list.GetArrayElementAtIndex(i).FindPropertyRelative("typeAction").enumValueIndex){
						case 0:
						case 9:
						case 10:
							EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("prefab"));
						 	break;
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 8:
							EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("amount"));
						 	break;
						case 6:
						case 7:
							EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("res"));
						 	break;
						case 11:
							EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("prefab"));
							EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("amount"));
							break;	
						case 12:
							break;	
					}
		          EditorGUILayout.Space();
		          

		          EditorGUI.indentLevel -= 1;
		        }
		    }
	    }
    EditorGUI.indentLevel -= 1;
    }
}
#endif
