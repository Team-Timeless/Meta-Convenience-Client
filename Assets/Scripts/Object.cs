using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    protected int _code = 0;

    protected string _name = "";        // object �̸�
    protected string _desc = "";        // object ����

    public int getCode
    {
        get
        {
            return _code;
        }
    }

    public string getName
    {
        get
        {
            return _name;
        }
    }

    public string getDesc
    {
        get
        {
            return _desc;
        }
    }
}
