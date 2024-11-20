using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherRunner : MonoBehaviour
{
    public WeatherManager manager;
    

    public void OnJsonDataLoaded(string data)
    {
        Debug.Log(data);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(manager.GetWeatherJson(OnJsonDataLoaded));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
