using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using UnityEngine;
using System.Text;
using System.Web;
using System.Xml;
using Newtonsoft.Json.Linq;

public class GPSAPI : MonoBehaviour
{
    static HttpClient client = new HttpClient();

    public int inputX, inputY;
    
    public void LoadGPS(DateTime dateTime)
    {
        string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtFcst"; // URL
        url += "?serviceKey=" + "VIwXGl%2FoHKOlPKdjxWiIiHU254C5N58x0K2HVftREVh5YuS3%2FReY0CtL6F5M39O39IhnghBTo0Cpt6ptdGYPvw%3D%3D"; // Service Key
        
        url += "&pageNo=1";
        url += "&numOfRows=1000";
                // XML, JSON 를 지원함
        url += "&dataType=JSON";    
        url += "&base_date=" + dateTime.ToString("yyyyMMdd");;
        url += "&base_time=" + dateTime.ToString("HHmm");
        url += "&nx=" + inputX;
        url += "&ny=" + inputY;
        
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
            
        string results = string.Empty;
        HttpWebResponse response;
        using (response = request.GetResponse() as HttpWebResponse)
        {
            StreamReader reader = new StreamReader(response.GetResponseStream());
            results = reader.ReadToEnd();
        }
        
        Debug.Log(results);
        
        // JObject jObj = JObject.Parse(results);
        // Debug.Log(jObj["body"].ToString());
        // Debug.Log(jObj["response"]);
        // Json 노드 값
        // notice = jObj["resultCode"].ToString();
    }
}
