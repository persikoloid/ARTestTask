using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollViewAnimated : MonoBehaviour
{
    [SerializeField] private Button button;
    [FormerlySerializedAs("_content")] [SerializeField]
    private GameObject content;
    [FormerlySerializedAs("_view")] [SerializeField]
    private GameObject view;
    [SerializeField] 
    private Transform canvas;

    private Vector2 _posView;
    private bool _isActive;
    private float _height;
    void Start()
    {
        _height = view.GetComponent<RectTransform>().sizeDelta.y;
        _posView = view.transform.localPosition;
        _isActive = true;
        button.onClick.AddListener(HideAndShowContent);
    }
    
    private void HideAndShowContent()
    {
        if (_isActive)
        {
            //view.transform.localPosition = new Vector2(0, -Screen.height);
            view.LeanMoveLocalY(-canvas.localPosition.y - _height, 0.5f).setEaseOutExpo();
            content.SetActive(!_isActive);
            _isActive = !_isActive;
        }
        else
        {
            view.LeanMoveLocalY(-canvas.localPosition.y, 0.5f).setEaseInExpo();
            content.SetActive(!_isActive);
            _isActive = !_isActive;
        }
        
    }
}
