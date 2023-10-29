using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
열기구 애니메이션 스크립트
 */

public class AirballoonAnimation : AirBalloon
{
    public delegate void BalloonArriveEventHandler();
    public event BalloonArriveEventHandler OnBalloonArrive;

    [SerializeField] private List<GameObject> lerpPoints;     // 중간점
    [SerializeField] private float duration = 10;             // 점에서 점으로 이동할 때까지의 시간, duration을 증가시키면 속도 느려짐
    private int lerpPointsIndex = 0;                          // 중간점 인덱스

    /// <summary>
    /// 트랙을 도는 애니메이션,
    /// duration을 증가시키면 속도 느려짐
    /// </summary>
    /// <param name="duration"></param>

    void Start()
    {
        StartAnimation(duration);
    }

    public void StartAnimation(float duration)
    {
        StartCoroutine(MoveAlongTrack(duration));
    }

    IEnumerator MoveAlongTrack(float duration)
    {
        while (true)
        {
            if (previousStopIndex == wayPoints.Count)
                previousStopIndex = 0;

            if (nextStopIndex == wayPoints.Count)
                nextStopIndex = 0;

            if (lerpPointsIndex == lerpPoints.Count)
                lerpPointsIndex = 0;

            float time = 0f;


            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;  // 선형보간 가중치
                transform.position = BezierCurves(t, previousStopIndex, nextStopIndex, lerpPointsIndex);

                yield return null;
            }

            if (isPlayerWantToGetOff)
            {
                yield return new WaitForSeconds(2f);

                // 하차 이벤트
                if (OnBalloonArrive != null)
                {
                    OnBalloonArrive();
                }
            }

            Debug.Log("이번 정류장: " + GetPreviousStop().name);
            Debug.Log("다음 정류장: " + GetNextStop().name);

            previousStopIndex++;
            nextStopIndex++;
            lerpPointsIndex++;
        }
    }

    Vector3 BezierCurves(float t, int indexOfWayPoints1, int indexOfWayPoints2, int indexOfLerpPoints)
    {

        Vector3 p1 = Vector3.Lerp(wayPoints[indexOfWayPoints1].GetComponent<Transform>().position, lerpPoints[indexOfLerpPoints].GetComponent<Transform>().position, t);
        Vector3 p2 = Vector3.Lerp(lerpPoints[indexOfLerpPoints].GetComponent<Transform>().position, wayPoints[indexOfWayPoints2].GetComponent<Transform>().position, t);
        Vector3 p = Vector3.Lerp(p1, p2, t);

        return p;
    }
}
