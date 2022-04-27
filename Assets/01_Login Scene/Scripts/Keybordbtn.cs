using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybordbtn : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text txt = null;

    void Start() 
    {
        txt = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        // ����Ʈ �������� ó��
        if(Background.Shift)        // �빮��
        {
            gameObject.name = gameObject.name.ToUpper();
            txt.text = gameObject.name;
        }
        else        // �ҹ���
        {
            gameObject.name = gameObject.name.ToLower();
            txt.text = gameObject.name;
        }
    }
}
