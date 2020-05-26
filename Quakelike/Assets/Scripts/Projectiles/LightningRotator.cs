using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRotator : MonoBehaviour
{
    public float spinSpeed;
    public float distFromPlayer;
    public float distFromRotator;
    GameObject pos;
    Lightning lightning;
    public int index;

    private void Start() {
        pos = transform.GetChild(0).gameObject;
        lightning = transform.parent.GetComponent<Lightning>();
        index = int.Parse(name.TrimStart('R', 'o', 't', 'a', 't', 'o', 'r'));
    }

    void Update() {
        if (lightning.isOn) {
            float distFromTarget = lightning.hitDist;
            distFromPlayer = (distFromTarget / 4) * index;
            distFromRotator = Random.Range(0, 0.5f) * (distFromTarget / 10);
            transform.Rotate(new Vector3(0, 0, spinSpeed));
            transform.localPosition = new Vector3(0, 0, distFromPlayer);
            pos.transform.localPosition = new Vector3(distFromRotator, 0, 0);
        }
    }
}
