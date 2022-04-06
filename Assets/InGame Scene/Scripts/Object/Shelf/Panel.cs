using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField]
    private SHELF_OBJECT objectcode;

    [SerializeField]
    private float width = 0.033f;
    [SerializeField]
    private float height = 0.01f;

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
