using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirBalloon : MonoBehaviour
{
    public static Action getLocation;

    private BezierCurveAnimation bezierCurveAnimation;

    // private float currentSpeed = 0;
    [SerializeField]
    private float duration = 10;       // 점에서 점으로 이동할 때까지의 시간
    [SerializeField]
    private Transform currentLocation;
    [SerializeField]
    private Transform playerLocation;
    private bool isPlayerOn = false;

    public bool isPlayerWantToGetOff = false;

    private void Awake()
    {
        getLocation = () =>
        {
            GetTransform();
        };
    }

    void Start()
    {
        bezierCurveAnimation = GetComponent<BezierCurveAnimation>();
        currentLocation = GetComponent<Transform>();
        Move();
    }

    public void StopToGetOff()
    {
        if (isPlayerWantToGetOff)
            bezierCurveAnimation.wantToGetOff = true;
    }

    private void Move()
    {
        bezierCurveAnimation.StartAnimation(duration);
    }

    public GameObject GetPreviousStop()   // 이전 정류장
    {
        return bezierCurveAnimation.wayPoints[bezierCurveAnimation.previousStopIndex];
    }

    public GameObject GetNextStop()      // 다음 정류장
    {
        return bezierCurveAnimation.wayPoints[bezierCurveAnimation.nextStopIndex];
    }

    public Transform GetTransform()     // 열기구의 현재 위치
    {
        return playerLocation;
    }

}
