using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        print(limitAxis);
        print(transform.localPosition);
        print(transform.position);
        print(axisPos);
    }

    public void SetParent(SelectEnterEventArgs arg)
    {
        transform.SetParent(slider);
    }
    
    public float GetValue() 
    {
        bool positive = true;
        // limitAxis를 곱함으로써 x값만 살린다. 
        // 그런데 지금 localPosition값이 너무 작아서 0으로 찍한다. 
        var currPos = Vector3.Scale(transform.localPosition, limitAxis);
        print(Vector3.Distance(axisPos, currPos));
        if(axisPos.x < currPos.x || axisPos.y < currPos.y || axisPos.z < currPos.z)
            positive = false;

        if(invertValue)
            positive = !positive;

        
        // 진짜 값
        value = Vector3.Distance(axisPos, currPos) / joint.linearLimit.limit;

        if(!positive) value *= -1;
        
        if (Mathf.Abs(value) < playRange)
            value = 0;  
        
        return value;
        // return Mathf.Clamp(value, -1f, 1f);
    }
    
    public ConfigurableJoint GetJoint() => joint;
}
