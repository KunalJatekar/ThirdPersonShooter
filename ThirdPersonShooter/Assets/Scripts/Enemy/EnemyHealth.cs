using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Destructable
{
    [SerializeField] Ragdoll ragdoll;

    public override void Die()
    {
        base.Die();
        ragdoll.EnableRagdoll(true);
        GameManager.instance.EventBus.RaiseEvent("Enemy Death");
    }
    
}
