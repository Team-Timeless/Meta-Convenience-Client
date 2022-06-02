using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class VR_UiInput : MonoBehaviour
{
    protected bool selected;

    /*
     * @brief 왼손 event초기화
     * @param PointerEventHandler pointerIn 컨트롤러 포인트가 ui 에 들어갔을 떄
     * @param PointerEventHandler pointerOut 컨트롤러 포인트가 ui 에 빠졌을 떄
     * @param PointerEventHandler pointerClick 컨트롤러 포인트 ui 안에 있을떄 클릭
     */
    protected void leftPoinerinAdd(PointerEventHandler pointerIn, PointerEventHandler pointerOut, PointerEventHandler pointerClick)
    {
        NetworkMng.I.pointer[0].PointerIn += pointerIn;
        NetworkMng.I.pointer[0].PointerClick += pointerClick;
        NetworkMng.I.pointer[0].PointerOut += pointerOut;
    }

    /*
    * @brief 왼손 event초기화
    * @param PointerEventHandler pointerIn 컨트롤러 포인트가 ui 에 들어갔을 떄
    * @param PointerEventHandler pointerOut 컨트롤러 포인트가 ui 에 빠졌을 떄
    * @param PointerEventHandler pointerClick 컨트롤러 포인트 ui 안에 있을떄 클릭
    */
    protected void rightPointerinAdd(PointerEventHandler pointerIn, PointerEventHandler pointerOut, PointerEventHandler pointerClick)
    {
        NetworkMng.I.pointer[1].PointerIn += pointerIn;
        NetworkMng.I.pointer[1].PointerClick += pointerClick;
        NetworkMng.I.pointer[1].PointerOut += pointerOut;
    }

    protected void RemoveEvent(PointerEventHandler pointerIn, PointerEventHandler pointerOut, PointerEventHandler pointerClick)
    {
        for (int i = 0; i < NetworkMng.I.pointer.Length; i++)
        {
            if (NetworkMng.I.pointer[i])
            {
                NetworkMng.I.pointer[i].PointerIn -= pointerIn;
                NetworkMng.I.pointer[i].PointerClick -= pointerClick;
                NetworkMng.I.pointer[i].PointerOut -= pointerOut;
            }
        }
    }
    public virtual void PointerInside(object sender, PointerEventArgs e)
    {
        Debug.Log("Adfdfdafsdfasdfasdfasdfsd");
        if (e.target.name == this.gameObject.name && selected == false)
        {
            selected = true;
            Debug.Log("pointer is inside this object : " + e.target.name);
        }
    }
    public virtual void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true)
        {
            selected = false;
            Debug.Log("pointer is outside this object : " + e.target.name);
        }
    }

    public virtual void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true)
        {
            Debug.Log("pointer is click this object : " + e.target.name);
        }
    }

    protected bool get_selected_value()
    {
        return selected;
    }
}
