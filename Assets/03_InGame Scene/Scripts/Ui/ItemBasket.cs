using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ItemBasket : MonoBehaviour
{
    private StringBuilder itemstr = new StringBuilder();

    [SerializeField] private UnityEngine.UI.Text basketContents = null;        // <! 상품 이름, 가격 떠있는 스크롤

    [SerializeField] private UnityEngine.UI.Text itemcost = null;        // <! 상품 가격

    [SerializeField] private GameObject uiActive;

    public int result = 0;      // <! 바구니에 들어있는 가격 측정

    public bool getUiActive
    {
        get
        {
            return uiActive.activeSelf;
        }
    }

    void Start()
    {
        GameMng.I.itembasket.Add(this);

        result = 0;
    }

    public void ActiveUI()
    {
        Cursor.lockState = CursorLockMode.None;
        uiActive.SetActive(true);

        foreach (var item in GameMng.I.basket)
        {
            result += item.Value.getPrice;
            itemstr.Append(item.Value.getName).Append("\t").Append(item.Value.getPrice).Append("\n");
        }

        // basketscroll.text = itemstr.ToString();
        itemcost.text = "총 가격 : " + result.ToString();
    }

    /**
     * @brief 상품 정보창 닫기
     */
    public void UnActiveUI() 
    {
        // Cursor.lockState = CursorLockMode.Locked;
        uiActive.SetActive(false);
    }

    public void sibal()
    {
        Debug.Log("sibal wrkxsp");
    }
}
