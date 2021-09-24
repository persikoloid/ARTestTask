using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Gallery: MonoBehaviour {
	
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    public GameObject canvasForDestroy;
    private string[] _files = null;
    private int _whichScreenShotIsShown = 0;

    private void Start () {
        _files = Directory.GetFiles(Application.persistentDataPath + "/", "*.jpg");
        if (_files.Length > 0) {
            GetPictureAndShowIt();
        }
    }

    public void CloseGallery()
    {
        Destroy(canvasForDestroy);
    }
    private void GetPictureAndShowIt()
    {
        string pathToFile = _files[_whichScreenShotIsShown];
        Texture2D texture = GetScreenshotImage(pathToFile);
        Sprite sp = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height),
            new Vector2 (0.5f, 0.5f));
        panel.GetComponent<Image>().sprite = sp;
    }

    public void ShareScreenshot()
    {
        new NativeShare().AddFile(_files[_whichScreenShotIsShown]).Share();
    }

    private Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists (filePath)) {
            fileBytes = File.ReadAllBytes (filePath);
            texture = new Texture2D (2, 2, TextureFormat.RGB24, false);
            texture.LoadImage (fileBytes);
        }
        return texture;
    }

    public void NextPicture()
    {
        if (_files.Length > 0) {
            _whichScreenShotIsShown += 1;
            if (_whichScreenShotIsShown > _files.Length - 1)
                _whichScreenShotIsShown = 0;
            GetPictureAndShowIt ();
        }
    }

    public void PreviousPicture()
    {
        if (_files.Length > 0) {
            _whichScreenShotIsShown -= 1;
            if (_whichScreenShotIsShown < 0)
                _whichScreenShotIsShown = _files.Length - 1;
            GetPictureAndShowIt ();
        }
    }
}