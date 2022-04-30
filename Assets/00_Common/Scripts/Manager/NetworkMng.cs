using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;
using Photon.Realtime;

public class NetworkMng : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public UnityEngine.UI.InputField inputfildId = null;
    [SerializeField]
    public UnityEngine.UI.InputField inputfildPwd = null;

    public string nickname = "";

    private static NetworkMng _Instance;
    public static NetworkMng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("Instance is null");
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _Instance = this;

        // ���߿� �ҽ�
        //PhotonNetwork.GameVersion = "1.0";      // ���� ����
        //PhotonNetwork.ConnectUsingSettings();   // ���� ����
    }

    /**
     * @brief ȸ������ â���� �̵�
     */
    public void SignUp()
    {
        System.Diagnostics.Process.Start("https://google.com");
    }

    public void ConnectToServer()
    {
        PhotonNetwork.GameVersion = "1.0";      // ���� ����
        PhotonNetwork.ConnectUsingSettings();   // ���� ����
    }
    /**
     * @brief ��������� �α���
     */
    public void Login()
    {
        if(Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            // ���ͳ��� ���� �ȵǾ� ������
            // �α��� ���� UI ������ּ���
            Debug.Log("network disconnected");
        }
        else
        {
            // TODO ���� ���º��� �α��� ����
            Debug.Log(inputfildId.text);
            Debug.Log(inputfildPwd.text);

            LoadingBar.LoadScene("InGame Scene");
        }
    }

    /**
     * @brief Master�������� ���� ���� callback �Լ�
     */
    public override void OnConnectedToMaster()
    {
        //Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom(); // ���� room ���°�
    }

    /**
     * @brief ���� �� ���� ���� ������ callback �Լ�
     * @param short returnCode ���� �ڵ�
     * @param string message ���� �޽���
     */
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        //Debug.Log("no room");
        PhotonNetwork.CreateRoom("myroom");     // �� ����
    }

    /**
     * @brief �� ���� �Ǿ����� callback�Լ�
     */
    public override void OnCreatedRoom()
    {
        //Debug.Log("Created room");
    }

    /**
     * @brief �� ���� ���� ������ callback �Լ�
     */
    public override void OnJoinedRoom()
    {
        StartCoroutine(this.CreatePlayer());
        //Debug.Log("Joined room");
    }

    /**
     * @brief player ����
     */
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
