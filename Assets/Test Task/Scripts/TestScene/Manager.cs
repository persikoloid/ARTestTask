using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Test_Task.Scripts.TestScene
{
    [RequireComponent(typeof(AudioSource))]
    public class Manager : MonoBehaviour
    {
        [FormerlySerializedAs("ARCamera")] [SerializeField] private Camera arCamera;
        [SerializeField]
        private GameObject planeMarker;
        private bool _isMarker;
        private bool _rotate;
        public bool _isUI;
        
        private ARRaycastManager _aRRaycastManager;
        private EventHandler _eventHandler;
        
        private Vector2 _touchPosition;
        private List<ARRaycastHit> _aRRaycastHitsForMoved;
        
        private GameObject _selectedObject;
        private bool _isSelected;

        [FormerlySerializedAs("objectForSpawnn")] public GameObject objectForSpawn;
        [SerializeField] private AudioClip clipForSpawn;
        private AudioSource _audioSource;

        private Quaternion _yRotation;
        
        
        //Для экранного помощника
        private bool _isScanned = false;
        private bool _isSpawned = false;
        private bool _isMoved = false;
        private bool _isRotated = false;
        private bool _isScaled = false;
        [SerializeField] private HelperManager _helperManager;

        private void Start()
        {
            _aRRaycastManager = GetComponent<ARRaycastManager>();
            _eventHandler = FindObjectOfType<EventHandler>();
            planeMarker.SetActive(false);
            _audioSource = GetComponent<AudioSource>();
        }

        public void RotateOrNot(bool isRotate)
        {
            _rotate = isRotate;
        }
        
        public void UIOrNot(bool isUI)
        {
            _isUI = isUI;
        }
        private void Update()
        {
            TrackMarker();
            SelectForTransform();
            if (_isMarker && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began
                && !_isSelected &&!_isUI)
                SpawnObject();
            
            if (_isSelected && Input.touchCount == 1 && !_rotate) MoveObject();
            if (_isSelected && Input.touchCount == 2) ScaleObject();
            if (_isSelected && Input.touchCount == 1 && _rotate) RotateObject();
        }
        
        //Появление и исчезновение маркера при обнаружении и утери нужной поверхности
        private void TrackMarker()
        {
            List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
            _aRRaycastManager.Raycast(new Vector2(Screen.width/2, Screen.height/2), aRRaycastHits, TrackableType.Planes);
            if (aRRaycastHits.Count > 0)
            {
                planeMarker.transform.position = aRRaycastHits[0].pose.position;
                planeMarker.SetActive(true);
                _isMarker = true;
                if (!_isScanned)
                {
                    _isScanned = true;
                    _helperManager.IsScanned(_isScanned); 
                }
            }
            else
            {
                planeMarker.SetActive(false);
                _isMarker = false;
            }
        }

        //Создание объекта
        private void SpawnObject()
        {
            if (objectForSpawn && !_isSpawned)
            {
                _isSpawned = true;
                _helperManager.IsSpawned(_isSpawned);
            }
            Instantiate(objectForSpawn, planeMarker.transform.position, objectForSpawn.transform.rotation);
            _audioSource.Stop();
            _audioSource.PlayOneShot(clipForSpawn);
        }
    
        //Передвижение объекта
        private void MoveObject()
        {
            var touch = Input.GetTouch(0);
            _touchPosition = touch.position;

            if (touch.phase == TouchPhase.Moved)
            {
                _aRRaycastHitsForMoved = new List<ARRaycastHit>();
                _aRRaycastManager.Raycast(_touchPosition, _aRRaycastHitsForMoved, TrackableType.Planes);
                _selectedObject.transform.position = _aRRaycastHitsForMoved[0].pose.position;
            }
            if (!_isMoved)
            {
                _isMoved = true;
                _helperManager.IsMoved(_isMoved);
            }
            if (touch.phase != TouchPhase.Ended) return;
            _selectedObject.tag = "Unselected";
            _selectedObject = null;
            _isSelected = false;
        }
        
        //Вращение объекта
        private void RotateObject()
        {
            var touch1 = Input.touches[0];
            if (touch1.phase == TouchPhase.Moved)
            {
                _yRotation = Quaternion.Euler(0f, -touch1.deltaPosition.x*0.1f, 0f);
                _selectedObject.transform.rotation = _yRotation * _selectedObject.transform.rotation;
            }
            if (!_isRotated)
            {
                _isRotated = true;
                _helperManager.IsRotated(_isRotated);
            }

            if (touch1.phase != TouchPhase.Ended) return;
            _selectedObject.tag = "Unselected";
            _selectedObject = null;
            _isSelected = false;
        }
        
        //Изменение размера объекта
        private void ScaleObject()
        {
            var touch1 = Input.touches[0];
            var touch2 = Input.touches[1];
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                var distance = Vector2.Distance(touch1.position, touch2.position);
                var prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                var delta = distance - prevDistance;
                var localScale = _selectedObject.transform.localScale;
                localScale = new Vector3(localScale.x + delta * 0.0001f,
                    localScale.y + delta * 0.0001f, localScale.z + delta * 0.0001f);
                _selectedObject.transform.localScale = localScale;
            }
            if (!_isScaled)
            {
                _isScaled = true;
                _helperManager.IsScaled(_isScaled);
            }
            if (touch1.phase != TouchPhase.Ended && touch2.phase != TouchPhase.Ended) return;
            _selectedObject.tag = "Unselected";
            _selectedObject = null;
            _isSelected = false;
        }
        
        //Проверка нажали ли мы на объект или просто по экрану
        private void SelectForTransform()
        {
            if (Input.touchCount == 0) return;
            Touch touch = Input.GetTouch(0);
            _touchPosition = touch.position;
            if (touch.phase != TouchPhase.Began) return;
            Ray ray = arCamera.ScreenPointToRay(_touchPosition);
            if (!Physics.Raycast(ray, out var hitObject)) return;
            if (!hitObject.collider.CompareTag("Unselected")) return;
            hitObject.collider.gameObject.tag = "Selected";
            _selectedObject = hitObject.collider.gameObject;
            _isSelected = true;
        }
    }
}
