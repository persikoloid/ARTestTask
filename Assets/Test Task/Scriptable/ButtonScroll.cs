using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "ButtonScroll", menuName = "TestScriptable/buttonScroll", order = 51)]
public class ButtonScroll : ScriptableObject
{
    [Header("Спрайты кнопок")]
    [SerializeField] 
    private List<Sprite> sprite;

    [Header("Префабы моделей")] 
    [SerializeField] 
    private List<GameObject> prefab;

    public List<Sprite> buttonSprite
    {
        get
        {
            return sprite;
        }
    }

    public List<GameObject> modelPrefab
    {
        get
        {
            return prefab;
        }
    }
}
