using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 포톤 보이스 디버그용
 */
public class TextGroup : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text[] content_Text= new UnityEngine.UI.Text[3];

    public string setName
    {
        set { content_Text[0].text = value; }
    }

    public string setCount
    {
        set { content_Text[1].text = value; }
    }

    public string setPrice
    {
        set { content_Text[2].text = value; }
    }
}
