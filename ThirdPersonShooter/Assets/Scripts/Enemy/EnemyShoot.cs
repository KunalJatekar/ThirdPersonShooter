using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Exstension;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController
{
    [SerializeField] float shootingSpeed;
    [SerializeField] float burstDurationMax;
    [SerializeField] float burstDurationMin;

    EnemyPlayer enemyPlayer;
    bool shouldFire;

    // Start is called before the first frame update
    void Start()
    {
        enemyPlayer = GetComponent<EnemyPlayer>();
        enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
    }

    private void EnemyPlayer_OnTargetSelected(Player target)
    {
        ActiveWeapon.AimTarget = target.transform;
        ActiveWeapon.AimTargetOffset = Vector3.up * 1.5f;
        startBurst();
    }

    void CrouchState()
    {
        bool takeCover = Random.Range(0, 3) == 0;

        if (!takeCover)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, ActiveWeapon.AimTarget.position);

        if (distanceToTarget > 15)
            enemyPlayer.GetComponent<EnemyAnimation>().IsCrouched = true;
    }

    void startBurst()
    {
        if (!enemyPlayer.EnemyHealth.IsAlive && !CanSeeTarget())
            return;

        checkReload();
        CrouchState();
        shouldFire = true;

        GameManager.instance.Timer.Add(endBurst, Random.Range(burstDurationMin, burstDurationMax));
    }

    void endBurst()
    {
        shouldFire = false;

        if (!enemyPlayer.EnemyHealth.IsAlive)
            return;

        checkReload();
        CrouchState();

        if (CanSeeTarget())
            GameManager.instance.Timer.Add(startBurst, shootingSpeed);
    }

    bool CanSeeTarget()
    {
        if (!transform.IsInLineOfSight(ActiveWeapon.AimTarget.position, 90, enemyPlayer.playerScanner.mask, Vector3.up))
        {
            //Clear the Target
            enemyPlayer.ClearTargetAndScan();
            return false;
        }
        return true;
    }

    void checkReload()
    {
        if (ActiveWeapon.reloader.RoundsRemainingInClip == 0)
        {
            CrouchState();
            ActiveWeapon.Reload();
        }
    }

    void Update()
    {
        if (!shouldFire || !CanFire || !enemyPlayer.EnemyHealth.IsAlive)
            return;

        ActiveWeapon.Fire();
    }

}
