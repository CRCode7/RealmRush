using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning_loop = true;
    Waypoint searchCenter; //The current searchCenter
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            ColorStartAndEnd();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        //Se comienza agregando el punto final
        path.Add(endWaypoint);
        endWaypoint.isPlaceable = false;
        //Luego el valor anterior, por el cual, el último valor fue descubierto
        Waypoint previous = endWaypoint.exploredFrom;
        //Conociendo los valores intermedios
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous.isPlaceable = false;
            //ahora revisamos el otro valor anterior
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        startWaypoint.isPlaceable = false;
        path.Reverse();
    }


    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning_loop)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            print("Searching from: " + searchCenter); // remove log
            if (searchCenter == endWaypoint)
            {
                isRunning_loop = false;
                print("Searching END");
            }
            // Explore Neighbours
            ExploreNeighbours();
        }
        print("Finished pathfinding?");
    }

    private void ExploreNeighbours()
    {
        if (!isRunning_loop) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }
        else
        {
            neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
            print("Queueing " + neighbour);
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.cyan);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            //overlapping blocks?
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping Overlapping block " + waypoint);
            }
            else
            {
                //add to dictionary
                grid.Add(gridPos, waypoint);
            }
        }
        print(grid);
    }
}
