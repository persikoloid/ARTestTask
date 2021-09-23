using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject textMeshPro;
    private bool _isActive;

    private Vector2 posBox;
    private void Start()
    {
        posBox = textMeshPro.transform.localPosition;
        _isActive = false;
        GetComponent<Button>().onClick.AddListener(HideAndShowInfo);
    }

    private void HideAndShowInfo()
    {
        if (_isActive == false)
        {
            textMeshPro.transform.localPosition = new Vector2(0, canvas.position.y + posBox.y / 2);
            textMeshPro.transform.LeanMoveLocalY(posBox.y, 0.5f).setEaseOutExpo().delay = 0.1f;
        }
        textMeshPro.SetActive(!_isActive);
        _isActive = !_isActive;
    }
}
