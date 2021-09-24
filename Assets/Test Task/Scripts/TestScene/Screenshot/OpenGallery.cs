using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGallery : MonoBehaviour
{
    [SerializeField] private GameObject gallery;

    public void OpenGalleryClick()
    {
        Instantiate(gallery, new Vector2(0f, 0f), Quaternion.identity);
    }
}
