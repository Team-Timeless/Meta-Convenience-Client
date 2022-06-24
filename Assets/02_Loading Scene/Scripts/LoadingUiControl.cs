using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUiControl : MonoBehaviour
{
    public GameObject[] division = new GameObject[2];       // <! VR PC 구별 PC 0 VR 1

    void Start()
    {
        division[NetworkMng.I.intIsVR].SetActive(true);
    }
}
