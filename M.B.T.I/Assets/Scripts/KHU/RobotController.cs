using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotController : MonoBehaviour
{
    public UnityEvent failEvent;
    public UnityEvent successEvent;
    private Rigidbody rb;
    [SerializeField]
    private Animator jumpAnim;
    private Vector3 respawnTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnTransform = GetComponent<Transform>().position;

    }

    // 로봇의 행동패턴
    public enum RobotBehaviour
    {
        Go,
        TurnRight,
        TurnLeft,
        Jump
    }

    private static float moveSpeed = 5f;
    private static float oneBlockDistance = 10.0f;
    private float duration = oneBlockDistance / moveSpeed;
    private Coroutine myCoroutine;
    private bool isSuccessed = false;

    public void PlayBehaviour(List<int> behaviourList)
    {
        myCoroutine = StartCoroutine(ExecuteBehavioursWithDelay(behaviourList));
    }

    private IEnumerator ExecuteBehavioursWithDelay(List<int> behaviourList)
    {
        foreach(int behaviour in behaviourList)
        {
            yield return new WaitForSeconds(duration);
            switch (behaviour)
            {
                case (int)RobotBehaviour.Go:
                    Debug.Log("case: Go");
                    jumpAnim.enabled = false;
                    StartCoroutine(MoveForward());
                    break;
                case (int)RobotBehaviour.TurnRight:
                    Debug.Log("case: TurnRight");
                    StartCoroutine(TurnRight());
                    break;
                case (int)RobotBehaviour.TurnLeft:
                    Debug.Log("case: TurnLeft");
                    StartCoroutine(TurnLeft());
                    break;
                case (int)RobotBehaviour.Jump:
                    Debug.Log("case: Jump");
                    jumpAnim.enabled = true;
                    if (jumpAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump") == false)
                    {
                        Debug.Log(" 점프 실헹");
                        jumpAnim.Play("Jump", -1, 0);
                    }
                    StartCoroutine(Jump());
                    break;
            }
        }

        Invoke("TestFail", duration);
    }

    private void TestFail()
    {
        if (!isSuccessed)
            Respawn();
    }

    private IEnumerator MoveForward()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            Vector3 newOffset = transform.position + transform.TransformDirection(Vector3.forward) * step;
            rb.MovePosition(newOffset);
            //transform.Translate(Vector3.forward * step);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    // 오른쪽으로 회전하는 코루틴
    private IEnumerator TurnRight()
    {
        Debug.Log("turn right start");
        float elapsedTime = 0.0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);

        while (elapsedTime < duration)
        {
            float step = 45 * Time.fixedDeltaTime;
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, step));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    // 왼쪽으로 회전하는 코루틴
    private IEnumerator TurnLeft()
    {
        float elapsedTime = 0.0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, -90, 0);

        while (elapsedTime < duration)
        {
            float step = 45 * Time.fixedDeltaTime;
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, step));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Jump()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float step = moveSpeed * 2 * Time.fixedDeltaTime;
            Vector3 newOffset = transform.position + transform.TransformDirection(Vector3.forward) * step;
            rb.MovePosition(newOffset);
            //transform.Translate(Vector3.forward * step);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Trophy")
        {
            Debug.Log("성공");
            isSuccessed = true;
            successEvent.Invoke();
        }
        else
        {
            // 코루틴 버그 수정 필요
            Debug.Log("실패");
            failEvent.Invoke();
        }
        Respawn();
    }

    private void Respawn()
    {
        Vector3 newPosition = respawnTransform;
        Vector3 newRotation = new Vector3(0, 180, 0);
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation);
    }

}