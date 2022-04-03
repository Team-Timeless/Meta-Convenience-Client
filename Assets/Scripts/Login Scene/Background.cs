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
        while (spriterenderer.color.a > 0)
        {
            spriterenderer.color -= new Color(0, 0, 0, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        index++;
        spriterenderer.sprite = backGround[index];
        if (index.Equals(backGround.Length)) { index = 0;}
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
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine("FadeIn");

    }
}
