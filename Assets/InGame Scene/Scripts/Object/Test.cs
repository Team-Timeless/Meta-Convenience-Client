using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Item
{
    private void Awake()
    {
        _name = "테스트";
        _desc = "테스트 내용";
        _isSall = true;
        _width = 0.006f;
        _height = 0.006f;
        _itemActive = ITEM_ACTIVE.NONE;
    }
}
