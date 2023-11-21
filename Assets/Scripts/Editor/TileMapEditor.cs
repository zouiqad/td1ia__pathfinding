using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{

/*    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileMap tilemap = (TileMap)target;

        if(GUILayout.Button("Generate TileMap"))
        {
            tilemap.ResetAllTiles();
            tilemap.GenerateMap(20, 20);

        }
    }*/
}
