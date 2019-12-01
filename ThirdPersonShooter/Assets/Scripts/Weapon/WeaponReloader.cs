using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] int clipSize;
    [SerializeField] Container inventory;
    [SerializeField] EWeaponType eWeaponType;

    public int shotsFiredInClip;
    public event System.Action OnAmmoChanged;

    bool isReloading;
    System.Guid containerItemId;

    private void Awake()
    {
        inventory.OnContainerReady += () => { containerItemId = inventory.Add(eWeaponType.ToString(), maxAmmo); };
    }

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
    {
        if (isReloading)
            return;

        isReloading = true;

        GameManager.instance.Timer.Add(() => {
            ExecuteReload(inventory.takeFromContainer(containerItemId, clipSize - RoundsRemainingInClip));
        }, reloadTime);
    }

    public void ExecuteReload(int amount)
    {
        isReloading = false;
        shotsFiredInClip -= amount;

        HandleOnAmmoChanged();
    }

    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;

        HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        if (OnAmmoChanged != null)
            OnAmmoChanged();
    }
}
