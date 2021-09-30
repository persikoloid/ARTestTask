using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HelperManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private AssetReference scanPlane;
    [SerializeField] private AssetReference modelPlane;
    [SerializeField] private AssetReference spawnPanel;
    [SerializeField] private AssetReference movedPanel;
    [SerializeField] private AssetReference rotatePanel;
    [SerializeField] private AssetReference scalePanel;

    private GameObject _forDestroy;
    // Start is called before the first frame update
    private void Start()
    {
        _forDestroy = Addressables.InstantiateAsync(scanPlane, canvas.transform, false, true).Result;
    }

    public void IsScanned(bool isScanned)
    {
        Addressables.ReleaseInstance(_forDestroy);
        _forDestroy = Addressables.InstantiateAsync(modelPlane, canvas.transform, false, true).Result;
    }
    public void IsModel(bool isModel)
    {
        Addressables.ReleaseInstance(_forDestroy);
        _forDestroy = Addressables.InstantiateAsync(spawnPanel, canvas.transform, false, true).Result;
    }
    public void IsSpawned(bool isSpawned)
    {
        Addressables.ReleaseInstance(_forDestroy);
        _forDestroy = Addressables.InstantiateAsync(movedPanel, canvas.transform, false, true).Result;
    }
    public void IsMoved(bool isMoved)
    {
        Addressables.ReleaseInstance(_forDestroy);
        _forDestroy = Addressables.InstantiateAsync(rotatePanel, canvas.transform, false, true).Result;
    }
    public void IsRotated(bool isRotated)
    {
        Addressables.ReleaseInstance(_forDestroy);
        _forDestroy = Addressables.InstantiateAsync(scalePanel, canvas.transform, false, true).Result;
    }
    public void IsScaled(bool isScaled)
    {
        Addressables.ReleaseInstance(_forDestroy);
    }
}
