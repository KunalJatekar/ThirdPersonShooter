using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
            return;

        pickUp(collider.transform);
    }

    public virtual void OnPickUp(Transform item)
    {

    }

    void pickUp(Transform item)
    {
        OnPickUp(item);
    } 
}
