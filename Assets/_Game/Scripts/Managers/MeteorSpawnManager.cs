using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnManager : MonoBehaviour {

    public float timeDelay = 15f;
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
        timeDelay = Random.Range(3f, 5f);
        ObjectPoolManager.Instance.meteorPool.TryGetNextObject(Random.onUnitSphere * 50, Quaternion.identity);
    }
}
