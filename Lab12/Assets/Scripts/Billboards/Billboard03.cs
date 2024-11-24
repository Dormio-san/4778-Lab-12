using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard03 : BillboardController
{
    private ImageDownloader imageDownloader;

    private void Start()
    {
        imageDownloader = new ImageDownloader();
        StartCoroutine(GetBillboardImage());
    }

    private IEnumerator GetBillboardImage()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(imageDownloader.GetWebImage(OnImageDownloaded, PickImage()));
    }
}