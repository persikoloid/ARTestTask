using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isUi;
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    [SerializeField]
    private EventSystem m_EventSystem;


    void Start()
    {

        m_Raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = touch.position;
            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);
            foreach (var result in results)
            {
                if (result.gameObject.layer == 5)
                {
                    isUi = true;
                    SendMessage("UIOrNot",isUi);
                    break;
                }
                else
                {
                    isUi = false;
                    SendMessage("UIOrNot",isUi);
                }
            }
        }
    }
    
}
