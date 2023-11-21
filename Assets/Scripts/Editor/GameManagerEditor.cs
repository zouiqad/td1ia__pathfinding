using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;
        serializedObject.Update();

        SerializedProperty sizeX = serializedObject.FindProperty("sizeX");
        sizeX.intValue = EditorGUILayout.IntField("TileMap size X", sizeX.intValue);

        SerializedProperty sizeY = serializedObject.FindProperty("sizeY");
        sizeY.intValue = EditorGUILayout.IntField("TileMap size Y", sizeY.intValue);

        SerializedProperty tilePrefab = serializedObject.FindProperty("tilePrefab");
        tilePrefab.objectReferenceValue = EditorGUILayout.ObjectField("Tile Prefab", tilePrefab.objectReferenceValue ,typeof(GameObject), true);

        serializedObject.ApplyModifiedProperties();


        if (GUILayout.Button("Generate Map"))
        {
            gameManager.GenerateTileMap(sizeX.intValue, sizeY.intValue, (GameObject)tilePrefab.objectReferenceValue);
        }

        if(GUILayout.Button("Reset World"))
        {
            gameManager.ResetWorld();
        }

        if(GUILayout.Button("Start Djikistra"))
        {
            gameManager.startDjikistra();
        }
    }
}
