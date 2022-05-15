using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    private RaycastHit hit;

    private static GameMng _Instance;

    public UnityEngine.UI.Image holdimg = null;
    
    public ItemDetails itemDetails = null;

    public static GameMng I
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

    void Awake()
    {
        NetworkMng.I.ConnectToServer();
        _Instance = this;
    }

    /**
     * @brief RayCast로 충돌 감지한 Gameobject 리턴
     * @param Transform startTrans Ray 시작 위치 (임시) 나중에 캐릭터 팔 생기면 사라질거 같음
     */
    public GameObject getRayCastGameObject(Transform startTrans)
    {
        if (Physics.Raycast(startTrans.position, startTrans.forward, out hit, 15.0f))
            return hit.collider.gameObject;
        else
        {
            return null;
        }
    }
}
