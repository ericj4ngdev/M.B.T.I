using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloon : MonoBehaviour
{
    private BezierCurveAnimation bezierCurveAnimation;

    private float currentSpeed = 0;
    [SerializeField]
    private float duration = 10;       // 점에서 점으로 이동할 때까지의 시간
    private Transform currentLocation;
    private bool isPlayerOn = false;

    void Start()
    {
        bezierCurveAnimation = GetComponent<BezierCurveAnimation>();
        currentLocation = GetComponent<Transform>();
        Move();
    }

    public void StopToGetOff()
    {
        if (isPlayerOn)
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

    public GameObject GetNextStop()      // 이전 정류장
    {
        return bezierCurveAnimation.wayPoints[bezierCurveAnimation.nextStopIndex];
    }

    public Transform GetTransform()     // 열기구의 현재 위치
    {
        return currentLocation;
    }

}
