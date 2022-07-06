using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public Coordinate coordinate;
    public WeatherController WeatherController;
    public GPSAPI gpsAPI;
    public TimeManager timeManager;
    
    private GeoData geoData;
    private DateTime dateTime;
    
    private void Start()
    {
        StartCoroutine(CurrentGPSWeatherLoad());
    }

    // 비동기로 바꿔야함
    private IEnumerator CurrentGPSWeatherLoad()
    {
        dateTime = timeManager.LoadTime();
        yield return new WaitUntil(() => dateTime.Year != 1);
        
        gpsAPI.LoadGPS(dateTime);

        geoData = coordinate.DataLoad();
        yield return new WaitUntil(() => geoData != null);
    }
}
