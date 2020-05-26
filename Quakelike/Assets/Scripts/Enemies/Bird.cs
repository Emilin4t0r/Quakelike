using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy {

    void Start() {
        target = new Vector3(0, 5, 0);
        player = GameObject.Find("Player");
        health = 100;
        origMoveSpeed = moveSpeed;
    }

    void FixedUpdate() {        
        if (!attacking)
            CheckForPlayer();
        Idle();
    }

    void Idle() {
        //Go towards target
        Vector3 moveDir = (target - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.LookAt(target);

        //Get new random target when close enough to current one
        if (!attacking) {
            if (Vector3.Distance(transform.position, target) < 0.5f) {
                target = new Vector3(Random.Range(-5, 5), 5, Random.Range(-5, 5));//<-A bit shit since can't determine any other patrol point
                float z = target.z;
                float x = target.x;
                target = new Vector3(z, 5, x);
            }
        } else {
            if (Vector3.Distance(transform.position, target) < 0.1f) {
                PoolManager.Instance.birdPool.ReturnEntity(gameObject);
                Invoke("ResetValues", 0.5f);
            }
        }
    }

    void CheckForPlayer() {
        //Check if player is close enough for attack
        if (Vector3.Distance(player.transform.position, transform.position) < 8) {
            if (!attacking)
                Attack();
        }
    }

    void Attack() {
        //Go towards player at attackSpeed
        target = player.transform.position;
        moveSpeed = attackSpeed;
        attacking = true;
    }

    void ResetValues() {
        moveSpeed = origMoveSpeed;
        target = new Vector3(0, 5, 0);
        attacking = false;
        health = 100;
    }    

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player")) {
            PoolManager.Instance.birdPool.ReturnEntity(gameObject);
            Invoke("ResetValues", 0.5f);
        }
    }
}
