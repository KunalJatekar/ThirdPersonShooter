using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour
{
    [SerializeField] int numberOfRays;
    [SerializeField] LayerMask coverMask;

    bool canTakeCover;
    bool isInCover;
    RaycastHit closestHit;

    bool isAiming
    {
        get
        {
            return GameManager.instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING
                    || GameManager.instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAiming && isInCover)
        {
            ExecuteCoverToggle();
            return;
        }

        if (!canTakeCover)
            return;

        if (GameManager.instance.GetInputController.coverToggle)
            TakeCover();
    }

    void TakeCover()
    {
        FindCoverAroundPlayer();

        if (closestHit.distance == 0)
            return;

        ExecuteCoverToggle();
    }

    void ExecuteCoverToggle()
    {
        isInCover = !isInCover;
        GameManager.instance.EventBus.RaiseEvent("CoverToggle");

        transform.rotation = Quaternion.LookRotation(closestHit.normal) * Quaternion.Euler(0, 180f, 0);
    }



    private void FindCoverAroundPlayer()
    {
        closestHit = new RaycastHit();
        float angelStep = 360 / numberOfRays;

        for (int i = 0; i < numberOfRays; i++)
        {
            Quaternion angle = Quaternion.AngleAxis(i * angelStep, transform.up);

            CheckClosestPoint(angle);
        }
    }

    private void CheckClosestPoint(Quaternion angle)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * .3f, angle * Vector3.forward, out hit, 5f, coverMask))
        {
            if (closestHit.distance == 0 || hit.distance < closestHit.distance)
                closestHit = hit;
        }
    }

    internal void SetPlayerCoverAllowed(bool value)
    {
        canTakeCover = value;

        if (!canTakeCover && isInCover)
            ExecuteCoverToggle();
    }
}
