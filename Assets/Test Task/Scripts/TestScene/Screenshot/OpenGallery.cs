using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class OpenGallery : MonoBehaviour
{
    [SerializeField] private AssetReference gallery;
    [SerializeField] private Canvas parentCanvas;
    public void OpenGalleryClick()
    {
        Addressables.InstantiateAsync(gallery, new Vector2(0,0),Quaternion.identity, null, true);
    }
}
