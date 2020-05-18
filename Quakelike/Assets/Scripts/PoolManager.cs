using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    public ProjectilePool flarePool;
    [SerializeField] List<GameObject> flares;
    public EnemyPool boarPool;
    [SerializeField] List<GameObject> boars;

    void Awake() {
        Instance = this;

        flarePool = new ProjectilePool(transform.GetChild(0).gameObject, flares);
        boarPool = new EnemyPool(transform.GetChild(1).gameObject, boars);
    }
    public void SetActiveRecursively(GameObject target, bool state) {
        target.SetActive(state);
        for (int i = 0; i < target.transform.childCount; i++) {
            target.transform.GetChild(i).gameObject.SetActive(state);
        }
    }
    public void RunCor(IEnumerator cor) {
        StartCoroutine(cor);
    }

    public class Pool {
        public GameObject poolObject;
        public List<GameObject> entities;

        public Pool(GameObject poolObject_, List<GameObject> entities_) { //Constructor
            poolObject = poolObject_;
            entities = entities_;
            for (int i = 0; i < poolObject.transform.childCount; i++) {                
                entities.Add(poolObject.transform.GetChild(i).gameObject);
                SetActiveRecursively(entities[i], false);
            }
        }
        public void SetActiveRecursively(GameObject target, bool state) {
            target.SetActive(state);
            for (int i = 0; i < target.transform.childCount; i++) {
                target.transform.GetChild(i).gameObject.SetActive(state);
            }
        }
        public IEnumerator ReturnEntity(GameObject entity, float time) {
            yield return new WaitForSeconds(time);
            entity.transform.position = poolObject.transform.position;
            entity.transform.parent = poolObject.transform;
            entities.Add(entity);
            SetActiveRecursively(entity, false);
        }
    }

    public class ProjectilePool : Pool {
        public ProjectilePool(GameObject poolObject_, List<GameObject> entities_) : base(poolObject_, entities_) { }//Constructor

        public void SpawnProjectile(Vector3 spawnPoint, Quaternion rotation, float shootForce) {
            Rigidbody proj = entities[0].gameObject.GetComponent<Rigidbody>();
            entities.Remove(proj.gameObject);
            SetActiveRecursively(proj.gameObject, true);
            proj.transform.parent = null;
            proj.transform.position = spawnPoint;
            proj.transform.rotation = rotation;
            proj.AddForce(proj.transform.forward * shootForce, ForceMode.Impulse);
            Instance.RunCor(ReturnEntity(proj.gameObject, 10));
        }        
    }

    public class EnemyPool : Pool {
        public EnemyPool(GameObject poolObject_, List<GameObject> entities_) : base(poolObject_, entities_) { }//Constructor

        public void SpawnEnemy(Vector3 spawnPoint) {
            GameObject enemy = entities[0];
            entities.Remove(enemy);
            SetActiveRecursively(enemy, true);
            enemy.transform.parent = null;
            enemy.transform.position = spawnPoint;
        }            
    }
}
