using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class ItemInfo : Item
{
    // 임시용
    [SerializeField]
    SHELF_OBJECT en;


    private void Awake()
    {
        //string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/itemdata.json");

        object jsonStr = Resources.Load("itemdata");

        JsonData jsondate = JsonMapper.ToObject(jsonStr.ToString());

        // 나중에 이진탐색 적용
        for (int i = 0; i < jsondate.Count; i++)
        {
            if (int.Parse(jsondate[i]["code"].ToString()) == (int)en)
            {
                _name = jsondate[i]["name"].ToString();
                _desc = jsondate[i]["desc"].ToString();
                _isSall = bool.Parse(jsondate[i]["isSall"].ToString());
                _width = float.Parse(jsondate[i]["width"].ToString());
                _height = float.Parse(jsondate[i]["height"].ToString());
                break;
            }
        }
        _itemActive = ITEM_ACTIVE.NONE;
    }
}
