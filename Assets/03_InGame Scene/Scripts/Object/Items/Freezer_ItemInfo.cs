using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Freezer_ItemInfo : Item
{
    // �ӽÿ�
    [SerializeField]
    FREEZER_OBEJCT en;

    private void Awake()
    {
        LoadJsonData((int)en);
        _itemActive = ITEM_ACTIVE.NONE;
    }
}
