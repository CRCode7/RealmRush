using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    [SerializeField] Transform towerParentTransform;

    //Create an empty queue of towers
    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        print(towerQueue.Count);
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit)
        {
            var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParentTransform;
            baseWaypoint.isPlaceable = false;

            //set the baseWaypoints
            newTower.baseWaypoint = baseWaypoint;


            towerQueue.Enqueue(newTower);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        //Take bottom tower off queue
        var oldTower = towerQueue.Dequeue();
        //Set the placeable flags
        oldTower.baseWaypoint.isPlaceable = true; //free up the block
        newBaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;
        //Set the baseWaypoints
        towerQueue.Enqueue(oldTower);
    }
}
