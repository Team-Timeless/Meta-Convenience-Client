using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] protected int _code = 0;        // <! objcet 코드

    [SerializeField] protected string _name = "";        // <! object 이름

    [SerializeField] protected TextMesh _textMesh = null;        // <! 마우스 혹은 컨트롤러 가져다 댈때 나오는 이름
    
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
}
