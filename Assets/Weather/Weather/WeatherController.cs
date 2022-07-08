using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Newtonsoft.Json.Linq;
using UnityEngine;


public class WeatherController : MonoBehaviour
{
    private RainController rain;
    
    public enum Weather
    {
        Sun,        // 햇빛
        Cloud,      // 구름만
        Cloudy,     // 흐리다
        Rain,       // 비
        Snow,       // 눈
        Windy       // 바람
    }

    private List<Weather> currentWeathers = new List<Weather>();

    public void InitWeather()
    {
        currentWeathers.Clear();
    }
    
    public void AddWeather(Weather addWeather)
    {
        if (currentWeathers.Contains(addWeather) == false)
        {
            currentWeathers.Add(addWeather);
        }
    }
    
    private void Start()
    {
        rain = FindObjectOfType<RainController>();
    }
    
    private void Update()
    {
        // 테스트 코드
        if (Input.GetKeyDown(KeyCode.F1))
        {
            rain.StopRaining();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            rain.StartRaining();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            rain.StartRaining();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            rain.StartRaining();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            rain.StartRaining();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            rain.StartRaining();
        }
    }
}

    