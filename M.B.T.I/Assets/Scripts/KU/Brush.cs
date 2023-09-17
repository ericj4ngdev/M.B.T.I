using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class Brush : MonoBehaviour
{
    private GameObject XRPlayer;
    private XRBaseInteractable interactable;
    private Vector3 originalScale;
    private CapsuleCollider capsuleCollider;

    public GameObject pocket;
    public XRDirectInteractor rightHand;
    public XRDirectInteractor leftHand;

    private void Start()
    {
        XRPlayer = FindObjectOfType<XROrigin>().gameObject;
        capsuleCollider = GetComponent<CapsuleCollider>();
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

        originalScale = transform.localScale;           // 원래 스케일 저장

        grabbable.selectEntered.AddListener(GetBrush);
        grabbable.selectEntered.AddListener(setoffOppositeHand);
        grabbable.selectExited.AddListener(ReturnPocket);
    }
    public void GetBrush(SelectEnterEventArgs arg)
    {
        capsuleCollider.isTrigger = true;
        transform.SetParent(null);
        transform.localScale = originalScale; // 원래 스케일로 설정
    }
    public void setoffOppositeHand(SelectEnterEventArgs arg)
    {
        // 왼손이 잡으면 오른손의 XRDirect컴포넌트를 false로 한다. 
        if (arg.interactorObject.transform.CompareTag("Left Hand"))
        {
            // 오른손의 XRDirect컴포넌트를 false로 한다. 
            rightHand.enabled = false;
        }
        if (arg.interactorObject.transform.CompareTag("Right Hand"))
        {
            leftHand.enabled = false;
        }
    }

    public void ReturnPocket(SelectExitEventArgs arg)
    {
        capsuleCollider.isTrigger = false;
        transform.position = pocket.transform.position;
        transform.rotation = pocket.transform.rotation;
        transform.SetParent(pocket.transform);
        rightHand.enabled = true;
        leftHand.enabled = true;
    }
}
