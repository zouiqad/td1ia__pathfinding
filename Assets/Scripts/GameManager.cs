using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab;
    [HideInInspector] public int sizeX;
    [HideInInspector] public int sizeY;

    public TileMap[,] worldGrid;
    public GameObject[] roomTypes;
    public GameObject ceilingPrefab;

    private GameObject _worldGridGO;
    private GameObject player;
    private GameObject ia_agent;
    private Pathfinding pathfinding;
    private Tile start_tile;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
        player = GameObject.Find("Player");
        ia_agent = GameObject.Find("IA_Agent");

        GenerateTileMap(sizeX, sizeY, tilePrefab);
        //disableDebugText();
        SpawnPlayer();
        SpawnIA();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                pathfinding.ResetAllTiles();
                Vector3Int startPos = Vector3Int.FloorToInt(raycastHit.transform.position);

                start_tile = tilemap.grid[startPos.x, startPos.z];
                start_tile._Color = Color.blue;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Vector3Int goalPos = Vector3Int.FloorToInt(raycastHit.transform.position);
                Tile goal_tile = tilemap.grid[goalPos.x, goalPos.z];

                pathfinding.Djikistra(start_tile, goal_tile);
            }
        }*/

        
    }
    
    private void disableDebugText()
    {
        foreach(var tilemap in worldGrid)
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    tilemap.grid[i, j].DisableText();
                }
            }
        }

    }
    public void startDjikistra()
    {
        print(worldGrid[0, 0]);
        print(worldGrid[0, 0].grid[0, 0]);
        pathfinding.Djikistra(worldGrid[0, 0].grid[1, 1], worldGrid[1, 1].grid[5, 5]);
    }

    public void GenerateTileMap(int sizeX, int sizeY, GameObject tilePrefab)
    {
        if (_worldGridGO == null)
            CreateWorldGrid();


        for(int i = 0; i < sizeX; i++)
        {
            for(int j = 0; j < sizeY; j++)
            {
                /*                TileMap newTileMap = new GameObject().AddComponent<TileMap>();

                                worldGrid[i, j] = newTileMap;
                                newTileMap.GenerateChunk(tilePrefab);*/

                Vector3 chunkPos = new Vector3(10 * i, 0.0f, 10 * j);

                GameObject roomType = (i == sizeX - 1 && j == sizeY - 1) ? roomTypes[0] : roomTypes[Random.Range(0, roomTypes.Length)];
                
                TileMap newTileMap = Instantiate(roomType, chunkPos, Quaternion.identity).GetComponent<TileMap>();
                worldGrid[i, j] = newTileMap;
                // Misc

                newTileMap.gameObject.name = $"Grid[{i},{j}]";
                newTileMap.transform.parent = _worldGridGO.transform;
                newTileMap.transform.position = chunkPos;


                Debug.Log($"{worldGrid[i, j].name} position ");
            }
        }

        SpawnCeiling();
        // Connect neighboring tilemaps

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                TileMap currentTileMap = worldGrid[i, j];


                // Add top neighboring tiles

                if(i < sizeX - 1) // if not last tile map on top
                {
                    for(int y = 0; y < currentTileMap.sizeY; y++)
                    {
                        Tile currentTile = currentTileMap.grid[currentTileMap.sizeX - 1, y];
                        currentTile.neighbors.Add(worldGrid[i + 1, j].grid[0, y]);
                    }
                }

                // Add right neighboring tiles

                if(j < sizeY - 1) // if not last tile map on the right
                {
                    for (int x = 0; x < currentTileMap.sizeX; x++)
                    {
                        Tile currentTile = currentTileMap.grid[x, currentTileMap.sizeY - 1];
                        currentTile.neighbors.Add(worldGrid[i, j + 1].grid[x, 0]);
                    }
                }

                if (i > 0) // if not last tile map on bottom
                {
                    for (int y = 0; y < currentTileMap.sizeY; y++)
                    {
                        Tile currentTile = currentTileMap.grid[0, y];
                        currentTile.neighbors.Add(worldGrid[i - 1, j].grid[currentTileMap.sizeX - 1, y]);
                    }
                }

                if (j > 0) // if not last tile map on the left
                {
                    for (int x = 0; x < currentTileMap.sizeX; x++)
                    {
                        Tile currentTile = currentTileMap.grid[x, 0];
                        currentTile.neighbors.Add(worldGrid[i, j - 1].grid[x, currentTileMap.sizeY - 1]);

                    }
                }




            }
        }

    }

 

    public void ResetWorld()
    {
        if (_worldGridGO != null)
            DestroyImmediate(_worldGridGO);

        CreateWorldGrid();
    }

    private void CreateWorldGrid()
    {
        worldGrid = new TileMap[sizeX, sizeY];
        _worldGridGO = new GameObject();
        _worldGridGO.name = "World Grid";
    }

    private void SpawnCeiling()
    {
        for(int i = 0; i < worldGrid.GetLength(0); i++)
        {
            for(int j = 0; j < worldGrid.GetLength(1); j++)
            {
                Vector3 parentPos = worldGrid[i, j].grid[5, 5].transform.position;
                Instantiate(ceilingPrefab, new Vector3(parentPos.x - 0.5f, 4.28f, parentPos.z - 0.5f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f))) ;
            }
        }
    }
    private void SpawnIA()
    {
        ia_agent.transform.position = worldGrid[sizeX - 1, sizeY - 1].grid[0, 0].transform.position;
    }

    private void SpawnPlayer()
    {
        player.transform.position = worldGrid[0, 0].grid[1, 1].transform.position;

    }

/*    private void SpawnPlayer()
    {
        Camera player_camera = player.GetComponentInChildren<Camera>();

        int rand_x = Random.Range(0, tilemap.sizeX);
        int rand_y = Random.Range(0, tilemap.sizeY);

        while (tilemap.grid[rand_x, rand_y]._TileType == Tile.TileType.Wall)
        {
            rand_x = Random.Range(0, tilemap.sizeX);
            rand_y = Random.Range(0, tilemap.sizeY);
        }

        player.transform.position = new Vector3(rand_x, 1.5f, rand_y);

    }*/
}
