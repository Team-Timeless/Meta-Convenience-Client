using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class ServerTime : MonoBehaviour
{
    private DateTime dateTime;
    public string url = "time.windows.com";

    private bool isLoadTime = false;

    public Text serverTimeText;

    private void Start()
    {
        StartCoroutine(WebChk());
    }

    private void Update()
    {
        if (isLoadTime)
        {
            Debug.Log(dateTime.Hour);
            isLoadTime = false;

            float totalSecond = (dateTime.Hour * 3600) + (dateTime.Minute * 60) + (dateTime.Second);
            Debug.Log(totalSecond);
            FindObjectOfType<DayCycleManager>().StartDay(totalSecond);
        }
    }
    
    private IEnumerator WebChk() 
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else 
            {
                string date = request.GetResponseHeader("date"); 
 
                dateTime = DateTime.Parse(date).ToLocalTime(); 
                Debug.Log("한국 시간 으로 변환"+dateTime);

                serverTimeText.text = "서버 시간 : " + dateTime;
                isLoadTime = true;
            }
        }
    }
    
    // 한국시간으로 변환시켜 준다.
    // Debug.Log("받아온 시간"+date); // GMT
}