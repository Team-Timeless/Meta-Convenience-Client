using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inputfiled : VR_UiInput
{
    [SerializeField] private UnityEngine.UI.InputField input;       // <! 아이디 or 비번 inputfeild
    [SerializeField] LoginUiControl uicontrol;      // <! UIControl 안에 인풋 필드 넣기위해

    void Start()
    {
        input = GetComponent<UnityEngine.UI.InputField>();
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
        if (e.target.name == this.gameObject.name && selected == true && input)
        {
            uicontrol.keybord.transform.localPosition = new Vector3(0, -90.0f, -190.0f);
            uicontrol.inputString.Clear();
            uicontrol.input = input;
            uicontrol.input.text = uicontrol.inputString.ToString();
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
