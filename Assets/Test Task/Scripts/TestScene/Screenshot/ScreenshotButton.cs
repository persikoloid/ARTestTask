using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class ScreenshotButton : MonoBehaviour
{
    [SerializeField]
    private Camera captureCamera;
    [SerializeField]
    private GameObject screenEffect;

    private GameObject screenEffectForDestroy;
    public void ScreenShot()
    {
        StartCoroutine(MakeScreenshot());
    }
    IEnumerator MakeScreenshot() {
        var width = captureCamera.pixelWidth;
        var height = captureCamera.pixelHeight;
        var texture = new Texture2D(width, height);
        var targetTexture = RenderTexture.GetTemporary(width, height);
        captureCamera.targetTexture = targetTexture;
        captureCamera.Render();
        RenderTexture.active = targetTexture;
        var rect = new Rect(0, 0, width, height);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();
        captureCamera.targetTexture = null;
        var bytesScSh = texture.EncodeToJPG();
        var timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        var path = Application.persistentDataPath + "/"+ timeStamp + ".jpg";
        File.WriteAllBytes(path, bytesScSh);
        Debug.Log(path);
        yield return new WaitForEndOfFrame();
        screenEffectForDestroy = Instantiate (screenEffect, new Vector2(0f, 0f), Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.05f);
        Destroy(screenEffectForDestroy);
    }
}
