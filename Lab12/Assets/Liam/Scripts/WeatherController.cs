using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    private WeatherManager weatherManager;

    // The city and its country to get weather data for that the user will input.
    [SerializeField] private string cityName;

    [SerializeField] private string countryAbbreviation;

    [Header("Skybox Materials")]
    [SerializeField] private Material sunnySkybox;

    [SerializeField] private Material snowySkybox;
    [SerializeField] private Material rainySkybox;
    [SerializeField] private Material nightSkybox;

    [Header("Directional Lights")]
    [SerializeField] private GameObject sunLight;

    [SerializeField] private GameObject moonLight;
    [SerializeField] private GameObject rainLight;

    private void Start()
    {
        weatherManager = new WeatherManager();
        weatherManager.SetCity(cityName, countryAbbreviation);
        StartCoroutine(weatherManager.GetWeatherJSON(OnWeatherDataReceived));
    }

    private void OnWeatherDataReceived(WeatherResponse weatherData)
    {
        if (weatherData != null)
        {
            // Temperature is given in kelvin.
            // To convert to celsius, subtract 273.15.
            // To convert to fahrenheit, multiply celsius by 9/5 and add 32.
            float temperature = weatherData.Main.Temp;
            float celsisus = temperature - 273.15f;
            float fahrenheit = celsisus * 9 / 5 + 32;
            Debug.Log($"Temperature in Celsius: {celsisus}. In Fahrenheit: {fahrenheit}");

            string condition = weatherData.Weather[0].MainCondition.ToLower();
            Debug.Log($"Condition: {condition}");

            UpdateSkyboxByCondition(condition);
            UpdateSkyboxByTime(weatherData.Sys.Sunrise, weatherData.Sys.Sunset, weatherData.Timezone);

            // Debug the city and country that was looked up so the user knows if it searched for the right place.
            Debug.Log($"{weatherData.City}, {weatherData.Sys.Country}");
        }
    }

    // Update the skybox based on the weather condition.
    private void UpdateSkyboxByCondition(string condition)
    {
        if (condition.Contains("snow"))
        {
            RenderSettings.skybox = snowySkybox;
        }
        else if (condition.Contains("rain"))
        {
            RenderSettings.skybox = rainySkybox;
            rainLight.SetActive(true);
        }
        else
        {
            RenderSettings.skybox = sunnySkybox;
        }
    }

    // Update the skybox based on the time of day.
    private void UpdateSkyboxByTime(double sunriseUnix, double sunsetUnix, int timezone)
    {
        DateTime sunriseTime = UnixTimeToDateTime(sunriseUnix);
        DateTime sunsetTime = UnixTimeToDateTime(sunsetUnix);

        DateTime currentTime = DateTime.UtcNow.AddSeconds(timezone);

        //Debug.Log("Sunrise: " + sunriseTime + " Sunset: " + sunsetTime + " Timezone: " + timezone);

        // If it is currently after sunrise and before sunset, set the skybox to sunny.
        if (currentTime > sunriseTime && currentTime < sunsetTime)
        {
            RenderSettings.skybox = sunnySkybox;
            sunLight.SetActive(true);
            moonLight.SetActive(false);
        }
        // Else, set the skybox to nighttime.
        else
        {
            RenderSettings.skybox = nightSkybox;
            sunLight.SetActive(false);
            moonLight.SetActive(true);
        }
    }

    // Used to convert unix time to a DateTime object.
    private DateTime UnixTimeToDateTime(double unixTime)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTime).ToLocalTime();
        return dateTime;
    }
}