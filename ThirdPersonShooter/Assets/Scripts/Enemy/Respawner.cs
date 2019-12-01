using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void DeSpawner(GameObject gameObject, float inSeconds)
    {
        gameObject.SetActive(false);

        GameManager.instance.Timer.Add(() => {
            gameObject.SetActive(true);
        }, inSeconds);
    }
}
