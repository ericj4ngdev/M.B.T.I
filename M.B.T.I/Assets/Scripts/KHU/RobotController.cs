using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotController : MonoBehaviour
{
    public UnityEvent failEvent;

    // 로봇의 행동패턴
    public enum RobotBehaviour
    {
        Go,
        TurnRight,
        TurnLeft,
        Jump
    }

    private static float moveSpeed = 5.0f;
    private static float oneBlockDistance = 10.0f;
    private float duration = oneBlockDistance / moveSpeed;


    public void PlayBehaviour(List<int> behaviourList)
    {
        StartCoroutine(ExecuteBehavioursWithDelay(behaviourList));
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
                    StartCoroutine(Jump());
                    break;
            }
        }
    }

    private IEnumerator MoveForward()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < (oneBlockDistance / moveSpeed))
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * step);
            elapsedTime += Time.deltaTime;
            yield return null;
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
            float t = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
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
            float t = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Jump()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float step = moveSpeed * 2 * Time.deltaTime;
            transform.Translate(Vector3.forward * step);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}