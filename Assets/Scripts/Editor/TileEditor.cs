using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
public class TileEditor : Editor
{

    SerializedProperty tileType;

    private void OnEnable()
    {

        tileType = serializedObject.FindProperty("_tileType");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        

        Tile tile = (Tile)target;

        Tile.TileType newTileType = (Tile.TileType)tileType.enumValueIndex;

        tile._TileType = newTileType;


    }
}
