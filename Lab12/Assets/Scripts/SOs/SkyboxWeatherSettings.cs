using UnityEngine;

[CreateAssetMenu(fileName = "SkyboxWeatherSettings", menuName = "Weather/SkyboxSettings", order = 1)]
public class SkyboxWeatherSettings : ScriptableObject
{
    public string conditionName;
    public Material skyboxMaterial;
    public GameObject directionalLight;
}