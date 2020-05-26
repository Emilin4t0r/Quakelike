using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public ProjectilePool wizballPool;
    public List<GameObject> wizBalls;
    public ProjectilePool mlawPool;
    public List<GameObject> mlaws;

    public EnemyPool boarPool;
    public List<GameObject> boars;
    public EnemyPool birdPool;
    public List<GameObject> birds;    

    void Awake() {
        Instance = this;

        wizballPool = new ProjectilePool(transform.GetChild(2).gameObject, wizBalls);
        mlawPool = new ProjectilePool(transform.GetChild(3).gameObject, mlaws);

        boarPool = new EnemyPool(transform.GetChild(0).gameObject, boars);
        birdPool = new EnemyPool(transform.GetChild(1).gameObject, birds);
    }
    public void SetActiveRecursively(GameObject target, bool state) {
        target.SetActive(state);
        for (int i = 0; i < target.transform.childCount; i++) {
            target.transform.GetChild(i).gameObject.SetActive(state);
        }
    }

    public class Pool {
        public GameObject poolObject;
        public List<GameObject> entities;

        public Pool(GameObject poolObject_, List<GameObject> entities_) { //Constructor
            poolObject = poolObject_;
            entities = entities_;
            for (int i = 0; i < poolObject.transform.childCount; i++) {                
                entities.Add(poolObject.transform.GetChild(i).gameObject);
                Instance.SetActiveRecursively(entities[i], false);
            }
        }       
        public void ReturnEntity(GameObject entity) {           
            string type = entity.name.TrimEnd(' ', '(', '1', '2', '3', '4', '5', '6', '7', '8', '9', ')');
            if (type == "WizBall") {
                entity.GetComponent<WizBall>().ResetValues();
            }            
            entity.transform.position = poolObject.transform.position;
            entity.transform.parent = poolObject.transform;
            entities.Add(entity);
            Instance.SetActiveRecursively(entity, false);
        }
    }

    public class ProjectilePool : Pool {
        public ProjectilePool(GameObject poolObject_, List<GameObject> entities_) : base(poolObject_, entities_) { }//Constructor

        public void SpawnProjectile(Vector3 spawnPoint, Quaternion rotation, float shootForce, float lifetime) {
            Rigidbody proj = entities[0].gameObject.GetComponent<Rigidbody>();
            entities.Remove(proj.gameObject);
            Instance.SetActiveRecursively(proj.gameObject, true);
            proj.transform.parent = null;
            proj.transform.position = spawnPoint;
            proj.transform.rotation = rotation;
            proj.AddForce(proj.transform.forward * shootForce, ForceMode.Impulse);
            proj.GetComponent<Projectile>().deathTime = Time.time + lifetime;
        }        
    }

    public class EnemyPool : Pool {
        public EnemyPool(GameObject poolObject_, List<GameObject> entities_) : base(poolObject_, entities_) { }//Constructor

        public void SpawnEnemy(Vector3 spawnPoint) {
            GameObject enemy = entities[0];
            entities.Remove(enemy);
            Instance.SetActiveRecursively(enemy, true);
            enemy.transform.parent = null;
            enemy.transform.position = spawnPoint;
        }            
    }
}
