using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager
{
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=xml&appid=075b4b30c5afa318dae5d2f5c390071e";
    private string jsonFilePath = Path.Combine(Application.persistentDataPath, "weather.json");
    private string city = "Orlando";
    private string country = "US";

    public void SetCity(string cityName, string countryName)
    {
        city = cityName;
        country = countryName;
    }
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
                GetWeatherJson(callback);
            }
        }
    }

    public IEnumerator GetWeatherJson(Action<string> callback)
    {
        return CallAPI(jsonApi, callback);
    }
    

    public void OnJsonDataLoaded(string data)
    {
        Debug.Log(data);
    }
}