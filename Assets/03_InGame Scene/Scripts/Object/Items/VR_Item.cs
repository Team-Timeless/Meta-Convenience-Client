using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Item : VR_UiInput
{
    [SerializeField] private Item item;
    void Awake()
    {
        item = GetComponent<Item>();
        NetworkMng.I.LeftHandEventAdd(leftInitialize);
        NetworkMng.I.RightHandEventAdd(rightInitialize);
    }

    /*
     * @brief 왼손 event초기화
     */
    void leftInitialize()
    {
        NetworkMng.I.pointer[0].PointerGrip += PointerGarpGrip;
        leftPoinerinAdd(PointerInside, PointerOutside, PointerClick);
    }
    
    /*
     * @brief 오른손 event초기화
     */
    void rightInitialize()
    {
        NetworkMng.I.pointer[1].PointerGrip += PointerGarpGrip;
        rightPointerinAdd(PointerInside, PointerOutside, PointerClick);
    }


    public override void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == false && item)
        {
            selected = true;
            item.setOutlineScale(0.12f);
            item.setOutlineColor(Color.yellow);
        }
    }

    public override void PointerGarpGrip(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true && item)
        {
            if (item.itemActive.Equals(ITEM_ACTIVE.NONE))
            {
                if (!item.gameObject.GetComponent<Rigidbody>())
                {
                    item.gameObject.AddComponent<Rigidbody>();
                }
                item.itemActive = ITEM_ACTIVE.HOLD;

                if (!GameMng.I.basket.ContainsKey(item.name))
                    GameMng.I.basket.Add(item.name, item);
            }
        }
    }

    public override void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true && item)
        {
            GameMng.I.setItemDetails(item);
        }
    }

    public override void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true && item)
        {
            selected = false;
                item.itemActive = ITEM_ACTIVE.NONE;
            item.setOutlineScale(0f);
        }
    }

    private void OnDestroy()
    {
        RemoveEvent(PointerInside, PointerOutside, PointerClick);

        NetworkMng.I.LeftHandEventRemove(leftInitialize);
        NetworkMng.I.RightHandEventRemove(rightInitialize);
    }
}
