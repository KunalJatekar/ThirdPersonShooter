using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Destructable
{
    [SerializeField] SpawnPoint[] spawnPoints;

    void SpawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    }

    public override void Die()
    {
        base.Die();
        SpawnAtNewSpawnPoint();
    }

    [ContextMenu("Test Die")]
    void TestDie()
    {
        Die();
    }
}
