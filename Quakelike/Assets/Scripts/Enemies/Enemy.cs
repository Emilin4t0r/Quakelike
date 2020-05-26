using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 target;
    public GameObject player;
    public float health;
    public float moveSpeed;
    public float origMoveSpeed;
    public float attackSpeed;
    public bool attacking;

    public void TakeDamage(float amount, PoolManager.Pool pool) {
        health -= amount;
        if (health <= 0) {
            pool.ReturnEntity(gameObject);
            Invoke("ResetValues", 0.5f);
        }
    }
}
