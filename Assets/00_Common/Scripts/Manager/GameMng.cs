using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    private RaycastHit hit;     // <! PC ���� Raycast

    public UnityEngine.UI.Image holdimg = null;     // <! ��Ŭ�� ������ ������ �̹���

    public List<ItemDetails> itemDetails = new List<ItemDetails>();      // <! ������ ����

    public Dictionary<string, Item> basket = new Dictionary<string, Item>();        // <! ��ٱ��Ͽ� ����ִ� ������ ����Ʈ

    public List<ItemBasket> itembasket = new List<ItemBasket>();     // <! ��ٱ��� UI

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            itembasket[0].gameObject.SetActive(true);
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

    /**
     * @brief ������ ���� ui ü���
     * @param Item targetitem Ŭ���� ��ǰ ������ ��ũ��Ʈ
     */
    public void setItemDetails(Item targetitem)
    {
        itemDetails[NetworkMng.I.intIsVR].ActiveUI();
        itemDetails[NetworkMng.I.intIsVR]._itemname = targetitem.getName;
        itemDetails[NetworkMng.I.intIsVR]._itemcost = targetitem.getPrice.ToString();
        itemDetails[NetworkMng.I.intIsVR]._itemdetails = targetitem.getDesc;
    }
}
