using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI textMeshPro;
    [SerializeField]
    private Slider slider;
    private void Start()
    {
        LoadTestScene(1);
    }

    private void LoadTestScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            textMeshPro.text = progress * 100f + "%";
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
