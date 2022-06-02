using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    private RaycastHit hit;     // <! PC ���� Raycast

    public UnityEngine.UI.Image holdimg = null;     // <! ��Ŭ�� ������ ������ �̹���
    
    public ItemDetails itemDetails = null;      // <! ������ ����

    public Dictionary<string, Item> basket = new Dictionary<string, Item>();        // <! ��ٱ��Ͽ� ����ִ� ������ ����Ʈ

    public int result = 0;      // <! �ٱ��Ͽ� ����ִ� ���� ����

    private static GameMng _Instance;

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))     // <! ��ٱ��� ���� ��� (�ӽ�)
        {
            result = 0;
            foreach(var items in basket)
            {
                result += items.Value.getPrice;
            }
            Debug.Log(result);
        }
    }

    /**
     * @brief RayCast�� �浹 ������ Gameobject ����
     * @param Transform startTrans Ray ���� ��ġ (�ӽ�) ���߿� ĳ���� �� ����� ������� ����
     */
    public GameObject getRayCastGameObject(Transform startTrans)
    {
        if (Physics.Raycast(startTrans.position, startTrans.forward, out hit, 3.0f))
            return hit.collider.gameObject;
        else
        {
            return null;
        }
    }
}
