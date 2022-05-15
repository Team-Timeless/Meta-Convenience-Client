using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetails : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text itemname = null;

    [SerializeField]
    private UnityEngine.UI.Text itemcost = null;

    [SerializeField]
    private UnityEngine.UI.Text itemdetails = null;

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
    
    public void BackBtn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
