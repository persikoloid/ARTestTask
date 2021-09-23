using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    // Start is called before the first frame update
    public bool isUi;
    public void OnPointerExit(PointerEventData eventData)
    {
        isUi = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isUi = true;
    }
}
