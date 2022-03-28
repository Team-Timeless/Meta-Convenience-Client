using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    protected int _code = 0;

    protected string _name = "";        // object 이름
    protected string _desc = "";        // object 설명

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
