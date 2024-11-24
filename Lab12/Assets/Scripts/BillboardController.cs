using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using static System.Net.WebRequestMethods;

public class BillboardController : MonoBehaviour
{
    // Array of various images that will be used for the billboards.
    private string[] webImages = {"https://m.media-amazon.com/images/M/MV5BYzI1MDk0NTktNTk4NC00MDhiLWE4NmEtODI0ZDY0ZTQ0MjQ3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg",
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTvmIZSs6w6Ps6ivJYpHjeWMo8kF6-7qRO1mg&s",
        "https://s7d9.scene7.com/is/image/lamar/Extension?qlt=85&wid=1600&ts=1716227253532&fit=fit,1&dpr=off",
        "https://ichef.bbci.co.uk/ace/standard/976/cpsprodpb/16620/production/_91408619_55df76d5-2245-41c1-8031-07a4da3f313f.jpg",
        "https://stacycorwin.com/wp-content/uploads/2018/12/sunrise-sunset.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/CargoNet_Di_12_Euro_4000_L%C3%B8nsdal_-_Bolna.jpg/640px-CargoNet_Di_12_Euro_4000_L%C3%B8nsdal_-_Bolna.jpg"};

    // List of random numbers.
    private List<int> randomNumbers = new List<int>();

    private void Awake()
    {
        // Rnadomizes which image is chosen for each billboard.
        for (int i = 0; i < webImages.Length; i++)
        {
            randomNumbers.Add(i);
        }
    }

    protected void OnImageDownloaded(Texture2D billboardImageTexture)
    {
        this.GetComponent<MeshRenderer>().material.mainTexture = billboardImageTexture;
        //Initializes the GameAnalytics
        GameAnalytics.Initialize();
        //Sends data that the images were downloaded successfully
        GameAnalytics.NewDesignEvent("Images Downloaded");
    }

    // Randomly choose an image from the array of images.
    protected string PickImage()
    {
        // If there are no more random images to display, display a default image.
        if (randomNumbers.Count == 0)
        {
            Debug.Log("No more images to display.");
            return "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg";
        }

        // Get a random image, then remove it from the list of random images.
        int index = Random.Range(0, randomNumbers.Count);
        int value = randomNumbers[index];

        randomNumbers.RemoveAt(index);

        // Spit out the random image.
        return webImages[value];
    }
}