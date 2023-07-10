using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
열기구 애니메이션 스크립트
 */

public class BezierCurveAnimation : MonoBehaviour
{
    public Transform target;
    public List<GameObject> wayPoints;      
    public List<GameObject> lerpPoints;     // 중간점
    //[SerializeField] private float duration;       // 점에서 점으로 이동할 때까지의 시간

    public int previousStopIndex = 0;      // 시작점 인덱스
    public int nextStopIndex = 1;      // 도착점 인덱스
    private int lerpPointsIndex = 0;      // 중간점 인덱스

    public bool wantToGetOff = true;

    /// <summary>
    /// 트랙을 도는 애니메이션,
    /// duration을 증가시키면 속도 느려짐
    /// </summary>
    /// <param name="duration"></param>
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
                Debug.Log(time);
                float t = time / duration;  // 선형보간 가중치
                target.position = BezierCurves(t, previousStopIndex, nextStopIndex, lerpPointsIndex);

                yield return null;
            }

            if (wantToGetOff)
            {
                Debug.Log("Time to Get Off!!");
                yield return new WaitForSeconds(2f);
                wantToGetOff = false;
            }

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
