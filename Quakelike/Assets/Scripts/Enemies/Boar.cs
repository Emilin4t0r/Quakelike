using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy {

    public bool charging;
    bool attacked;

    void Start()
    {
        target = new Vector3(0, 0.5f, 0);
        player = GameObject.Find("Player");
        health = 200;
        origMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (!charging)
            CheckForPlayer();
        Idle();
    }

    void Idle() {
        //Go towards target
        Vector3 moveDir = (target - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.LookAt(target);

        
        if (!charging) {
            //Get new random target when close enough to current one
            if (Vector3.Distance(transform.position, target) < 0.5f) {
                target = new Vector3(Random.Range(-5, 5), 0.5f, Random.Range(-5, 5));//<-A bit shit since can't determine any other patrol point
                float z = target.z;
                float x = target.x;
                target = new Vector3(z, 0.5f, x);
            }
        }
        else {
            if (Vector3.Distance(transform.position, target) < 0.1f) {
                if (!attacked)
                    Attack();
                attacked = true;
            }
        }
    }

    void CheckForPlayer() {
        //Check if player is close enough for attack
        if (Vector3.Distance(player.transform.position, transform.position) < 8) {
            if (!charging)
                Charge();
        }
    }

    void Attack() {
        print("damaged player");
    }

    void ResetValues() {
        moveSpeed = origMoveSpeed;
        target = new Vector3(0, 0.5f, 0);
        charging = false;
        health = 200;
    }

    void Charge() {
        //Go towards player at attackSpeed
        target = player.transform.position;
        moveSpeed = attackSpeed;
        charging = true;
    }
}
