using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    protected int _code = 0;        // objcet 코드

    protected string _name = "";        // object 이름

    protected TextMesh _textMesh = null;        // 마우스 혹은 컨트롤러 가져다 댈때 나오는 이름

    // protected Material[] _outLine = new Material[3];        //  outline 데두리 재질 3d 파일이 어떻게 나오는지 확인후 
    
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
