using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader
{
   


    public IEnumerator DownloadImage(Action<Texture2D> callback, string billBoardImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(billBoardImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}