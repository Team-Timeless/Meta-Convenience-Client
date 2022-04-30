using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Shelf_ItemInfo : Item
{
    // 임시용
    [SerializeField]
    SHELF_OBJECT en;

    private void Awake()
    {
        LoadJsonData((int)en);
        _itemActive = ITEM_ACTIVE.NONE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Map"))
        {
            this.transform.localPosition = _firstPos;
            this.transform.rotation = Quaternion.identity;
            Destroy(this.gameObject.GetComponent<Rigidbody>());     // 나는 모르겠다...
        }
    }
}
