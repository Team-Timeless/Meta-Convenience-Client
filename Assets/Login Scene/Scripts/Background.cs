using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backGround;

    [SerializeField]
    private SpriteRenderer spriterenderer = null;

    private int index = 0;

    [SerializeField]
    private float fadeSpeed = 0.05f;    // fade in/out �ӵ�

    [SerializeField]
    private float minAlpha = 0.25f;     // ���İ� �ּҰ�

    // ---- Ű���� UI (���߿� vr ���� ó�� �ϱ�)
    [SerializeField]
    private StringBuilder sb = new StringBuilder();

    static private bool isShift = false;

    [SerializeField]
    private GameObject keybord = null;

    [SerializeField]
    private GameObject[] special = new GameObject[2];

    [SerializeField]
    private List<UnityEngine.UI.InputField> input = new List<UnityEngine.UI.InputField>();

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
    }

    void Update()
    {
        setIinputField();
        if(Input.GetKeyDown(KeyCode.Return))
        {
            NetworkMng.I.Login();
        }
    }

    /**
     * @brief ���̵��� (������°�)
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
     * @brief ���̵�ƿ�(��Ÿ���°�)
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

    // ui �κ� (Ű����)

    /**
     * brief Ű���� ��ư Ŭ�� ������
     * @param Ű���� ��ư obj �����ͼ� �̸��� ���ڿ��� ������ֱ�
     */
    public void keyclick(UnityEngine.UI.Button button)
    {
        sb.Append(button.name);
        input[0].text = sb.ToString();
    }

    /**
     * @brief ����Ʈ Ŭ�� �ϸ� Ư������, ���� ��ȯ
     */
    public void shiftClick()
    {
        if (isShift)
        {
            special[0].SetActive(false);        // Ư������
            special[1].SetActive(true);         // ����
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
     * @biref �� �����̽� ����
     */
    public void backSpace()
    {
        if(sb.Length > 0) { sb.Length--; }
        input[0].text = sb.ToString();
    }

    /**
     * @brief Enter ��ư ������ �α���
     */
    public void returnBtn()
    {
        NetworkMng.I.Login();
    }

    /**
     * @brief �����̽� (����) ����
     */
    public void Space()
    {
        sb.Append(" ");
        input[0].text = sb.ToString();
    }

    /**
     * @brief Ű���� �ڷΰ��� ��ư ����
     */
    public void backBtn()
    {
        keybord.SetActive(false);
        input.Clear();
    }

    /**
     * @brief ���ڿ� �ʱ�ȭ �� Ŭ���� ��ǲ �ʵ� ����
     */
    void setIinputField()
    {
        void clear(UnityEngine.UI.InputField field)
        {
            sb.Clear();
            keybord.SetActive(true);
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
