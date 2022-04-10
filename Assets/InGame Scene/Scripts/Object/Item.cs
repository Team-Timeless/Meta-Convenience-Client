using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Object
{
    [SerializeField]
    protected string _desc = "";        // object 설명

    [SerializeField]
    protected string _info = "";        // 영양 정보

    [SerializeField]
    protected int _price = 0;

    [SerializeField]
    protected bool _isSall = false;     // 팔수 있는지 없는지(재고관리)

    [SerializeField]
    protected float _width = 0.0f;      // 물체의 가로길이 (좌우 간격)

    [SerializeField]
    protected float _height = 0.0f;     // 물체의 세로길이 (앞뒤 간격)

    [SerializeField]
    protected ITEM_ACTIVE _itemActive;      // 아이템 클릭 상태

    public BoxCollider boxcollider = null;

    public string getDesc
    {
        get
        {
            return _desc;
        }
    }

    public string getInfo
    {
        get
        {
            return _info;
        }
    }
    
    public int getPrice
    {
        get
        {
            return _price;
        }
    }

    public bool getIsSall
    {
        get
        {
            return _isSall;
        }
    }

    public float getWidth
    {
        get
        {
            return _width;
        }
    }
    
    public float getHeight
    {
        get
        {
            return _height;
        }
    }

    public ITEM_ACTIVE itemActive
    {
        get
        {
            return _itemActive;
        }
        set
        {
            _itemActive = value;
        }
    }
}
