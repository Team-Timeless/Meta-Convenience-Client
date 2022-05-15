using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

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

    [SerializeField]
    protected List<string> _tags = new List<string>();

    [SerializeField]
    protected Vector3 _firstPos = Vector3.zero;

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

    public Vector3 setPos() => this._firstPos = this.transform.localPosition; 

    /**
     * @brief json(������ ������ ����) �ε� & ������ �� �ʱ�ȭ
     * @param int code ���� ������ �ڵ�
     */
    protected void LoadJsonData(int code)
    {
        object jsonStr = Resources.Load("itemdata");

        JsonData jsondate = JsonMapper.ToObject(jsonStr.ToString());

        // ���߿� ����Ž�� ����
        for (int i = 0; i < jsondate.Count; i++)
        {
            if (int.Parse(jsondate[i]["code"].ToString()) == code)
            {   
                _code = int.Parse(jsondate[i]["code"].ToString());
                _name = jsondate[i]["name"].ToString();
                _desc = jsondate[i]["desc"].ToString();
                _price = int.Parse(jsondate[i]["price"].ToString());
                _isSall = bool.Parse(jsondate[i]["isSall"].ToString());
                _width = float.Parse(jsondate[i]["width"].ToString());
                _height = float.Parse(jsondate[i]["height"].ToString());
                foreach(var tags in jsondate[i]["tags"])
                {
                    _tags.Add(tags.ToString());
                }
                break;
            }
        }
    }
}
