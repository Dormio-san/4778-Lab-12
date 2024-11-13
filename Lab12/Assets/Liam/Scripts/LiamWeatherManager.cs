using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LiamWeatherManager : MonoBehaviour
{
    private string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=xml&appid=075b4b30c5afa318dae5d2f5c390071e";
    private string city = "Orlando";


    private void Start()
    {
        StartCoroutine(GetWeatherJSON((json) =>
        {
            Debug.Log(json);
        }));
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
                Debug.Log(request.downloadHandler.text);
                RenderSettings.skybox = new Material(Shader.Find("Skybox/Procedural"));
            }
        }
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        //jsonApi = $"http://api.openweathermap.org/data/2.5/weather?q={city},us&mode=xml&appid=075b4b30c5afa318dae5d2f5c390071e";
        return CallAPI(jsonApi, callback);
    }
}