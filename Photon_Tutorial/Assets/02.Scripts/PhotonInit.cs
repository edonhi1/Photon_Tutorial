using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonInit : MonoBehaviour {

    public Text logText;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings("photon_example01");         //어플리케이션 이름을 사용한다.
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");
        PhotonNetwork.JoinRandomRoom();        
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Room.");
        PhotonNetwork.CreateRoom("MyRoom");
    }

    void OnCreatedRoom()
    {
        Debug.Log("Finish Make a Room.");     
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined Room.");
        StartCoroutine(CreatPlayer());
    }

    IEnumerator CreatPlayer()
    {
        PhotonNetwork.Instantiate("MyPlayer", new Vector3(0, 1, 0), Quaternion.identity, 0);
        yield return null;
    }

    private void Update()
    {
        logText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }
}
