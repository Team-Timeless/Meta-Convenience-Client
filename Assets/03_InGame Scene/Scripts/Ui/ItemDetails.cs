using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text itemname = null;        // <! 상품 이름

    [SerializeField] private UnityEngine.UI.Text itemcost = null;        // <! 상품 가격

    [SerializeField] private UnityEngine.UI.Text itemdetails = null;     // <! 상품 정보

    private void Start() 
    {
        // GameMng.I.itemDetails.Add(this);
    }
    public GameObject _gameobject
    {
        get
        {
            return this.gameObject;
        }
    }
    
    public string _itemname
    {
        set
        {
            if(itemname != null)
                itemname.text = value;
        }
    }

    public string _itemcost
    {
        set
        { 
            if(itemcost != null)
                itemcost.text = value;
        }
    }
    
    public string _itemdetails
    {
        set
        { 
            if(itemdetails != null)
                itemdetails.text = value;
        }
    }

    /*
     * @brief 상품 정보창 닫기 버튼
     */
    public void BackBtn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
