using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizBall : Projectile
{
    Rigidbody rb;
    public bool released;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();        
    }

    void Update()
    {                       
        if (Time.time >= deathTime) {
            ResetValues();
            PoolManager.Instance.wizballPool.ReturnEntity(gameObject);            
        }
        if (released) {
            rb.AddForce(transform.up * 10, ForceMode.Force);
            if (rb.velocity.z > 0) { //Slows the ball down to a stop
                rb.AddForce(transform.forward * (-10), ForceMode.Force);
            }
        }
    }

    void Explode() {
        int random = Random.Range(0, 3);
        AudioFW.Play("WizExplode" + random);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
        for (int i = 0; i < colliders.Length; i++) {            
            string type = colliders[i].name.TrimEnd(' ', '(', '1', '2', '3', '4', '5', '6', '7', '8', '9', ')');
            if (type == "WizBall") {
            } else if (type == "Player") {
                print("damaged " + type);
            } else if (type == "Bird") {
                colliders[i].GetComponent<Bird>().TakeDamage(100, PoolManager.Instance.birdPool);
            }
        }
        ResetValues();
        PoolManager.Instance.wizballPool.ReturnEntity(gameObject);
    }

    public void Release() {
        if (gameObject.activeSelf)
        released = true;
    }

    public void ResetValues() {
        rb.velocity = Vector3.zero;
        deathTime = 0;
        released = false;
    }

    private void OnTriggerEnter(Collider other) {
        Explode();
    }
}
