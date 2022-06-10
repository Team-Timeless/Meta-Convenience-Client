using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    private RaycastHit hit;     // <! PC 버전 Raycast

    public UnityEngine.UI.Image holdimg = null;     // <! 좌클릭 유지시 나오는 이미지

    public List<ItemDetails> itemDetails = new List<ItemDetails>();      // <! 아이템 정보

    public Dictionary<string, Item> basket = new Dictionary<string, Item>();        // <! 장바구니에 들어있는 아이템 리스트

    public List<ItemBasket> itembasket = new List<ItemBasket>();     // <! 장바구니 UI

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
     * @brief RayCast로 충돌 감지한 Gameobject 리턴
     * @param Transform startTrans Ray 시작 위치 (임시) 나중에 캐릭터 팔 생기면 사라질거 같음
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
     * @brief 아이템 설명 ui 체우기
     * @param Item targetitem 클릭한 상품 아이템 스크립트
     */
    public void setItemDetails(Item targetitem)
    {
        itemDetails[NetworkMng.I.intIsVR].ActiveUI();
        itemDetails[NetworkMng.I.intIsVR]._itemname = targetitem.getName;
        itemDetails[NetworkMng.I.intIsVR]._itemcost = targetitem.getPrice.ToString();
        itemDetails[NetworkMng.I.intIsVR]._itemdetails = targetitem.getDesc;
    }
}
