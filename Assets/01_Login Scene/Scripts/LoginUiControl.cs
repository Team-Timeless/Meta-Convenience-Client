using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LoginUiControl : MonoBehaviour
{
    public GameObject[] division = new GameObject[2];       // <! VR PC 구별 PC 0 VR 1

    // ---- 키보드 부분
    public StringBuilder inputString = new StringBuilder();     // <! inputfield에 들어갈 문장 구성

    static bool isShift = false;        // <! shift 가 눌렸는지

    public GameObject keybord = null;      // <! 키보드

    [SerializeField] private GameObject[] special = new GameObject[2];       // <! 특수 문자

    public UnityEngine.UI.InputField input;     // <! 선택된 inputfield

    public static bool Shift
    {
        get { return isShift; }
    }

    void Start()
    {
        keybord.transform.position = new Vector3(9999.0f, 9999.0f, 9999.0f);
        division[NetworkMng.I.intIsVR].SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadingBar.LoadScene("InGame Scene");
        }
    }

    // ui 부분 (키보드)

    /**
     * brief 키보드 버튼 클릭 했을떄
     * @param 키보드 버튼 obj 가져와서 이름을 문자열로 만들어주기
     */
    public void keyclick(UnityEngine.UI.Button button)
    {
        Debug.LogError(inputString.ToString());
        inputString.Append(button.name);
        input.text = inputString.ToString();
    }

    /**
     * @brief 쉬프트 클릭 하면 특수문자, 숫자 전환
     */
    public void shiftClick()
    {
        if (isShift)
        {
            special[0].SetActive(false);        // 특수문자
            special[1].SetActive(true);         // 숫자
            isShift = false;
        }
        else
        {
            special[0].SetActive(true);
            special[1].SetActive(false);
            isShift = true;
        }
    }

    /**
     * @biref 백 스페이스 구현
     */
    public void backSpace()
    {
        if (inputString.Length > 0) { inputString.Length--; }
        input.text = inputString.ToString();
    }

    /**
     * @brief Enter 버튼 누르면 로그인
     */
    public void returnBtn()
    {
        NetworkMng.I.Login();
    }

    /**
     * @brief 스페이스 (띄어쓰기) 구현
     */
    public void Space()
    {
        inputString.Append(" ");
        input.text = inputString.ToString();
    }

    /**
     * @brief 키보드 뒤로가기(창 닫기)) 버튼 구현
     */
    public void backBtn()
    {
        keybord.transform.position = new Vector3(9999.0f, 9999.0f, 9999.0f);
    }
}
