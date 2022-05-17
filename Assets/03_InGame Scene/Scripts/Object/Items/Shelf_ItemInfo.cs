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

    private void Start() 
    {
        setPos();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Map") && _itemActive.Equals(ITEM_ACTIVE.NONE))
        {
            this.transform.localPosition = _firstPos;
            this.transform.rotation = Quaternion.identity;
            Destroy(this.gameObject.GetComponent<Rigidbody>());     // ���� �𸣰ڴ�...
            if(GameMng.I.basket.ContainsKey(this.gameObject.name))
                GameMng.I.basket.Remove(this.gameObject.name);
        }
    }
}
