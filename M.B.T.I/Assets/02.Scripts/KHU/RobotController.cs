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
    [SerializeField]
    private AudioSource moveSound, jumpSound;
    private Vector3 startTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTransform = this.transform.position;
    }

    // 로봇의 행동패턴
    public enum RobotBehaviour
    {
        Go,
        TurnRight,
        TurnLeft,
        Jump
    }

    private static float moveSpeed = 1f;
    private static float oneBlockDistance = 2.0f;
    private float duration = oneBlockDistance / moveSpeed;
    private Coroutine myCoroutine;
    private bool isSuccessed = false;

    public void PlayBehaviour(List<int> behaviourList)
    {
        myCoroutine = StartCoroutine(ExecuteBehavioursWithDelay(behaviourList));
    }

    private IEnumerator ExecuteBehavioursWithDelay(List<int> behaviourList)
    {
        Debug.Log("다 채움");
        foreach(int behaviour in behaviourList)
        {
            yield return new WaitForSeconds(duration);
            switch (behaviour)
            {
                case (int)RobotBehaviour.Go:
                    moveSound.Play();
                    jumpAnim.enabled = false;
                    StartCoroutine(MoveForward());
                    break;
                case (int)RobotBehaviour.TurnRight:
                    moveSound.Play();
                    StartCoroutine(TurnRight());
                    break;
                case (int)RobotBehaviour.TurnLeft:
                    moveSound.Play();
                    StartCoroutine(TurnLeft());
                    break;
                case (int)RobotBehaviour.Jump:
                    jumpSound.Play();
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

        yield return new WaitForSeconds(duration);

        if (!isSuccessed)
        {
            Debug.Log("에잉");
            isSuccessed = false;
            failEvent.Invoke();
        }
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
        Debug.Log("충돌");
        if (collision.gameObject.name == "Trophy")
        {
            RespawnRobot();
            Debug.Log("성공");
            isSuccessed = true;
            successEvent.Invoke();
        }
        else
        {
            Debug.Log("실패");
            isSuccessed = false;
            failEvent.Invoke();
        }
    }

    public void RespawnRobot()
    {
        Vector3 newPosition = startTransform;
        Vector3 newRotation = new Vector3(0, 180, 0);
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation);
        isSuccessed = false;
    }

}