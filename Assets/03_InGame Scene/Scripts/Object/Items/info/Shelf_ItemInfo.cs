using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Shelf_ItemInfo : Item
{
    // 임시용
    [SerializeField] SHELF_OBJECT en;

    private void Awake()
    {
        LoadJsonData((int)en);
        _itemActive = ITEM_ACTIVE.NONE;
    }

    private void Start()
    {
        initializeRenderer();
        setPos();
    }
    private void Update()
    {
        if (_itemActive.Equals(ITEM_ACTIVE.HOLD))
        {
            setOutlineColor(Color.cyan);
            setOutlineScale(0.12f);
            if (NetworkMng.I.isVR)
            {
                if (NetworkMng.I.pointer[1].isGrip)
                    transform.position = NetworkMng.I.pointer[1].transform.position;
                else
                {
                    itemActive = ITEM_ACTIVE.NONE;
                    setOutlineScale(0f);
                }
            }

        }
    }

    private void OnMouseEnter()
    {
        setOutlineColor(_isSall ? Color.yellow : Color.red);
        setOutlineScale(0.12f);
    }

    private void OnMouseExit()
    {
        OutlineClear();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map") && _itemActive.Equals(ITEM_ACTIVE.NONE))
        {
            this.transform.localPosition = _firstPos;
            this.transform.localRotation = Quaternion.identity;
            Destroy(this.gameObject.GetComponent<Rigidbody>());     // 나는 모르겠다...
            if (GameMng.I.basket.ContainsKey(this.gameObject.name))
                GameMng.I.basket.Remove(this.gameObject.name);
        }
    }
}
