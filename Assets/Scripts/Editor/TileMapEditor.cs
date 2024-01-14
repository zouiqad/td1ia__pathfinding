using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileMap tilemap = (TileMap)target;

        serializedObject.Update();

        SerializedProperty sizeX = serializedObject.FindProperty("sizeX");
        sizeX.intValue = EditorGUILayout.IntField("TileMap size X", sizeX.intValue);

        SerializedProperty sizeY = serializedObject.FindProperty("sizeY");
        sizeY.intValue = EditorGUILayout.IntField("TileMap size Y", sizeY.intValue);

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Generate TileMap"))
        {
            tilemap.ResetAllTiles();
            tilemap.GenerateChunk(sizeX.intValue, sizeY.intValue);

        }
    }
}
