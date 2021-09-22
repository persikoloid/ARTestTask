using UnityEngine;

namespace Test_Task.Scripts
{
    public class SnapScrolling : MonoBehaviour
    {
        [Range(1, 50)]
        [Header("Controllers")]
        public int panCount;
        [Range(0, 500)]
        public int panOffset;
        [Range(0f, 20f)]
        public float snapSpeed;
        [Range(0f, 10f)]
        public float scaleSpeed;
        [Range(0f, 5f)]
        public float scaleOffset;
        [Header("Other Objects")]
        public GameObject[] panPrefab;

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
            _instPans = new GameObject[panCount];
            _pansPos = new Vector2[panCount];
            _panScale = new Vector2[panCount];
            for(int i = 0; i < panCount; i++)
            {
                _instPans[i] = Instantiate(panPrefab[i], transform, false);
                if (i == 0) continue;
                _instPans[i].transform.localPosition = new Vector2(_instPans[i - 1].transform.localPosition.x + 
                                                                   panPrefab[i].GetComponent<RectTransform>().sizeDelta.x + panOffset, 
                    _instPans[i].transform.localPosition.y);
                _pansPos[i] = -_instPans[i].transform.localPosition;
            }
        }

        private void FixedUpdate()
        {
            float nearestPos = float.MaxValue;
            for (int i = 0; i< panCount; i++)
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
