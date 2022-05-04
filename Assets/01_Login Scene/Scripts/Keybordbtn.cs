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
        // 쉬프트 눌렸을때 처리
        if(Background.Shift)        // 대문자
        {
            gameObject.name = gameObject.name.ToUpper();
            txt.text = gameObject.name;
        }
        else        // 소문자
        {
            gameObject.name = gameObject.name.ToLower();
            txt.text = gameObject.name;
        }
    }
}
