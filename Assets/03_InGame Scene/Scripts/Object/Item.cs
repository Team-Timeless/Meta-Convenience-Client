using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Item : Object
{
    [SerializeField] protected string _desc = "";        // <! object 설명

    [SerializeField] protected string _info = "";        // <! 영양 정보

    [SerializeField] protected int _price = 0;          // <! 상품 가격

    [SerializeField] protected bool _isSall = false;     // <! 팔수 있는지 없는지(재고관리)

    [SerializeField] protected float _width = 0.0f;      // <! 물체의 가로길이 (좌우 간격)

    [SerializeField] protected float _height = 0.0f;     // <! 물체의 세로길이 (앞뒤 간격)

    [SerializeField] protected ITEM_ACTIVE _itemActive;      // <! 아이템 클릭 상태

    [SerializeField] protected List<string> _tags = new List<string>();     // <! 아이템 태그

    [SerializeField] protected Vector3 _firstPos = Vector3.zero;        // <! 아이템 처음 생성 위치

    [SerializeField] protected MeshRenderer _renderer = null;           // <! 메쉬 렌더러

    // [SerializeField] protected 

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

    public MeshRenderer GetMesh
    {
        get
        {
            return _renderer;
        }
    }

    public void initializeRenderer() => this._renderer?.GetComponent<MeshRenderer>();       // <! 렌더러 초기화

    public void setOutlineColor(Color color) => this._renderer.material.SetColor("_OutlineColor", color);       // <! 테두리 색 설정

    public void setOutlineScale(float scale) => this._renderer.material.SetFloat("_Outline", scale);        // <! 테두ㄹ리 크키 설정

    public void OutlineClear() => this._renderer.material.SetFloat("_Outline", 0.0f);       // <! 테두리 삭제

    public Vector3 setPos() => this._firstPos = this.transform.localPosition;       // <! 물품 처음 생성 위치

    /**
     * @brief json(아이템 데이터 파일) 로드 & 아이템 값 초기화
     * @param int code 각각 아이템 코드
     */
    protected void LoadJsonData(int code)
    {
        object jsonStr = Resources.Load("itemdata");

        JsonData jsondate = JsonMapper.ToObject(jsonStr.ToString());

        // 나중에 이진탐색 적용
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
