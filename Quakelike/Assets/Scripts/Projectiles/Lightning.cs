using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {

    public LineRenderer lr;
    public int vertices;
    public float width;
    public GameObject player;
    public GameObject[] basePositions;
    public bool isOn;
    public Vector3[] linePositions;
    RaycastHit hit;
    public float hitDist;
    bool hitEnemy;

    private void Start() {
        linePositions = new Vector3[vertices];
        lr.positionCount = vertices;
        basePositions = new GameObject[vertices];
        for (int i = 0; i < vertices; i++) {
            basePositions[i] = transform.GetChild(i).GetChild(0).gameObject;
        }
    }

    void FixedUpdate() {
        if (isOn) {
            if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out hit, 500)) {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward, Color.green);
                if (hit.transform.tag == "Obstacle") {
                    hitDist = Vector3.Distance(transform.position, hit.transform.position);
                    hitEnemy = false;
                }
                if (hit.transform.tag == "Enemy") {
                    hitDist = Vector3.Distance(transform.position, hit.transform.position);
                    hitEnemy = true;
                }

            }
            else {
                hitDist = 15;
                hitEnemy = false;
            }

            for (int i = 0; i < vertices; i++) {
                linePositions[i] = basePositions[i].transform.position;
            }
            linePositions[0] = transform.position;
            if (hitEnemy)
                linePositions[4] = hit.transform.position;
            lr.SetPositions(linePositions);
        }
        else {
            for (int i = 0; i < vertices; i++) {
                linePositions[i] = new Vector3(0, 0, 0);
            }
            lr.SetPositions(linePositions);
        }
    }

    public void Toggle() {
        if (isOn) {
            isOn = false;
        }
        else {
            isOn = true;
        }
    }
}
