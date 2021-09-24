using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject textMeshPro;
    private bool _isActive;
    [SerializeField] private Sprite _close, _info;
    [SerializeField] private Image _image;

    private Vector2 posBox;
    private float _height;
    private void Start()
    {
        _height = textMeshPro.GetComponent<RectTransform>().sizeDelta.y;
        posBox = textMeshPro.transform.localPosition;
        _isActive = false;
        GetComponent<Button>().onClick.AddListener(HideAndShowInfo);
    }

    private void HideAndShowInfo()
    {
        if (!_isActive)
        {
            textMeshPro.transform.localPosition = new Vector2(0, posBox.y + _height);
            textMeshPro.transform.LeanMoveLocalY(posBox.y, 0.5f).setEaseOutExpo().delay = 0.1f;
            _image.sprite = _close;
        }
        else _image.sprite = _info;
        textMeshPro.SetActive(!_isActive);
        _isActive = !_isActive;
    }
}
