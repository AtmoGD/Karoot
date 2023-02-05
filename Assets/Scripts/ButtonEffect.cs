using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    bool isLoading = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isLoading)
        {
            transform.Rotate(0, 0, 3);
            Debug.Log("Click");
            isLoading = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isLoading)
        {
            transform.Rotate(0, 0, -3);
            Debug.Log("HOVERED");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isLoading)
        {
            transform.Rotate(0, 0, 3);
            Debug.Log("Not hovered");
        }
    }

}
