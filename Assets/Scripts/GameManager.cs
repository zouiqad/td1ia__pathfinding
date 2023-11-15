using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMap tilemap;

    private Pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                pathfinding.ResetTiles();
                Vector3Int goalPos = Vector3Int.FloorToInt(raycastHit.transform.position);
                pathfinding.Djikistra(tilemap.grid[0, 0], tilemap.grid[goalPos.x, goalPos.z]);
                print(raycastHit.transform.position);
            }
        }

        
    }
}
