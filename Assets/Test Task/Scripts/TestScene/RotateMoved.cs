using System.Collections;
using System.Collections.Generic;
using Test_Task.Scripts.TestScene;
using UnityEngine;
using UnityEngine.UI;

public class RotateMoved : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _isRotate;
    [SerializeField]
    private Image _image;
    [SerializeField] private Manager _manager;
    [SerializeField] private Sprite rotate;
    [SerializeField] private Sprite moved;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(IsRotate);
        _isRotate = false;
    }

    private void IsRotate()
    {
        if (_isRotate)
        {
            _isRotate = !_isRotate;
            _image.sprite = moved;
            _manager.SendMessage("RotateOrNot", _isRotate);
        }
        else
        {
            _isRotate = !_isRotate;
            _image.sprite = rotate;
            _manager.SendMessage("RotateOrNot", _isRotate);
        }
    } 
}
