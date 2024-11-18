using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager
{
    private string jsonApi = "https://api.openweathermap.org/data/2.5/weather?q=Orlando,us&mode=json&appid=075b4b30c5afa318dae5d2f5c390071e";
    private string city = "Orlando";
    private string country = "US";
    private string jsonFilePath = Path.Combine(Application.persistentDataPath, "weather.json");

    // Set the city to search for weather data in.
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
                string jsonData = request.downloadHandler.text;
                SaveJSON(jsonData);
                callback(jsonData);
            }
        }
    }

    public IEnumerator GetWeatherJSON(Action<WeatherResponse> callback)
    {
        // Set the jsonApi to the city and country the user wants to get weather data for.
        jsonApi = $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&mode=json&appid=075b4b30c5afa318dae5d2f5c390071e";
        return CallAPI(jsonApi, jsonData =>
        {
            try
            {
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonData);
                callback(weatherResponse);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse JSON: {e.Message}");
            }
        });
    }

    // Save the json data to a file so we can read it.
    private void SaveJSON(string jsonData)
    {
        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save JSON: {e.Message}");
        }
    }
}

// The classes below are used to deserialize the JSON data from the OpenWeatherMap API.
public class WeatherResponse
{
    [JsonProperty("main")]
    public MainData Main { get; set; }

    [JsonProperty("weather")]
    public List<WeatherData> Weather { get; set; }

    public SystemData Sys { get; set; }

    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    [JsonProperty("name")]
    public string City { get; set; }
}

public class MainData
{
    [JsonProperty("temp")]
    public float Temp { get; set; }
}

public class WeatherData
{
    [JsonProperty("main")]
    public string MainCondition { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}

public class SystemData
{
    [JsonProperty("sunrise")]
    public double Sunrise { get; set; }

    [JsonProperty("sunset")]
    public double Sunset { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }
}