using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;


    // Update is called once per frame
    private void Update()
    {
        //gets position of platform and compares it to the waypoint locations from previous array
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            //if waypoint array is at last index, it goes back to the starting object
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;

            }
        }
        //only moves towards the waypoint a small amount per frame depending on time between frames 
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,Time.deltaTime * speed);
    }
}
