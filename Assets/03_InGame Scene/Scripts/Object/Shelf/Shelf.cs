using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items;     // 선반위에 올라가는 gameobjct들

    //[SerializeField]
    //private List<Panel> panel = new List<Panel>();

    private Item tempItem;

    private Panel tempPanel;

    int widthcount = 0;     // 가로 최대 몇개 (X)
    int heightcount = 0;    // 세로 최대 몇개 (Z)

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tempPanel = transform.GetChild(i).GetComponent<Panel>();        // 양날의 검이다 추후에 렉걸리면 바꾸자...
            if (tempPanel != null)
            {
                CreateItem(tempPanel, (int)tempPanel.getCode);
                //panel.Add(temp);
            }
        }
    }

    /**
     * @brief 물품 생성
     * @param Panel panel 선반 스크립트
     * @param int code 아이템 코드
     */
    void CreateItem(Panel panel, int code)
    {
        for (int i = 0; i <= widthcount; i++)
        {
            for(int j = 0; j <= heightcount; j++)
            {
                tempItem = Instantiate(items[code], Vector3.zero, Quaternion.identity).GetComponent<Item>();
                if (i.Equals(0))
                {
                    widthcount = Convert.ToInt32(panel.getWidth * 2 / tempItem.getWidth);
                    heightcount = Convert.ToInt32(panel.getHeight * 2 / tempItem.getHeight);
                }
                if(!j.Equals(0))
                {
                    tempItem.boxcollider.enabled = false;
                }
                tempItem.transform.localPosition = new Vector3(panel.getWidth - tempItem.getWidth * i, 0.003f, panel.getHeight - tempItem.getHeight * j);
                tempItem.transform.SetParent(panel.transform, false);
            }
        }
    }
}
