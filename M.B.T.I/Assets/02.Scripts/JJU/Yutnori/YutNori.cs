using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class YutNori : MonoBehaviour
{
    public Transform[] yuts; // 윷 GameObject들의 배열
    public Transform throwPoint; // 던질 위치
    public GameObject boxCollider;

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // 초기 윷들의 위치와 회전값 저장
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("놓음");
        isGrabbed = false;
    }


    public void ThrowYuts()
    {
        Debug.Log("윷놀이");
        // 윷들을 던질 위치로 이동시킴
        if (isGrabbed == false)
        {
            return;
        }
        else
        {
            boxCollider.SetActive(false);
            foreach (Transform yut in yuts)
            {
                yut.position = throwPoint.position;
                yut.rotation = Random.rotation; // 회전값을 랜덤하게 설정하여 던지는 모션 효과 추가
                Rigidbody rb = yut.GetComponent<Rigidbody>();
                rb.isKinematic = false; // 던질 때 물리 시뮬레이션 활성화
                rb.AddForce(Vector3.one , ForceMode.Impulse); // 던지는 힘을 랜덤하게 설정
            }
        }
    }
}