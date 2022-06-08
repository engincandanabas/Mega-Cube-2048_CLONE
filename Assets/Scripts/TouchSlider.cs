using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TouchSlider : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public UnityAction onPointerDownEvent;
    public UnityAction<float> onPointerDragEvent;
    public UnityAction onPointerUpEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDownEvent != null)
        {
            onPointerDownEvent.Invoke();
        }

        if (onPointerDragEvent != null)
        {
            onPointerDragEvent.Invoke(uiSlider.value);
        }
        
    }

    private void OnSliderValueChanged(float value)
    {
        if (onPointerDragEvent != null)
        {
            onPointerDragEvent.Invoke(value);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUpEvent != null)
        {
            onPointerUpEvent.Invoke();
        }
        //reset slider value
        uiSlider.value = 0f;
    }

    private void OnDestroy()
    {
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
