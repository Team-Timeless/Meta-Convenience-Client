using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkMng : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0";      // 게임 버전
        PhotonNetwork.ConnectUsingSettings();   // 서버 연결
    }

    /**
     * @brief Master권한으로 서버 연결 callback 함수
     */
    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom(); // 렌덤 room 들어가는곳
    }

    /**
     * @brief 랜덤 방 들어가기 실패 했을때 callback 함수
     * @param short returnCode 에러 코드
     * @param string message 에러 메시지
     */
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        Debug.Log("no room");
        PhotonNetwork.CreateRoom("myroom");     // 방 생성
    }

    /**
     * @brief 방 생성 되었을때 callback함수
     */
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    /**
     * @brief 방 들어간거 성공 했을때 callback 함수
     */
    public override void OnJoinedRoom()
    {
        StartCoroutine(this.CreatePlayer());
        Debug.Log("Joined room");
    }

    IEnumerator CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0, 2, 0), Quaternion.identity, 0);

        yield return null;
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}
