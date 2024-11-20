using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardMa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        imageDownloader = new ImageDownloader();
        currBoard = billBoards[i];
        StartCoroutine(imageDownloader.DownloadImage(OnImageDownloaded, webImages[i]));
    }

    // Update is called once per frame
    void Update()
    {
        imageDownloader = new ImageDownloader();
    }
}
