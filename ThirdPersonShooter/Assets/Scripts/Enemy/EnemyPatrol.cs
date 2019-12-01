using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] WaypointController waypointController;
    [SerializeField] float waitTimeMin;
    [SerializeField] float waitTimeMax;

    PathFinder pathFinder;

    EnemyPlayer m_EnemyPlayer;
    public EnemyPlayer EnemyPlayer
    {
        get
        {
            if (m_EnemyPlayer == null)
                m_EnemyPlayer = GetComponent<EnemyPlayer>();

            return m_EnemyPlayer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waypointController.SetNextWaypoint();    
    }

    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.OnDestinationChanged += PathFinder_OnDestinationChanged;
        waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;

        EnemyPlayer.EnemyHealth.OnDeath += EnemyHealth_OnDeath;
        EnemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
    }

    private void EnemyPlayer_OnTargetSelected(Player obj)
    {
        if(pathFinder.Agent.isActiveAndEnabled)
            pathFinder.Agent.isStopped = true;
    }

    private void EnemyHealth_OnDeath()
    {
        if (pathFinder.Agent.isActiveAndEnabled)
            pathFinder.Agent.isStopped = true;
    }

    void WaypointController_OnWaypointChanged(Waypoint waypoint)
    {
        pathFinder.setTarget(waypoint.transform.position);
    }

    private void PathFinder_OnDestinationChanged()
    {
        GameManager.instance.Timer.Add(waypointController.SetNextWaypoint, Random.Range(waitTimeMin, waitTimeMax));
    }

}
