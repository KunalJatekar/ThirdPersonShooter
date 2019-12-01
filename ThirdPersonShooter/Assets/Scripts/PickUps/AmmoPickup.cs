using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickUpItem
{
    [SerializeField] EWeaponType eWeaponType;
    [SerializeField] float respawnTime;
    [SerializeField] int amount;

    public override void OnPickUp(Transform item)
    {
        var playerInventory = item.GetComponentInChildren<Container>();
        GameManager.instance.Respawner.DeSpawner(gameObject, respawnTime);
        playerInventory.Put(eWeaponType.ToString(), amount);
        item.GetComponent<Player>().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged();
    }
}
