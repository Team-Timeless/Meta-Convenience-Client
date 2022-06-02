using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private SHELF_OBJECT objectcode;       // <! 선반에 올려질 아이템 코드

    [SerializeField] private float width = 0.033f;      // <! 선반 가로길이
    [SerializeField] private float height = 0.01f;      // <! 선반 세로길이

    public int getCode
    {
        get
        {
            return (int)objectcode;
        }
    }
    public float getWidth
    {
        get
        {
            return width;
        }
    }
    public float getHeight
    {
        get
        {
            return height;
        }
    }
}
