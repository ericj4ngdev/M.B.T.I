using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class MoveLever : MonoBehaviour
{
    public bool invertValue = false;
    public float playRange;
    ConfigurableJoint joint;
    public Vector3 axisPos;
    public float value;
    public Vector3 limitAxis;
    public Transform slider;
    public Slider Smoothness_silder;
    
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(SetParent);
        joint = GetComponent<ConfigurableJoint>();
        limitAxis = new Vector3(joint.xMotion == ConfigurableJointMotion.Locked ? 0 : 1,
            joint.yMotion == ConfigurableJointMotion.Locked ? 0 : 1,
            joint.zMotion == ConfigurableJointMotion.Locked ? 0 : 1);
        // 축 위치
        axisPos = Vector3.Scale(transform.localPosition, limitAxis);
    }

    public void SetParent(SelectEnterEventArgs arg)
    {
        transform.SetParent(slider);
    }
    
    public float GetValue() 
    {
        bool positive = true;
        float temp;
        // limitAxis를 곱함으로써 x값만 살린다. 
        var currPos = Vector3.Scale(transform.localPosition, limitAxis);
        
        if(axisPos.x < currPos.x || axisPos.y < currPos.y || axisPos.z < currPos.z)
            positive = false;

        if(invertValue)
            positive = !positive;

        // 진짜 값
        temp = Vector3.Distance(axisPos, currPos) / joint.linearLimit.limit;
        if(!positive) temp *= -1;
        value = -(((temp / 2 ) + 0.5f ) - 1 );
        
        if (Mathf.Abs(value) < playRange)
            value = 0;
        Smoothness_silder.value = value;
        return value;
        // return Mathf.Clamp(value, -1f, 1f);
    }
    
    public ConfigurableJoint GetJoint() => joint;
}
