using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotButton : MonoBehaviour
{
    [SerializeField]
    private Camera captureCamera;
    private void Start()
    {
        
    }
    private void MakeScrenshot() {
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
    }
}
