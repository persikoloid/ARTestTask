using UnityEngine;
using UnityEngine.UI;

namespace Test_Task.Scripts.TestScene
{
    public class ChooseObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectForSpawn;
        private Manager _manager;
        [SerializeField] private HelperManager _helperManager;
        private bool _isChoose = false;

        public GameObject setObject
        {
            set
            {
                objectForSpawn = value;
            }
        }

        private void Start()
        {
            _manager = FindObjectOfType<Manager>();
            GetComponent<Button>().onClick.AddListener(SetObject);
        }

        private void SetObject()
        {
            _manager.objectForSpawn = objectForSpawn;
            if (!_isChoose)
            {
                _isChoose = true;
                _helperManager.IsModel(_isChoose);
            }
        }
    }
}
