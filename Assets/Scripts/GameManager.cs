using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMap tilemap;
    public GameObject player;

    private Pathfinding pathfinding;

    private Tile start_tile;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
        }

        
    }

    private void SpawnPlayer()
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

    }
}
