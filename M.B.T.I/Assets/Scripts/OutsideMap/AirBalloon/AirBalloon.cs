using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirBalloon : MonoBehaviour
{
    public bool isPlayerWantToGetOff = false;
    [SerializeField] protected List<GameObject> wayPoints;      // 정류장

    protected int previousStopIndex = 0;      // 시작점 인덱스
    protected int nextStopIndex = 1;      // 도착점 인덱스

    public void OnClickedGetOffBtn()
    {

    }

    // 열기구 UI에 사용할 함수
    public GameObject GetPreviousStop()   // 이전 정류장
    {
        return wayPoints[previousStopIndex];
    }

    public GameObject GetNextStop()      // 다음 정류장
    {
        return wayPoints[nextStopIndex];
    }
}
