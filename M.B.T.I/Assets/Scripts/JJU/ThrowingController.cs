using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingController : MonoBehaviour
{
    public Rigidbody thrownObject;
    public float throwForceMultiplier = 3f;
    public float maxThrowSpeed = 10f;

    private Vector3 previousControllerPosition;

    void Start()
    {
        previousControllerPosition = transform.position;
    }

    void Update()
    {
        // 컨트롤러의 속도 계산
        Vector3 controllerVelocity = (transform.position - previousControllerPosition) / Time.deltaTime;
        previousControllerPosition = transform.position;

        // 컨트롤러의 속도에 따라 힘의 크기 결정
        float throwForce = Mathf.Clamp(controllerVelocity.magnitude * throwForceMultiplier, 0f, maxThrowSpeed);

        // 컨트롤러의 이동 방향에 따라 힘의 방향 결정
        Vector3 throwDirection = transform.forward;

        // 힘을 물체의 Rigidbody에 적용하여 던지는 동작 구현
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody thrownRigidbody = Instantiate(thrownObject, transform.position, Quaternion.identity);
            thrownRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}
