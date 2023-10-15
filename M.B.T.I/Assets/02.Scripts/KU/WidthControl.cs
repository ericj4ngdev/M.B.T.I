using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class WidthControl : MonoBehaviour
{
    public float currentWidth;
    public LineRenderer lr;
    public Slider widthSlider;

    private float maxWidthValue;
    // Start is called before the first frame update
    void Start()
    {
        maxWidthValue = 0.05f;
    }
    
    public void SetWidth()
    {
        currentWidth = widthSlider.value  * maxWidthValue;
        lr.startWidth = currentWidth;
        lr.endWidth = currentWidth;
    }

    // Update is called once per frame
    void Update()
    {
        SetWidth();
    }
}
