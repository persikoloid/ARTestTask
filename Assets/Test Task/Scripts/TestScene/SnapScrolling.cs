using UnityEngine;
using UnityEngine.UI;

namespace Test_Task.Scripts.TestScene
{
    public class SnapScrolling : MonoBehaviour
    {
        [SerializeField] private ButtonScroll buttonScrollData;
        
        private int panCount;
        
        [Range(0, 500)][SerializeField]
        private int panOffset;
        [Range(0f, 20f)]
        [SerializeField]
        private float snapSpeed;
        [Range(0f, 10f)]
        [SerializeField]
        private float scaleSpeed;
        [Range(0f, 5f)]
        [SerializeField]
        private float scaleOffset;

        [Header("Префаб кнопки")] [SerializeField]
        private GameObject panPrefab;

        private GameObject[] _instPans;
        private Vector2[] _pansPos;
        private Vector2[] _panScale;
    
        private Vector2 _contentVector;
        private RectTransform _contentRect;
    
        private int _panID;
        public bool isScrolling;
    
        private void Start()
        {
            _contentRect = GetComponent<RectTransform>();
            panCount = buttonScrollData.modelPrefab.Count;
            _instPans = new GameObject[panCount];
            _pansPos = new Vector2[panCount];
            _panScale = new Vector2[panCount];
            for(int i = 0; i < panCount; i++)
            {
                panPrefab.GetComponent<Image>().sprite = buttonScrollData.buttonSprite[i];
                panPrefab.GetComponent<ChooseObject>().setObject = buttonScrollData.modelPrefab[i];
                _instPans[i] = Instantiate(panPrefab, transform, false);
                if (i == 0) continue;
                _instPans[i].transform.localPosition = new Vector2(_instPans[i - 1].transform.localPosition.x + 
                                                                   panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, 
                    _instPans[i].transform.localPosition.y);
                _pansPos[i] = -_instPans[i].transform.localPosition;
            }
        }

        private void FixedUpdate()
        {
            float nearestPos = float.MaxValue;
            for (int i = 0; i < panCount; i++)
            {
                float distance = Mathf.Abs(_contentRect.anchoredPosition.x - _pansPos[i].x); 
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    _panID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / panOffset / 2) * scaleOffset, 0.5f, 1f);
                _panScale[i].x = Mathf.SmoothStep(_instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
                _panScale[i].y = Mathf.SmoothStep(_instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
                _instPans[i].transform.localScale = _panScale[i];

            }
            if (isScrolling) return;
            _contentVector.x = Mathf.SmoothStep(_contentRect.anchoredPosition.x, _pansPos[_panID].x, snapSpeed * Time.fixedDeltaTime);
            _contentRect.anchoredPosition = _contentVector;
        }

        public void Scrolling(bool scroll)
        {
            isScrolling = scroll;
        }
    }
}
