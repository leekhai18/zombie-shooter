using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        int rand = Random.Range(0, 100);
        //if (rand <= 80)
            ObjectPoolManager.Instance.enemyPool.TryGetNextObject(transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
