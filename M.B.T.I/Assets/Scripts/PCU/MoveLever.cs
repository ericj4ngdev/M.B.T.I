using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLever : MonoBehaviour
{
    public bool invertValue = false;
    public float playRange;
    ConfigurableJoint joint;
    public Vector3 axisPos;
    public float value;
    public Vector3 limitAxis;
    
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        limitAxis = new Vector3(joint.xMotion == ConfigurableJointMotion.Locked ? 0 : 1,
            joint.yMotion == ConfigurableJointMotion.Locked ? 0 : 1,
            joint.zMotion == ConfigurableJointMotion.Locked ? 0 : 1);
        axisPos = Vector3.Scale(transform.localPosition, limitAxis);
    }
    public float GetValue() 
    {
        bool positive = true;
        var currPos = Vector3.Scale(transform.localPosition, limitAxis);
        if(axisPos.x < currPos.x || axisPos.y < currPos.y || axisPos.z < currPos.z)
            positive = false;

        if(invertValue)
            positive = !positive;

        // 진짜 값
        value = Vector3.Distance(axisPos, currPos)/joint.linearLimit.limit;

        if(!positive) value *= -1;

        if (Mathf.Abs(value) < playRange)
            value = 0;
        
        return value;
        // return Mathf.Clamp(value, -1f, 1f);
    }
    
    public ConfigurableJoint GetJoint() => joint;
}
