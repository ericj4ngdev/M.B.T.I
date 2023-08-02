using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SliderTextChanger : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public MoveLever sliderReader;
    void Update()
    {
        text.text = sliderReader.GetValue().ToString();; // Math.Round(sliderReader.GetValue(), 2).ToString();
    }
}
