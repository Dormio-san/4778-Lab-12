using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard01 : BillboardController
{
    private ImageDownloader imageDownloader;

    private void Start()
    {
        imageDownloader = new ImageDownloader();
        StartCoroutine(imageDownloader.GetWebImage(OnImageDownloaded, PickImage()));
    }
}