using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybordbtn : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text txt = null;        // <! 키보드 버튼 안에 txt 가져오기 (대문자 하면 바꿔주기 위해)

    void Start() 
    {
        txt = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        // 쉬프트 눌렸을때 처리
        if(LoginUiControl.Shift)        // <! 대문자 일때
        {
            gameObject.name = gameObject.name.ToUpper();
            txt.text = gameObject.name;
        }
        else        // <! 소문자 일때
        {
            gameObject.name = gameObject.name.ToLower();
            txt.text = gameObject.name;
        }
    }
}
