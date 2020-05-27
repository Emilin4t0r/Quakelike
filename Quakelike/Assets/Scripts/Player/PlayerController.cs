using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject lightning;
    GameObject projSpawn;
    public GameObject lastProj;
    public enum weaponType {wizBall, lightning, MLAW};
    public weaponType weapon;
    void Start()
    {
        lightning = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        projSpawn = transform.GetChild(0).GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (weapon == weaponType.wizBall) {
                lastProj = PoolManager.Instance.wizBalls[0];
                ShootWizBall();
            }
            if (weapon == weaponType.lightning) {
                lightning.GetComponent<Lightning>().Toggle();
            }
            if (weapon == weaponType.MLAW) {
                ShootMLAW();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (weapon == weaponType.wizBall) {
                lastProj.GetComponent<WizBall>().Release();
            }
            if (weapon == weaponType.lightning) {
                lightning.GetComponent<Lightning>().Toggle();
            }
            if (weapon == weaponType.MLAW) {
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) {
        PoolManager.Instance.birdPool.SpawnEnemy(new Vector3(0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.Mouse2)) {
            PoolManager.Instance.boarPool.SpawnEnemy(new Vector3(0, 2, 0));
        }
    }

    void ShootWizBall() {
        int random = Random.Range(0, 3);
        AudioFW.Play("WizSpawn" + random);
        PoolManager.Instance.wizballPool.SpawnProjectile(projSpawn.transform.position, projSpawn.transform.rotation, 30, 5);
    }
    void ShootMLAW() {
        int random = Random.Range(0, 3);
        AudioFW.Play("MLAWFire" + random);       
        PoolManager.Instance.mlawPool.SpawnProjectile(projSpawn.transform.position, projSpawn.transform.rotation, 100, 1);
    }
}
