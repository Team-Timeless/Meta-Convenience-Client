using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Btn : VR_UiInput
{
    [SerializeField] private UnityEngine.UI.Button btn;     // <! target 버튼

    void Start()
    {
        btn = GetComponent<UnityEngine.UI.Button>();
        NetworkMng.I.LeftHandEventAdd(leftInitialize);
        NetworkMng.I.RightHandEventAdd(rightInitialize);
    }

    /*
     * @brief 왼손 event초기화
     */
    void leftInitialize()
    {
        leftPoinerinAdd(PointerInside, PointerOutside, PointerClick);
    }
    /*
     * @brief 오른손 event초기화
     */
    void rightInitialize()
    {
        rightPointerinAdd(PointerInside, PointerOutside, PointerClick);
    }

    public override void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true && btn)
        {
            btn.onClick.Invoke();
            Debug.Log("pointer is click this object : " + e.target.name);
        }
    }
    
    private void OnDestroy() 
    {
        RemoveEvent(PointerInside, PointerOutside, PointerClick);
    
        NetworkMng.I.LeftHandEventRemove(leftInitialize);
        NetworkMng.I.RightHandEventRemove(rightInitialize);
    }
}
