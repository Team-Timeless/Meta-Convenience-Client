using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkMng : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0";      // ���� ����
        PhotonNetwork.ConnectUsingSettings();   // ���� ����
    }

    /**
     * @brief Master�������� ���� ���� callback �Լ�
     */
    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom(); // ���� room ���°�
    }

    /**
     * @brief ���� �� ���� ���� ������ callback �Լ�
     * @param short returnCode ���� �ڵ�
     * @param string message ���� �޽���
     */
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        Debug.Log("no room");
        PhotonNetwork.CreateRoom("myroom");     // �� ����
    }

    /**
     * @brief �� ���� �Ǿ����� callback�Լ�
     */
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    /**
     * @brief �� ���� ���� ������ callback �Լ�
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
