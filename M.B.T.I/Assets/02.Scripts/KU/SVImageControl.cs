using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Image pickerImage;
    private RawImage SVImage;
    private ColorPickerControl CC;
    private RectTransform rectTransform;
    private RectTransform pickerTransform;
    public Camera camera;
    
    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColor(eventData);
        
    }
    
    private void Awake()
    {
        SVImage = GetComponent<RawImage>();
        CC = FindObjectOfType<ColorPickerControl>();
        rectTransform = GetComponent<RectTransform>();

        pickerTransform = pickerImage.GetComponent<RectTransform>();
        pickerTransform.position =
            new Vector2(-(rectTransform.sizeDelta.x * 0.5f), -(rectTransform.sizeDelta.y * 0.5f));
    }

    void UpdateColor(PointerEventData eventData)
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, camera,
            out localMousePosition);
        
        // Vector3 pos = rectTransform.InverseTransformPoint(new Vector3(eventData.position.x,eventData.position.y,0));
        Vector3 pos = localMousePosition;
        
        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;
        
        if (pos.x < -deltaX) pos.x = -deltaX;
        else if (pos.x > deltaX) pos.x = deltaX;
        if (pos.y < -deltaY) pos.y = -deltaY;
        else if (pos.y > deltaY) pos.y = deltaY;

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;
        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        pickerTransform.localPosition = pos;
        pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);
        
        CC.SetSV(xNorm,yNorm);
    }
}
