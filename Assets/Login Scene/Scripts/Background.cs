using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        spriterenderer.sprite = backGround[0];
        StartCoroutine("FadeIn");
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
        if (index.Equals(backGround.Length)) { index = 0;}
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
}
