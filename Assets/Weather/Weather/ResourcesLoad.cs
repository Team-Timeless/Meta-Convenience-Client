using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ResourcesLoad : MonoBehaviour
{
    // Temp Execute Function
    private void Awake()
    {
        
    }

    public void WeatherIconLoad()
    {
        Resources.LoadAll<Sprite>("Assets/InGame Scene/Scripts/Weather/WeatherIcon");
        
        
    }

}
