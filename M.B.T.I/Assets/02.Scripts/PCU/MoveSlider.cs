using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
public class MoveSlider : MonoBehaviour
{
    public bool invertValue = false;
    public float playRange;
    ConfigurableJoint joint;
    public Vector3 axisPos;
    public float value;
    public Vector3 limitAxis;
    public Transform slider;
    public Slider Smoothness_silder;
    public Vector3 preScale;
    public Transform controllerScale;
    
    void Start()
    {
        preScale = transform.localScale;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(SetParent);
        grabbable.selectExited.AddListener(SetOffParent);
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
        // 아무리 스케일 값을 넣어도 잡는 동안에는 커질수밖에 없다. 
    }
    public void SetOffParent(SelectExitEventArgs arg)
    {
        // preScale = controllerScale.localScale;
        // 부모의 스케일에 따라 스케일 값을 조정
        Vector3 newLocalScale = new Vector3(
            preScale.x / controllerScale.localScale.x,
            preScale.y / controllerScale.localScale.y,
            preScale.z / controllerScale.localScale.z
        );
        Debug.Log("controllerScale.localScale.x : " + controllerScale.localScale.x);
        // 아무리 스케일 값을 넣어도 잡는 동안에는 커질수밖에 없다. 
        // // 새로운 로컬 스케일을 설정
        transform.localScale = newLocalScale;
        // 부모 설정은 둘째치고 잡을 때마다 계속 커진다. 
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
