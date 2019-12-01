using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    Waypoint[] waypoints;

    int currentWaypointIndex = -1;

    public event System.Action<Waypoint> OnWaypointChanged;

    void Awake()
    {
        waypoints = GetWayPoints();
    }

    Waypoint[] GetWayPoints()
    {
        return GetComponentsInChildren<Waypoint>();
    }

    public void SetNextWaypoint()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex == waypoints.Length)
            currentWaypointIndex = 0;

        if (OnWaypointChanged != null)
            OnWaypointChanged(waypoints[currentWaypointIndex]);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 previousWaypoint = Vector3.zero;

        foreach(var waypoint in GetWayPoints())
        {
            Vector3 waypointPosition = waypoint.transform.position;
            Gizmos.DrawWireSphere(waypointPosition, 0.2f);
            if (previousWaypoint != Vector3.zero)
                Gizmos.DrawLine(previousWaypoint, waypointPosition);

            previousWaypoint = waypointPosition;
        }
    }
}
