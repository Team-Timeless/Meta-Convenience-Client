using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    private RaycastHit hit;

    private static GameMng _Instance;

    public UnityEngine.UI.Image holdimg = null;
    
    public ItemDetails itemDetails = null;

    public Dictionary<string, Item> basket = new Dictionary<string, Item>();

    public int result = 0;
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
        if(Input.GetKeyDown(KeyCode.Q))     // <! ��ٱ��� ���� ���
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
        if (Physics.Raycast(startTrans.position, startTrans.forward, out hit, 15.0f))
            return hit.collider.gameObject;
        else
        {
            return null;
        }
    }
}
