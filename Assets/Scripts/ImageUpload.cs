using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ImageUploader : MonoBehaviour
{
    public Image targetImage;
    public void UploadImage()
    {
        string imagePath = GetImagePath();
        if (!string.IsNullOrEmpty(imagePath))
        {
            Texture2D texture = LoadImageFromFile(imagePath);
            targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        }
    }

    private string GetImagePath()
    {
        string[] imageExtensions = { ".png", ".jpg", ".jpeg" };

        // Open a file dialog or device-specific image picker
#if UNITY_EDITOR
        string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg");
#elif UNITY_ANDROID
        AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment");
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.intent.action.GET_CONTENT");
        intent.Call<AndroidJavaObject>("setType", "image/*");
        AndroidJavaObject chooser = intent.CallStatic<AndroidJavaObject>("createChooser", intent, "Select Image");
        AndroidJavaObject uri = activity.Call<AndroidJavaObject>("startActivityForResult", chooser, 0);
        string imagePath = uri.Call<string>("getPath");
#elif UNITY_IOS
        string imagePath = NativeGallery.GetImageFromGallery((path) => { Debug.Log("Image path: " + path); }, "Select Image", "image/*");
#else
        string imagePath = null;
#endif

        if (!string.IsNullOrEmpty(imagePath) && Array.Exists(imageExtensions, ext => imagePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
        {
            return imagePath;
        }

        return null;
    }

    private Texture2D LoadImageFromFile(string path)
    {
        byte[] imageData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        return texture;
    }
}