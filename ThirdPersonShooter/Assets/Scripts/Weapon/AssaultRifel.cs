using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifel : Shooter
{
    public override void Fire()
    {
        base.Fire();

        if (canFire)
        {
            //Fire a gun;
        }
    }
}
