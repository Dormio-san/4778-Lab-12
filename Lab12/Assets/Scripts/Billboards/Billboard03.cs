using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard03 : BillboardController
{
    private ImageDownloader imageDownloader;
    // Start is called before the first frame update
    void Start()
    {
        imageDownloader = new ImageDownloader();
        StartCoroutine(imageDownloader.DownloadImage(OnImageDownloaded, webImages[2]));

    }
}
