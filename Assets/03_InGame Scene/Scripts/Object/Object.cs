using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] protected int _code = 0;        // <! objcet �ڵ�

    [SerializeField] protected string _name = "";        // <! object �̸�

    [SerializeField] protected TextMesh _textMesh = null;        // <! ���콺 Ȥ�� ��Ʈ�ѷ� ������ � ������ �̸�
    
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
