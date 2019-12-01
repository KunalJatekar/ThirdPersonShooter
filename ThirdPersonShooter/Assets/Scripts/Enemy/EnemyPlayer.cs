using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyPlayer : MonoBehaviour
{
    [SerializeField] public Scanner playerScanner;
    [SerializeField] SwatSoldier settings;

    PathFinder pathFinder;

    Player priorityTargets;
    List<Player> myTargets;

    public event System.Action<Player> OnTargetSelected;

    EnemyHealth m_EnemyHealth;
    public EnemyHealth EnemyHealth
    {
        get
        {
            if (m_EnemyHealth == null)
                m_EnemyHealth = GetComponent<EnemyHealth>();

            return m_EnemyHealth;
        }
    }

    EnemyState m_EnemyState;
    public EnemyState EnemyState
    {
        get
        {
            if (m_EnemyState == null)
                m_EnemyState = GetComponent<EnemyState>();

            return m_EnemyState;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        pathFinder.Agent.speed = settings.WalkSpeed;

        playerScanner.OnScanReady += Scanner_OnScanReady;
        Scanner_OnScanReady();

        EnemyHealth.OnDeath += EnemyHealth_OnDeath;
        EnemyState.OnModeChange += EnemyState_OnModeChange;
    }

    void EnemyState_OnModeChange(EnemyState.EMode state)
    {
        pathFinder.Agent.speed = settings.WalkSpeed;

        if(state == EnemyState.EMode.AWARE)
            pathFinder.Agent.speed = settings.RunSpeed;
    }

    void CheckEaseWeapon()
    {
        //Check if we can ease our weapon. (stop firing)
        if (priorityTargets != null)
            return;

        this.EnemyState.CurrentMode = EnemyState.EMode.UNAWARE;
    }

    void CheckContinuePatrol()
    {
        //Check if we can continue patrol. 
        if (priorityTargets != null)
            return;

        if(pathFinder.Agent.isActiveAndEnabled)
            pathFinder.Agent.Resume();
    }

    internal void ClearTargetAndScan()
    {
        priorityTargets = null;

        GameManager.instance.Timer.Add(CheckEaseWeapon, UnityEngine.Random.Range(3, 6));
        GameManager.instance.Timer.Add(CheckContinuePatrol, UnityEngine.Random.Range(12, 16));

        Scanner_OnScanReady();
    }

    private void EnemyHealth_OnDeath()
    {
        
    }

    private void Scanner_OnScanReady()
    {
        if (priorityTargets != null)
            return;

        myTargets = playerScanner.ScanForTargets<Player>();

        if (myTargets.Count == 1)
            priorityTargets = myTargets[0];
        else
            SelectClosectTarget();

        if (priorityTargets != null)
        {
            if (OnTargetSelected != null) {
                this.EnemyState.CurrentMode = EnemyState.EMode.AWARE;
                OnTargetSelected(priorityTargets);
            }
        }
    }

    void SelectClosectTarget()
    {
        float closestTarget = playerScanner.ScanRange;
        foreach(var possibleTarget in myTargets)
        {
            if (Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
                priorityTargets = possibleTarget;
        }
    }

    void Update()
    {
        if (priorityTargets == null || !EnemyHealth.IsAlive)
            return;

        transform.LookAt(priorityTargets.transform.position);
    }
}
