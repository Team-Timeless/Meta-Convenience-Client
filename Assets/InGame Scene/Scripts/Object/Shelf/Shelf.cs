using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items;     // �������� �ö󰡴� gameobjct��

    //[SerializeField]
    //private List<Panel> panel = new List<Panel>();

    private Item tempItem;

    private Panel tempPanel;

    int widthcount = 0;     // ���� �ִ� � (X)
    int heightcount = 0;    // ���� �ִ� � (Z)

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tempPanel = transform.GetChild(i).GetComponent<Panel>();        // �糯�� ���̴� ���Ŀ� ���ɸ��� �ٲ���...
            if (tempPanel != null)
            {
                CreateItem(tempPanel, (int)tempPanel.getCode);
                //panel.Add(temp);
            }
        }
    }

    /**
     * @brief ��ǰ ����
     * @param Panel panel ���� ��ũ��Ʈ
     * @param int code ������ �ڵ�
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
