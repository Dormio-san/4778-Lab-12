using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    // Array of skybox settings for different weather conditions.
    [SerializeField] private SkyboxWeatherSettings[] skyboxWeatherSettings;

    // The directional light in the scene.
    [SerializeField] private GameObject directionalLight;

    // Apply the skybox and light settings to the scene.
    private void ApplySkyboxSettings(SkyboxWeatherSettings settings)
    {
        try
        {
            RenderSettings.skybox = settings.skyboxMaterial;
            ApplyLightProperties(settings.directionalLight.GetComponent<Light>(), directionalLight.GetComponent<Light>());
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error applying skybox settings: " + e.Message);
        }
    }

    // Public function that is called from WeatherController.cs to set the skybox and light based on the weather condition.
    public void SetSkyboxAndLight(string condition)
    {
        foreach (SkyboxWeatherSettings settings in skyboxWeatherSettings)
        {
            if (settings.conditionName.Equals(condition, System.StringComparison.OrdinalIgnoreCase))
            {
                ApplySkyboxSettings(settings);
                return;
            }
        }

        Debug.LogWarning("Skybox settings not found for condition: " + condition);
    }

    private void ApplyLightProperties(Light settingsLight, Light worldLight)
    {
        // Set the location of the light.
        directionalLight.transform.position = settingsLight.transform.position;

        // Set all the light properties that are different from the default light.
        worldLight.color = settingsLight.color;
        worldLight.intensity = settingsLight.intensity;
        worldLight.shadows = settingsLight.shadows;
        worldLight.shadowStrength = settingsLight.shadowStrength;
        worldLight.shadowResolution = settingsLight.shadowResolution;
        worldLight.shadowBias = settingsLight.shadowBias;
        worldLight.shadowNormalBias = settingsLight.shadowNormalBias;
        worldLight.shadowNearPlane = settingsLight.shadowNearPlane;
    }
}
