using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            PoolManager.Instance.flarePool.SpawnProjectile(transform.position, transform.rotation, 10);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            PoolManager.Instance.boarPool.SpawnEnemy(transform.position);
        }
    }
}
