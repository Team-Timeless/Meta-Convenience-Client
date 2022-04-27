using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Shelf_ItemInfo : Item
{
    // �ӽÿ�
    [SerializeField]
    SHELF_OBJECT en;

    private void Awake()
    {
        LoadJsonData((int)en);
        _itemActive = ITEM_ACTIVE.NONE;
    }
}
