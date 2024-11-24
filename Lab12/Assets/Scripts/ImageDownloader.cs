using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader
{
    private List<string> downloadedImageLinks = new List<string>();
    private List<Texture2D> downloadedImageTextures = new List<Texture2D>();

    public IEnumerator GetWebImage(Action<Texture2D> callback, string billBoardImage)
    {
        // Check if the downloaded image links contain the bill board image string the user inputted.
        if (downloadedImageLinks.Contains(billBoardImage))
        {
            // If it does, return the already downloaded image.
            callback(downloadedImageTextures[downloadedImageLinks.IndexOf(billBoardImage)]);
            yield break;
        }
        else
        {
            // Add the download image link to the list of downloaded image links.
            downloadedImageLinks.Add(billBoardImage);

            // Send out a request to download the image.
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(billBoardImage);
            yield return request.SendWebRequest();

            // Assign the downloaded image to a texture.
            Texture2D image = DownloadHandlerTexture.GetContent(request);

            // Add the downloaded image to the list of downloaded image textures.
            downloadedImageTextures.Add(image);

            // Return the downloaded image.
            callback(image);
        }
    }
}