using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    public EnemyController enemyController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyController.target = other.transform;
        }
    }
}
