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
     * @brief ��������� �α���
     */
    public void Login()
    {
        // TODO ���� ���º��� �α��� ����
        Debug.Log(inputfildId.text);
        Debug.Log(inputfildPwd.text);
    }
    
    /**
     * @brief ȸ������ â���� �̵�
     */
    public void SignUp()
    {
        System.Diagnostics.Process.Start("https://google.com");
    }
}
