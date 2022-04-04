using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.InputField inputfildId = null;

    [SerializeField]
    UnityEngine.UI.InputField inputfildPwd = null;

    /**
     * @brief 웹통신으로 로그인
     */
    public void Login()
    {
        // TODO 서버 상태보고 로그인 구현
        Debug.Log(inputfildId.text);
        Debug.Log(inputfildPwd.text);
    }
    
    /**
     * @brief 회원가입 창으로 이동
     */
    public void SignUp()
    {
        System.Diagnostics.Process.Start("https://google.com");
    }
}
