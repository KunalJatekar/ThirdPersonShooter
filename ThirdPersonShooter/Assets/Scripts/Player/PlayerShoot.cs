using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController
{
    bool IsPlayerAlive;

    void Start()
    {
        IsPlayerAlive = true;
        GetComponent<Player>().PlayerHealth.OnDeath += PlayerHealth_OnDeath;
    }

    void PlayerHealth_OnDeath()
    {
        IsPlayerAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPlayerAlive || GameManager.instance.IsPaused)
            return;

        if (GameManager.instance.GetInputController.mouseWheelDown)
            SwitchWeapon(1);

        if (GameManager.instance.GetInputController.mouseWheelUp)
            SwitchWeapon(-1);

        if (GameManager.instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.SPRINTING)
            return;

        if (!CanFire)
            return;

        if (GameManager.instance.GetInputController.fire1)
            ActiveWeapon.Fire();

        if (GameManager.instance.GetInputController.reload)
            ActiveWeapon.Reload();
    }
}
