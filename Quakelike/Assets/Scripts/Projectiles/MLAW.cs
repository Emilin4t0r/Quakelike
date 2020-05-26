﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLAW : Projectile
{
    public float speed;
    Rigidbody rb;
    int random;

    private void Awake() {
        random = Random.Range(0, 2);
        if (random == 0)
            random = -1;
        print(random);
    }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }    

    void Update()
    {
        if (Time.time >= deathTime) {
            PoolManager.Instance.mlawPool.ReturnEntity(gameObject);
            ResetValues();
        }
               
        transform.Rotate(new Vector3(0, 0, speed * random));
    }

    public void ResetValues() {
        rb.velocity = Vector3.zero;
        deathTime = 0;
    }

    private void OnTriggerEnter(Collider other) {
        string type = other.name.TrimEnd(' ', '(', '1', '2', '3', '4', '5', '6', '7', '8', '9', ')');
        if (type == "WizBall") {
        }
        else if (type == "Player") {
            print("damaged " + type);
        }
        else if (type == "Bird") {
            other.GetComponent<Bird>().TakeDamage(100, PoolManager.Instance.birdPool);
        }
    }
}
