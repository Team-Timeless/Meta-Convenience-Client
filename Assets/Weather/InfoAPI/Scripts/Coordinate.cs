using UnityEngine;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Web;
using Newtonsoft.Json;

[System.Serializable]
public class GeoData
{
    public string Country;
    public string CountryCode;
    public string Region;
    public string RegionName;
    public string City;
    public string Zip;
    public string Lat;
    public string Lon;
    public string IP;
}

public class Coordinate : MonoBehaviour
{
    public string jsonURL = "http://ip-api.com/json";

    private GeoData geoData;
    
    /// <summary>
    /// 현재 위치 정보를 전달합니다.
    /// </summary>
    /// <returns></returns>
    public GeoData DataLoad()
    {
        geoData = new GeoData();
        
        string jsonStr = JsonUtility.ToJson(geoData);
        StartCoroutine(GeoDataHttpLoadJson(jsonURL, jsonStr));
        return geoData;
    }
    
    /// <summary>
    /// 서버로부터 현재 위치 정보를 가져옵니다. 
    /// </summary>
    /// <param name="URL">요청 주소</param>
    /// <param name="jsonStr">적용할 json 파일</param>
    /// <returns></returns>

    private IEnumerator GeoDataHttpLoadJson(string URL, string jsonStr)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, jsonStr))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonStr);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                geoData = JsonConvert.DeserializeObject<GeoData>(json);

                // 위치 정보 테스트
                if (geoData != null)
                {
                    Debug.Log(geoData.City);
                    Debug.Log(geoData.Country);
                    Debug.Log(geoData.Region);
                    Debug.Log(geoData.Zip);
                    Debug.Log(geoData.IP);
                    Debug.Log(geoData.RegionName);
                }

                if (geoData == null)
                {
                    Debug.LogError("날씨 JSON을 불러오지 못했습니다.");
                }
            }
        }
    }
}