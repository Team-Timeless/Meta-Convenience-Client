using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerVR_input : MonoBehaviour
{
    // VR 왼손, 오른손 컨트롤러
    public SteamVR_Input_Sources right_hand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources left_hand = SteamVR_Input_Sources.LeftHand;

    [SerializeField] private GameObject basketUI;

    // VR 컨트롤러 액션
    public SteamVR_Action_Vector2 vrInputVec2 = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("Move");
    public SteamVR_Action_Boolean jump = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Jump");
    public SteamVR_Action_Boolean BasketBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("BasketBtn");

    public bool isBasket = false;

    public UnityEngine.UI.Scrollbar scroll;

    // Update is called once per frame
    void Update()
    {
        ControllerClick();
    }

    void ControllerClick()
    {
        if (BasketBtn.GetStateDown(left_hand))
        {
            if (!basketUI.activeSelf)
            {
                isBasket = true;
                basketUI.SetActive(true);
            }
            else
            {
                isBasket = false;
                basketUI.SetActive(false);
            }
        }
    }
}
