using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Object
{
    protected string _desc = "";        // object ����
    protected string _info = "";        // ���� ����
    protected bool _isSall = false;     // �ȼ� �ִ��� ������(������)
    protected float _width = 0.0f;      // ��ü�� ���α���
    protected float _height = 0.0f;     // ��ü�� ���α���

    [SerializeField]
    protected ITEM_ACTIVE _itemActive;      // ������ Ŭ�� ����

    public string getDesc
    {
        get
        {
            return _desc;
        }
    }

    public string getInfo
    {
        get{
            return _info;
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

    public ITEM_ACTIVE getActive
    {
        get
        {
            return _itemActive;
        }
    }
    
    protected ITEM_ACTIVE setActive
    {
        set
        {
            _itemActive = value;
        }
    }
}
