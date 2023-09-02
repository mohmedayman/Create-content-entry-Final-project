using UnityEngine;

public class ImageLoader : MonoBehaviour
{
    
    public Texture2D LoadImageFromFile(string imagePath)
    {
        Debug.Log("alo");
        if (!System.IO.File.Exists(imagePath))
        {
            Debug.LogError("Image file does not exist: " + imagePath);
            return null;
        }

        byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        if (!texture.LoadImage(imageData))
        {
            Debug.LogError("Failed to load image: " + imagePath);
            return null;
        }
        Debug.Log("hello");
        return texture;
    }
}