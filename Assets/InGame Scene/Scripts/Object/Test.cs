using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Item
{
    private void Awake()
    {
        _name = "�׽�Ʈ";
        _desc = "�׽�Ʈ ����";
        _isSall = true;
        _width = 0.006f;
        _height = 0.006f;
        _itemActive = ITEM_ACTIVE.NONE;
    }
}
