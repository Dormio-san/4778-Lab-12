using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager
{
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=xml&appid=075b4b30c5afa318dae5d2f5c390071e";

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback);
    }
}