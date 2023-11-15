using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public TileMap tilemap;

    void Start()
    {
        //Djikistra(tilemap.grid[2, 2], tilemap.grid[10, 14]);
    }

    public void Djikistra(Tile depart_tile, Tile goal_tile)
    {
        List<Tile> closed_list = new List<Tile>();
        List<Tile> open_list = new List<Tile>();

        open_list.Add(depart_tile);
        print("start");

        Tile current_tile = depart_tile;
        current_tile.Cost = 0;

        while (open_list.Count > 0)
        {
            open_list.Sort((x, y) => x.CostToReach.CompareTo(y.CostToReach)); // Ordonner la liste pour toujours avoir l'element avec le plus bas cout en premier

            current_tile = open_list[0]; // Pop first element from open list

            foreach (Tile neighbor in tilemap.Neighbors(current_tile))
            {
                if(!closed_list.Contains(neighbor) && neighbor._TileType != Tile.TileType.Wall)
                {

                    if (neighbor.Cost > neighbor.CostToReach + current_tile.Cost)
                    {
                        neighbor.Cost = neighbor.CostToReach + current_tile.Cost;
                        neighbor._Color = Color.yellow;
                        neighbor.predecessor = current_tile;
                    }

                    if (!open_list.Contains(neighbor)) open_list.Add(neighbor);
                }
            }

            open_list.Remove(current_tile);
            closed_list.Add(current_tile);
        }
        print(goal_tile.predecessor);
        Tile current = goal_tile;

        while(current.predecessor != null)
        {
            current._Color = Color.red;
            print(current.name);
            current = current.predecessor;

        }

    }

    public void ResetTiles()
    {
        for(int i = 0; i < tilemap.sizeX; i++)
        {
            for(int j = 0; j < tilemap.sizeY; j++)
            {
                tilemap.grid[i, j].Cost = Mathf.Infinity;
                tilemap.grid[i, j]._Color = Color.white;
            }
        }
    }

}