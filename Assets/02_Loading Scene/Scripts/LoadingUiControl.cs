using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUiControl : MonoBehaviour
{
    public GameObject[] division = new GameObject[2];       // <! VR PC 구별 PC 0 VR 1

    void Start()
    {
        if (!NetworkMng.I.isVR)
            division[0].SetActive(true);
        else
            division[1].SetActive(true);
    }
}
