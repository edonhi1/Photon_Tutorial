using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {

    private PhotonView pv;
    private NavMeshAgent nvAgent;
    private Transform tr;
    private Transform target;

    public GameObject[] players;

    public float traceTime = 0.5f;

    private Vector3 currPos;
    private Quaternion currRot;

	void Start () {
        pv = PhotonView.Get(this);
        pv.ObservedComponents[0] = this; 
        nvAgent = GetComponent<NavMeshAgent>();
        tr = GetComponent<Transform>();

        if (PhotonNetwork.isMasterClient)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            target = players[0].transform;

            currPos = tr.position;

            float dist = (target.position - tr.position).sqrMagnitude;
            foreach (GameObject _player in players)
            {
                if ((_player.transform.position - tr.position).sqrMagnitude < dist)
                {
                    target = _player.transform;
                    break;
                }
            }
            nvAgent.destination = target.position;
        }
	}

	void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if (Time.time > traceTime)
            {
                players = GameObject.FindGameObjectsWithTag("Player");

                float dist = (target.position - tr.position).sqrMagnitude;
                foreach (GameObject _player in players)
                {
                    if ((_player.transform.position - tr.position).sqrMagnitude < dist)
                    {
                        target = _player.transform;
                        break;
                    }
                }
                nvAgent.destination = target.position;

                traceTime = Time.time + 0.5f;
            }
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
            tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //자신의 플레이는 다른 네트워크 사용자에게 내 정보를 송신한다.
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            //타 플레이어의 정보는 수신한다.
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
