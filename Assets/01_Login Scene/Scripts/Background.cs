using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Valve.VR;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] backGround;

    [SerializeField] private SpriteRenderer spriterenderer = null;

    private int index = 0;

    [SerializeField] private float fadeSpeed = 0.05f;    // fade in/out 속도

    [SerializeField] private float minAlpha = 0.25f;     // 알파값 최소값

    // ---- 키보드 부분
    [SerializeField] private StringBuilder inputString = new StringBuilder();

    static private bool isShift = false;        // <! shift 가 눌렸는지

    [SerializeField] private GameObject keybord = null;      // <! 키보드

    [SerializeField] private GameObject[] special = new GameObject[2];       // <! 특수 문자 

    [SerializeField] private List<UnityEngine.UI.InputField> input = new List<UnityEngine.UI.InputField>();      // <! 아이디 비번 inputfield

    public static bool Shift
    {
        get
        {
            return isShift;
        }
    }

    void Start()
    {
        spriterenderer.sprite = backGround[0];
        StartCoroutine("FadeIn");

        if(NetworkMng.I.isVR)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        setIinputField();
        if(Input.GetKeyDown(KeyCode.Return))
        {
            NetworkMng.I.Login();
        }
        // if(SteamVR_Input)
    }

    /**
     * @brief 페이드인 (사라지는거)
     */
    IEnumerator FadeIn()
    {
        while (spriterenderer.color.a > minAlpha)
        {
            spriterenderer.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(fadeSpeed);
        }

        index++;
        if (index.Equals(backGround.Length)) { index = 0; }
        spriterenderer.sprite = backGround[index];
        StartCoroutine("FadeOut");
    }

    /**
     * @brief 페이드아웃(나타나는거)
     */
    IEnumerator FadeOut()
    {
        while (spriterenderer.color.a < 1)
        {
            spriterenderer.color += new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(fadeSpeed);
        }
        StartCoroutine("FadeIn");
    }

    // ui 부분 (키보드)

    /**
     * brief 키보드 버튼 클릭 했을떄
     * @param 키보드 버튼 obj 가져와서 이름을 문자열로 만들어주기
     */
    public void keyclick(UnityEngine.UI.Button button)
    {
        inputString.Append(button.name);
        input[0].text = inputString.ToString();
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
        if(inputString.Length > 0) { inputString.Length--; }
        input[0].text = inputString.ToString();
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
        input[0].text = inputString.ToString();
    }

    /**
     * @brief 키보드 뒤로가기 버튼 구현
     */
    public void backBtn()
    {
        keybord.SetActive(false);
        input.Clear();
    }

    /**
     * @brief 문자열 초기화 후 클릭한 인풋 필드 저장
     */
    void setIinputField()
    {
        void clear(UnityEngine.UI.InputField field)
        {
            inputString.Clear();
            if(NetworkMng.I.isVR)
            {
                keybord.SetActive(true);
            }
            if (input.Count != 0) { input.RemoveAt(0); }
            input.Add(field);
        }

        if (NetworkMng.I.inputfildId.isFocused)
        {
            clear(NetworkMng.I.inputfildId);
        }
        else if (NetworkMng.I.inputfildPwd.isFocused)
        {
            clear(NetworkMng.I.inputfildId);
        }
    }
}
