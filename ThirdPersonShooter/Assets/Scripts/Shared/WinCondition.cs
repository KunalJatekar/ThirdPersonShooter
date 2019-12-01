using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] Destructable[] targets;

    int targetDestroyedCounter;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < targets.Length; i++)
        {
            targets[i].OnDeath += WinCondition_OnDeath;
        }
        
    }

    private void WinCondition_OnDeath()
    {
        targetDestroyedCounter++;

        if (targetDestroyedCounter == targets.Length)
            GameManager.instance.EventBus.RaiseEvent("OnAllEnemyKilled");
    }

}
