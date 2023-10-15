using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SliderTextChanger : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public MoveSlider sliderReader;
    void Update()
    {
        //text.text = sliderReader.GetValue().ToString();; //
        text.text = Math.Round(sliderReader.GetValue(), 2).ToString();
    }
}
