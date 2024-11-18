using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    private ImageDownloader imageDownloader;

    [SerializeField] private Material billboardImage;

    private void Start()
    {
        imageDownloader = new ImageDownloader();
        
        StartCoroutine(imageDownloader.DownloadImage(OnImageDownloaded));
    }

    private void OnImageDownloaded(Texture2D billboardImageTexture)
    {
        billboardImage.mainTexture = billboardImageTexture;
    }
}