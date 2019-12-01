using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] Collider trigger;

    PlayerCover playerCover;

    bool CheckLocalPlayer(Collider other)
    {
        if (other.tag != "Player")
            return false;

        if (other.GetComponent<Player>() != GameManager.instance.LocalPlayer)
            return false;

        playerCover = GameManager.instance.LocalPlayer.GetComponent<PlayerCover>();

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLocalPlayer(other))
            return;

        playerCover.SetPlayerCoverAllowed(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckLocalPlayer(other))
            return;

        playerCover.SetPlayerCoverAllowed(false);
    }
}
