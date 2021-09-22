using UnityEngine;
using UnityEngine.UI;

namespace Test_Task.Scripts
{
    public class ChooseObject : MonoBehaviour
    {
        [Header("Объекты для спавна на экране")]
        [SerializeField]
        private GameObject objectForSpawn;
        private Manager _manager;

        private void Start()
        {
            _manager = FindObjectOfType<Manager>();
            GetComponent<Button>().onClick.AddListener(SetObject);
        }

        private void SetObject()
        {
            _manager.objectForSpawn = objectForSpawn;
        }
    }
}
