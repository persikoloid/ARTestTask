using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenshotBlink : MonoBehaviour
{
    private Image _flashImage;
    private Coroutine _flashCoroutine;

    [SerializeField] private GameObject blinkObj;
    [SerializeField]
    private float flashDuration;
    void Start()
    {
        _flashImage = GetComponent<Image>();
        _flashCoroutine = StartCoroutine(FlashScreen());
    }

    IEnumerator FlashScreen()
    {
        for (float t = 0; t <= flashDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = _flashImage.color;
            colorThisFrame.a = Mathf.Lerp(.9f, 0, t / flashDuration);
            _flashImage.color = colorThisFrame;
            yield return null;
        }
        Destroy(blinkObj);
    }
}
