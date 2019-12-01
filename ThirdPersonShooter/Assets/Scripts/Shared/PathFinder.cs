using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathFinder : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] float distanceRemainingThreshold;

    bool m_destinationReached;
    bool destinationReached
    {
        get { return m_destinationReached; }
        set
        {
            m_destinationReached = value;

            if (m_destinationReached)
            {
                if (OnDestinationChanged != null)
                    OnDestinationChanged();
            }
        }
    }

    public event System.Action OnDestinationChanged;

    // Start is called before the first frame update
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    public void setTarget(Vector3 target)
    {
        if (Agent.isActiveAndEnabled)
        {
            destinationReached = false;
            Agent.SetDestination(target);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if (destinationReached || !Agent.hasPath)
            return;

        if (Agent.remainingDistance < distanceRemainingThreshold)
            destinationReached = true;
    }
}
