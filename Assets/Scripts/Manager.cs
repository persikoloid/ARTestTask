using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject planeMarker;

    private ARRaycastManager _aRRaycastManager;

    private Vector2 _touchPosition;

    private SnapScrolling _snapScrolling;

    private GameObject _selectedObject;

    private List<ARRaycastHit> _aRRaycastHitsForMoved;

    public bool isSelected;

    [FormerlySerializedAs("objectForSpawnn")] public GameObject objectForSpawn;
    [FormerlySerializedAs("ARCamera")] [SerializeField] private Camera arCamera;

    private Quaternion _yRotation;

    private void Start()
    {
        _snapScrolling = FindObjectOfType<SnapScrolling>();
        _aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        planeMarker.SetActive(false);
    }


    private void Update()
    {
        var aRRaycastHits = new List<ARRaycastHit>();
        SelectForTransform();
        if (TrackMarker(aRRaycastHits) && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began
            && !_snapScrolling.isScrolling && !isSelected)
            SpawnObject(aRRaycastHits);
        //if (Input.anyKey) SpawnObject(aRRaycastHits);  

        if (isSelected && Input.touchCount == 1) MoveObject();
        if (isSelected && Input.touchCount == 2) ScaleObject();
        if (isSelected && Input.touchCount == 3) RotateObject();
    }
    //Появление и исчезновение маркера при обнаружении и утери нужной поверхности
    private bool TrackMarker(List<ARRaycastHit> aRRaycastHits)
    {
        
        _aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), aRRaycastHits, TrackableType.Planes);
        if (aRRaycastHits.Count != 0)
        {
            planeMarker.transform.position = aRRaycastHits[0].pose.position;
            planeMarker.SetActive(true);
            return true;
        }
        else planeMarker.SetActive(false);
        return false;
    }

    //Создание объекта
    private void SpawnObject(List<ARRaycastHit> aRRaycastHits)
    {
        Instantiate(objectForSpawn, aRRaycastHits[0].pose.position, objectForSpawn.transform.rotation);
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
            _selectedObject = GameObject.FindWithTag("Selected");
            _selectedObject.transform.position = _aRRaycastHitsForMoved[0].pose.position;
        }

        if (touch.phase != TouchPhase.Ended) return;
        if (!_selectedObject.CompareTag("Selected")) return;
        _selectedObject.tag = "Unselected";
        isSelected = false;
    }
    //Вращение объекта
    private void RotateObject()
    {
        var touch1 = Input.touches[0];
        var touch2 = Input.touches[1];
        var touch3 = Input.touches[2];
        if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved|| touch3.phase == TouchPhase.Moved)
        {
            if (touch1.position.y - touch1.deltaPosition.y < 0 || touch2.position.y - touch2.deltaPosition.y < 0
                                                               || touch3.position.y - touch3.deltaPosition.y < 0) 
                _yRotation = Quaternion.Euler(0f, -0.1f, 0f);
            else _yRotation = Quaternion.Euler(0f, 0.1f, 0f);
            _selectedObject.transform.rotation = _yRotation * _selectedObject.transform.rotation;
        }

        if (touch1.phase != TouchPhase.Ended && touch3.phase != TouchPhase.Ended &&
            touch2.phase != TouchPhase.Ended) return;
        if (!_selectedObject.CompareTag("Selected")) return;
        _selectedObject.tag = "Unselected";
        isSelected = false;
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
            localScale = new Vector3(localScale.x + delta,
                localScale.y + delta, localScale.z + delta);
            _selectedObject.transform.localScale = localScale;
        }

        if (touch1.phase != TouchPhase.Ended && touch2.phase != TouchPhase.Ended) return;
        if (!_selectedObject.CompareTag("Selected")) return;
        _selectedObject.tag = "Unselected";
        isSelected = false;
    }
    //Проверка нажали ли мы на объект или просто по экрану
    private void SelectForTransform()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            _touchPosition = touch.position;
            if (touch.phase != TouchPhase.Began) return;
            var ray = arCamera.ScreenPointToRay(_touchPosition);
            if (!Physics.Raycast(ray, out var hitObject)) return;
            if (!hitObject.collider.CompareTag("Unselected")) return;
            hitObject.collider.gameObject.tag = "Selected";
            isSelected = true;
        }
    }
}
