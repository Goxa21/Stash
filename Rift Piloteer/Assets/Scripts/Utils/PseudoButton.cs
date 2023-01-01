using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PseudoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent FingerDown;
    public UnityEvent FingerUp;


    public void OnPointerDown(PointerEventData eventData)
    {
        FingerDown.Invoke();
        Debug.Log("FingerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FingerUp.Invoke();
        Debug.Log("FingerUp");
    }

}
