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
                // Save the json data to a string to then save it and send it to the callback.
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
            // Try to parse the json data and send it to the callback.
            try
            {
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonData);
                callback(weatherResponse);
            }
            // If there is some sort of error while parsing the json data, log the error.
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse JSON: {e.Message}");
            }
        });
    }

    // Save the json data to a file so we can read it.
    private void SaveJSON(string jsonData)
    {
        // Try to save the json data to a file.
        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
        }
        // If there is some sort of error while saving the json data, log the error.
        catch (Exception e)
        {
            Debug.LogError($"Failed to save JSON: {e.Message}");
        }
    }
}

// The classes below are used to deserialize the JSON data from the OpenWeatherMap API.
public class WeatherResponse
{
    // Seraches for "main" in the json data and assigns it to the MainData class.
    [JsonProperty("main")]
    public MainData Main { get; set; }

    // Searches for "weather" in the json data and assigns it to the WeatherData class.
    [JsonProperty("weather")]
    public List<WeatherData> Weather { get; set; }

    // Searches for "sys" in the json data and assigns it to the SystemData class.
    public SystemData Sys { get; set; }

    // Searches for "timezone" in the json data and assigns it to the Timezone variable.
    [JsonProperty("timezone")]
    public int Timezone { get; set; }

    // Searches for "name" in the json data and assigns it to the City variable.
    [JsonProperty("name")]
    public string City { get; set; }
}

public class MainData
{
    // Searches for the "temp" in the json data and assigns it to the Temp variable, which is used to indicate the temperature.
    [JsonProperty("temp")]
    public float Temp { get; set; }
}

public class WeatherData
{
    // Once within the "weather" object, searches for "main" and assigns it to the MainCondition variable.
    [JsonProperty("main")]
    public string MainCondition { get; set; }

    // Also searches for "description" and assigns it to the Description variable.
    [JsonProperty("description")]
    public string Description { get; set; }
}

public class SystemData
{
    // Once within the "sys" object, searches for "sunrise" and assigns it to the Sunrise variable.
    [JsonProperty("sunrise")]
    public double Sunrise { get; set; }

    // Also searches for "sunset" and assigns it to the Sunset variable.
    [JsonProperty("sunset")]
    public double Sunset { get; set; }

    // Finally, searches for "country" and assigns it to the Country variable.
    [JsonProperty("country")]
    public string Country { get; set; }
}