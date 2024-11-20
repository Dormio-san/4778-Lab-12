
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    // Image of the duck song.
    [SerializeField]
    protected string[] webImages = {"https://m.media-amazon.com/images/M/MV5BYzI1MDk0NTktNTk4NC00MDhiLWE4NmEtODI0ZDY0ZTQ0MjQ3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg",
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTvmIZSs6w6Ps6ivJYpHjeWMo8kF6-7qRO1mg&s"};

    protected List<int> randomNumbers = new List<int>();

    // https://ichef.bbci.co.uk/ace/standard/976/cpsprodpb/16620/production/_91408619_55df76d5-2245-41c1-8031-07a4da3f313f.jpg
    // https://www.google.com/url?sa=i&url=https%3A%2F%2Flamar.com%2Ffresno%2Fen%2Fproducts%2Fbillboards&psig=AOvVaw1TWQoAN9ortsL8gEIm8LQQ&ust=1732231360759000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCMDno-mG7IkDFQAAAAAdAAAAABAE
    private void Start()
    {
        for (int i = 0; i < webImages.Length; i++)
        {
            randomNumbers.Add(i);

        }
    }
    protected void OnImageDownloaded(Texture2D billboardImageTexture)
    {
        
        this.GetComponent<MeshRenderer>().material.mainTexture = billboardImageTexture;
    }

    protected string PickImage()
    {
        if (randomNumbers.Count == 0)
        {
            return null;
        }
        int index = Random.Range(0, randomNumbers.Count);
        int value = randomNumbers[index];

        randomNumbers.RemoveAt(index);

        return webImages[value];
        
    }
}