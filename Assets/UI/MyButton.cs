using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onPressDown;
    public UnityEvent onPressUp;
    public UnityEvent onPress;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private IEnumerator WhilePressed()
    {
        while(true)
        {
            onPress?.Invoke();
            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_button.interactable) return;
        
        StopAllCoroutines();
        StartCoroutine(WhilePressed());

        onPressDown?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        onPressUp?.Invoke();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        onPressUp?.Invoke();
    }
    // Afaik needed so Pointer exit works .. doing nothing further
    public void OnPointerEnter(PointerEventData eventData) { }
}
