using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard02 : BillboardController
{
    // Start is called before the first frame update
    private ImageDownloader imageDownloader;
    // Start is called before the first frame update
    void Start()
    {
        imageDownloader = new ImageDownloader();
        StartCoroutine(imageDownloader.DownloadImage(OnImageDownloaded, webImages[1]));

    }
}
