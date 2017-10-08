using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratesSpawnManager : MonoBehaviour
{
    public float timeDelay = 20f;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0;
            Spawn();             
        }
    }

    public void Spawn()
    {
        timeDelay = Random.Range(15f, 30f);
        ObjectPoolManager.Instance.cratesPool.TryGetNextObject(Random.onUnitSphere * 30, Quaternion.identity);
    }
}
