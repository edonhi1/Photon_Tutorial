using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 10f;
    public float fireRange = 300f;
    public float damage = 10f;

    private Transform tr;
    private Vector3 spawnPoint;

	void Start () {
        tr = GetComponent<Transform>();
        spawnPoint = tr.position;
	}
	
	void Update () {
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
        if((spawnPoint - tr.position).sqrMagnitude > fireRange)
        {
            Destroy(gameObject);
        }
	}
}
