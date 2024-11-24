using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeatherController : MonoBehaviour
{
    private WeatherManager weatherManager;

    // The city and its country to get weather data for that the user will input.
    private string cityName;

    private string countryAbbreviation;

    // Ref to the script that controls the skybox.
    private SkyboxController skyboxController;

    // values for the UI
    public TMP_Text output_text;

    public TMP_InputField input_field;

    private void Start()
    {
        // Set ref to the skybox controller.
        skyboxController = GameObject.Find("SkyboxController").GetComponent<SkyboxController>();

        weatherManager = new WeatherManager();

        //Set up the basic text UI to display what is tested
        output_text.text = "Weather In: " + cityName;

        input_field.onEndEdit.AddListener(OnInputEnd);
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

            UpdateSkyboxByTime(weatherData.Sys.Sunrise, weatherData.Sys.Sunset, weatherData.Timezone);
            UpdateSkyboxByCondition(condition);

            // Debug the city and country that was looked up so the user knows if it searched for the right place.
            Debug.Log($"{weatherData.City}, {weatherData.Sys.Country}");
        }
    }

    // Update the skybox based on the weather condition.
    private void UpdateSkyboxByCondition(string condition)
    {
        if (condition.Contains("snow"))
        {
            skyboxController.SetSkyboxAndLight("snow");
        }
        else if (condition.Contains("rain"))
        {
            skyboxController.SetSkyboxAndLight("rain");
        }
    }

    // Update the skybox based on the time of day.
    private void UpdateSkyboxByTime(double sunriseUnix, double sunsetUnix, int timezone)
    {
        // Add the timezone to the sunrise and sunset times to get the sunrise and sunset times in the looked up city's timezone.
        sunriseUnix += timezone;
        sunsetUnix += timezone;

        // Convert the Unix epoch time to date time for easy readability.
        DateTime sunriseTime = UnixTimeToDateTime(sunriseUnix);
        DateTime sunsetTime = UnixTimeToDateTime(sunsetUnix);

        // Set the current time in the inputted city.
        DateTime currentTime = DateTime.UtcNow.AddSeconds(timezone);

        // Debug the sunrise, sunset, and current time in the inputted city.
        Debug.Log("Sunrise: " + sunriseTime + " Sunset: " + sunsetTime + " Current Time: " + currentTime);

        // If it is currently an hour before or after sunrise, set the skybox to sunrise.
        if (currentTime <= sunriseTime.AddHours(1) && currentTime >= sunriseTime.AddHours(-1))
        {
            skyboxController.SetSkyboxAndLight("sunrise");
        }
        // Else if it is currently an hour before or after sunset, set the skybox to sunset.
        else if (currentTime <= sunsetTime.AddHours(1) && currentTime >= sunsetTime.AddHours(-1))
        {
            skyboxController.SetSkyboxAndLight("sunset");
        }
        // Else if it is daytime, set the skybox to daytime.
        else if (currentTime > sunriseTime && currentTime < sunsetTime)
        {
            skyboxController.SetSkyboxAndLight("day");
        }
        // Else, set the skybox to nighttime.
        else
        {
            skyboxController.SetSkyboxAndLight("night");
        }
    }

    // Used to convert unix time to a DateTime object.
    private DateTime UnixTimeToDateTime(double unixTime)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTime);
        return dateTime;
    }

    public void OnInputEnd(string userInput)
    {
        cityName = userInput;
        output_text.text = $"Weather In: {cityName}";

        // If the user enters more than just the city name, split the string into another string if there is a comma.
        // A comma is used to separate the city name and country abbreviation because city names can have spaces.
        string[] searchInfo = cityName.Split(",");

        // If the array has more than 1 item (there is a city name and country abbreviation), set the city and country.
        if (searchInfo.Length > 1)
        {
            cityName = searchInfo[0];
            countryAbbreviation = searchInfo[1];
        }

        // Set the city and country in the weather manager and get the weather data.
        weatherManager.SetCity(cityName, countryAbbreviation);
        StartCoroutine(weatherManager.GetWeatherJSON(OnWeatherDataReceived));
    }
}