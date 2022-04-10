using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Object
{
    [SerializeField]
    protected string _desc = "";        // object ����

    [SerializeField]
    protected string _info = "";        // ���� ����

    [SerializeField]
    protected int _price = 0;

    [SerializeField]
    protected bool _isSall = false;     // �ȼ� �ִ��� ������(������)

    [SerializeField]
    protected float _width = 0.0f;      // ��ü�� ���α��� (�¿� ����)

    [SerializeField]
    protected float _height = 0.0f;     // ��ü�� ���α��� (�յ� ����)

    [SerializeField]
    protected ITEM_ACTIVE _itemActive;      // ������ Ŭ�� ����

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
