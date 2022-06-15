using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;
using Photon.Realtime;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using System;
using Valve.VR;

public class NetworkMng : MonoBehaviourPunCallbacks
{
    public UnityEngine.UI.InputField inputfildId = null;        // <! �α����Ҷ� �� ������������
    public UnityEngine.UI.InputField inputfildPwd = null;       // <! �α����Ҷ� �� ������������

    public string nickname = "";        // <! �г���

    public delegate void InitializeLaserEvent();        // <! laserpointer �̺�Ʈ ��� delegate

    public InitializeLaserEvent leftHandEvent;        // <! �޼� �̺�Ʈ �ʱ�ȭ��
    public InitializeLaserEvent rightHandEvent;       // <! ������ �̺�Ʈ �ʱ�ȭ��

    public Custom_LaserPointer[] pointer = new Custom_LaserPointer[2];      // <! �޼� ������ ��Ʈ�ѷ�

    public UnityEngine.UI.Text[] failedtxt = new UnityEngine.UI.Text[2];       // <! �α��� ���� �̰ų� ���ͳ� ������ �ȵǾ� ������ 0 pc 1 vr

    private static NetworkMng _Instance;

    public bool isVR = true;        // <! vr ����

    public int intIsVR
    {
        get
        {
            return isVR ? 1 : 0;
        }
    }

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

        isVR = XRGeneralSettings.Instance.Manager.activeLoader;
        Debug.Log("IS VR : " + isVR);

        RightHandEventAdd(rightHandEvent);
        LeftHandEventAdd(leftHandEvent);
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
        try
        {
            // �α���
        }
        catch
        {
            // �α��� ���� UI ������ּ���
        }
        if (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            // ���ͳ��� ���� �ȵǾ� ����
            failedtxt[intIsVR].gameObject.SetActive(true);
            failedtxt[intIsVR].text = "���ͳ� ������ �Ǿ����� �ʽ��ϴ�.";

            Debug.Log("network disconnected");
        }
        else
        {
            // TODO ���� ���º��� �α��� ����
            LoadingBar.LoadScene("InGame Scene");
        }
    }

    /**
     * @brief Master�������� ���� ���� callback �Լ�
     */
    public override void OnConnectedToMaster()
    {
        //Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();      // ���� room ���°�
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
        // VR
        if (isVR)
        {
            PhotonNetwork.Instantiate("PlayerVR", new Vector3(0, 2, 0), Quaternion.identity, 0);
        }
        // Window
        else
        {
            PhotonNetwork.Instantiate("Player", new Vector3(0, 2, 0), Quaternion.identity, 0);
        }

        yield return null;
    }

    /**
     * @brief ������ ��Ʈ�ѷ� event ��������Ʈ �߰�
     * @param InitializeLaserEvent func �̺�Ʈ �ʱ�ȭ �Լ�
     */
    public void RightHandEventAdd(InitializeLaserEvent func) => this.rightHandEvent += func;

    /**
     * @brief ������ ��Ʈ�ѷ� event ��������Ʈ �߰�
     * @param InitializeLaserEvent func �̺�Ʈ �ʱ�ȭ �Լ�
     */
    public void LeftHandEventAdd(InitializeLaserEvent func) => this.leftHandEvent += func;

    /**
     * @brief ������ ��Ʈ�ѷ� event ��������Ʈ ����
     * @param InitializeLaserEvent func �̺�Ʈ �ʱ�ȭ �Լ�
     */
    public void RightHandEventRemove(InitializeLaserEvent func) => this.rightHandEvent -= func;

    /**
     * @brief ������ ��Ʈ�ѷ� event ��������Ʈ ����
     * @param InitializeLaserEvent func �̺�Ʈ �ʱ�ȭ �Լ�
     */
    public void LeftHandEventRemove(InitializeLaserEvent func) => this.leftHandEvent -= func;

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    private void OnApplicationQuit()
    {
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
