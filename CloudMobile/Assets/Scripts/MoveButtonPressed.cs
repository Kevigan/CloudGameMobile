using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] UnityEvent OnButtonPressed;
    [SerializeField] UnityEvent OnButtonReleased;
    [SerializeField] private bool buttonPressed;


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        OnButtonPressed.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        OnButtonReleased.Invoke();
    }
}