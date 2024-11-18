using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader
{
    // Image of the duck song.
    private const string webImage = "https://m.media-amazon.com/images/M/MV5BYzI1MDk0NTktNTk4NC00MDhiLWE4NmEtODI0ZDY0ZTQ0MjQ3XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg";

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}