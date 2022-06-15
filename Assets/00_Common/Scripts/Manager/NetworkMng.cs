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
    public UnityEngine.UI.InputField inputfildId = null;        // <! 로그인할때 값 가져오기위해
    public UnityEngine.UI.InputField inputfildPwd = null;       // <! 로그인할때 값 가져오기위해

    public string nickname = "";        // <! 닉네임

    public delegate void InitializeLaserEvent();        // <! laserpointer 이벤트 등록 delegate

    public InitializeLaserEvent leftHandEvent;        // <! 왼손 이벤트 초기화용
    public InitializeLaserEvent rightHandEvent;       // <! 오른손 이벤트 초기화용

    public Custom_LaserPointer[] pointer = new Custom_LaserPointer[2];      // <! 왼손 오른손 컨트롤러

    public UnityEngine.UI.Text[] failedtxt = new UnityEngine.UI.Text[2];       // <! 로그인 실패 이거나 인터넷 연결이 안되어 있을떄 0 pc 1 vr

    private static NetworkMng _Instance;

    public bool isVR = true;        // <! vr 구별

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
     * @brief 회원가입 창으로 이동
     */
    public void SignUp()
    {
        System.Diagnostics.Process.Start("https://google.com");
    }

    public void ConnectToServer()
    {
        PhotonNetwork.GameVersion = "1.0";      // 게임 버전
        PhotonNetwork.ConnectUsingSettings();   // 서버 연결
    }

    /**
     * @brief 웹통신으로 로그인
     */
    public void Login()
    {
        try
        {
            // 로그인
        }
        catch
        {
            // 로그인 실패 UI 만들어주세요
        }
        if (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            // 인터넷이 연결 안되어 있을
            failedtxt[intIsVR].gameObject.SetActive(true);
            failedtxt[intIsVR].text = "인터넷 연결이 되어있지 않습니다.";

            Debug.Log("network disconnected");
        }
        else
        {
            // TODO 서버 상태보고 로그인 구현
            LoadingBar.LoadScene("InGame Scene");
        }
    }

    /**
     * @brief Master권한으로 서버 연결 callback 함수
     */
    public override void OnConnectedToMaster()
    {
        //Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();      // 렌덤 room 들어가는곳
    }

    /**
     * @brief 랜덤 방 들어가기 실패 했을때 callback 함수
     * @param short returnCode 에러 코드
     * @param string message 에러 메시지
     */
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        //Debug.Log("no room");
        PhotonNetwork.CreateRoom("myroom");     // 방 생성
    }

    /**
     * @brief 방 생성 되었을때 callback함수
     */
    public override void OnCreatedRoom()
    {
        //Debug.Log("Created room");
    }

    /**
     * @brief 방 들어간거 성공 했을때 callback 함수
     */
    public override void OnJoinedRoom()
    {
        StartCoroutine(this.CreatePlayer());
        //Debug.Log("Joined room");
    }
    /**
     * @brief player 생성
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
     * @brief 오른손 컨트롤러 event 델리게이트 추가
     * @param InitializeLaserEvent func 이벤트 초기화 함수
     */
    public void RightHandEventAdd(InitializeLaserEvent func) => this.rightHandEvent += func;

    /**
     * @brief 오른손 컨트롤러 event 델리게이트 추가
     * @param InitializeLaserEvent func 이벤트 초기화 함수
     */
    public void LeftHandEventAdd(InitializeLaserEvent func) => this.leftHandEvent += func;

    /**
     * @brief 오른손 컨트롤러 event 델리게이트 삭제
     * @param InitializeLaserEvent func 이벤트 초기화 함수
     */
    public void RightHandEventRemove(InitializeLaserEvent func) => this.rightHandEvent -= func;

    /**
     * @brief 오른손 컨트롤러 event 델리게이트 삭제
     * @param InitializeLaserEvent func 이벤트 초기화 함수
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
