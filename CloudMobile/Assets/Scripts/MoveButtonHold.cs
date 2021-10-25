using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] UnityEvent OnButtonPressed;
    [SerializeField] UnityEvent OnButtonReleased;
    [SerializeField] private bool buttonPressed;

    private Vector2 stop = Vector2.zero;

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