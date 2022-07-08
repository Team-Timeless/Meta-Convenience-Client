using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items;     // <! 선반위에 올라가는 gameobjct들

    private Item tempItem;

    private Panel tempPanel;

    private int widthcount = 0;     // <! 가로 최대 몇개 (X)
    private int heightcount = 0;    // <! 세로 최대 몇개 (Z)
    private int count = 0;

    private StringBuilder itemsname = new StringBuilder();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tempPanel = transform.GetChild(i).GetComponent<Panel>();        // <! 양날의 검이다 추후에 렉걸리면 바꾸자...
            if (tempPanel != null)
            {
                CreateItem(tempPanel, (int)tempPanel.getCode);
            }
        }
    }

    /**
     * @brief 물품 생성
     * @param Panel panel 선반 스크립트\
     * @param int code 아이템 코드
     */
    void CreateItem(Panel panel, int code)
    {
        for (int i = 0; i <= widthcount; i++)
        {
            for(int j = 0; j <= heightcount; j++)
            {
                tempItem = Instantiate(items[code], Vector3.zero, Quaternion.identity).GetComponent<Item>();
                itemsname.Clear();
                itemsname.Append(tempItem.name).Append(count);
                tempItem.name = itemsname.ToString();
                if (i.Equals(0))
                {
                    widthcount = Convert.ToInt32(panel.getWidth * 2 / tempItem.getWidth);
                    heightcount = Convert.ToInt32(panel.getHeight * 2 / tempItem.getHeight);
                }
                tempItem.transform.localPosition = new Vector3(panel.getWidth - tempItem.getWidth * i, 0.003f, panel.getHeight - tempItem.getHeight * j);
                tempItem.transform.SetParent(panel.transform, false);
                count++;
            }
        }
    }
}
