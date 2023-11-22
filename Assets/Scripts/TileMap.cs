using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private GameManager gameManager;


    private TileMap[,] worldGrid;
    public int sizeX = 10;
    public int sizeY = 10;

    public Tile[,] grid;

    public Dictionary<Tile, List<Tile>> neighborDictionary;

    public List<Tile> Neighbors(Tile tile)
    {
        return neighborDictionary[tile];
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        worldGrid = gameManager.worldGrid;
        print("awoken " + name);
        grid = new Tile[sizeX, sizeY];
    }
    public void GenerateChunk(GameObject _tilePrefab)
    {

        neighborDictionary = new Dictionary<Tile, List<Tile>>();

        // Generer la map (gameobjects)
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                grid[x, y] = Instantiate(_tilePrefab, new Vector3(x, 0, y), Quaternion.identity).GetComponent<Tile>();
                grid[x, y].gameObject.transform.parent = transform;
                grid[x, y].Init(x, y, 0);
            }
        }

        // Construire le graphe
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                List<Tile> neighbors = new List<Tile>();
                
                if (y < sizeY - 1)
                    neighbors.Add(grid[x, y + 1]);
                if (x < sizeX - 1)
                    neighbors.Add(grid[x + 1, y]);
                if (y > 0)
                    neighbors.Add(grid[x, y - 1]);
                if (x > 0)
                    neighbors.Add(grid[x - 1, y]);

                neighborDictionary.Add(grid[x, y], neighbors);
                grid[x, y].neighbors = neighbors;
            }
        }

    }


    public Vector2Int WorldToGridPosition(Vector3 world_position)
    {
        int x = world_position.x < 0 ? Mathf.CeilToInt(world_position.x) : Mathf.FloorToInt(world_position.x);
        int y = world_position.z < 0 ? Mathf.CeilToInt(world_position.z) : Mathf.FloorToInt(world_position.z);
        return new Vector2Int(x, y);
    }

    public void ResetAllTiles()
    {
        if(grid != null)
        {
            foreach (Tile t in grid)
            {
                t.predecessor = null;
                t.Cost = Mathf.Infinity;
                t._Color = Color.white;
                t._Text = "";
            }
        }

    }
}