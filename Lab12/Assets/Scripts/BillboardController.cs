using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    // Image of the duck song.
    protected  string[] webImages = {"https://m.media-amazon.com/images/M/MV5BYzI1MDk0NTktNTk4NC00MDhiLWE4NmEtODI0ZDY0ZTQ0MjQ3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
        "https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/Cat_August_2010-4.jpg/2560px-Cat_August_2010-4.jpg",
        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTvmIZSs6w6Ps6ivJYpHjeWMo8kF6-7qRO1mg&s"};

    private ImageDownloader imageDownloader;
    [SerializeField]private GameObject[] billBoards;
    private GameObject currBoard;

    private Material billboardImage;

    private void Start()
    {
        
    }

    private void OnImageDownloaded(Texture2D billboardImageTexture)
    {
        currBoard.GetComponent<MeshRenderer>().material.mainTexture = billboardImageTexture;
    }
}