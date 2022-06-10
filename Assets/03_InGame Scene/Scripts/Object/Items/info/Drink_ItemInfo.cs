using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Drink_ItemInfo : Item
{
    // юс╫ц©К
    [SerializeField] DRINK_OBJECT en;

    private void Awake()
    {
        LoadJsonData((int)en);
        _itemActive = ITEM_ACTIVE.NONE;
    }
}
