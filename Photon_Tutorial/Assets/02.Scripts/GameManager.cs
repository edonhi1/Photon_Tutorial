using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private PhotonView pv;
    public Transform[] spawnPoint;
    public float creatTime = 3.0f;

	void Start () {
        pv = PhotonView.Get(this);
        spawnPoint = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
	}
	
	void Update () {
		if(PhotonNetwork.connected && PhotonNetwork.isMasterClient)
        {
            if (Time.time > creatTime)
            {
                MakeEnemy();
                creatTime = Time.time + 3.0f;
            }
        }
	}

    void MakeEnemy()
    {
        StartCoroutine(this.CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        int idx = Random.Range(1, spawnPoint.Length);
        PhotonNetwork.InstantiateSceneObject("Enemy", spawnPoint[idx].position, Quaternion.identity, 0, null);

        yield return null;
    }
}
