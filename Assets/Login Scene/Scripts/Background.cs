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
    private float fadeSpeed = 0.05f;    // fade in/out 속도

    [SerializeField]
    private float minAlpha = 0.25f;     // 알파값 최소값

    void Start()
    {
        spriterenderer.sprite = backGround[0];
        StartCoroutine("FadeIn");
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
        if (index.Equals(backGround.Length)) { index = 0;}
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
}
