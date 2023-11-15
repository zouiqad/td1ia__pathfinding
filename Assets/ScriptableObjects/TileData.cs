using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="TileData")]

public class TileData : ScriptableObject
{
    public GameObject GameObject;
    public enum TileType
    {
        Floor,
        Wall
    }

    public float cost = 1.0f;
    public TileType tileType = TileType.Floor;
}
