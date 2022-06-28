using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class vButtons : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool value = false, value1 = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        value = true;
        value1 = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        value = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        value = false;
    }

}
