using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ChooseObject : MonoBehaviour
{
    private Button _button;
    [SerializeField]
    private GameObject objectForSpawn;
    
    private Manager _manager;

    // Start is called before the first frame update
    private void Start()
    {
        _manager = FindObjectOfType<Manager>();
        _button.onClick.AddListener(SetObject);
    }

    private void SetObject()
    {
        _manager.objectForSpawn = objectForSpawn;
    }
}
