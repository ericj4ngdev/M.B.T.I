using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveAnimation : MonoBehaviour
{
    public Transform target;
    public List<GameObject> wayPoints;      
    public List<GameObject> lerpPoints;     // 중간점
    [Range(0f, 10f)] [SerializeField] private float duration;       // 속도?
    //[Range(0f, 3f)] [SerializeField] private float value03;

    private int indexOfWayPoints1 = 0;      // 시작점 인덱스
    private int indexOfWayPoints2 = 1;      // 도착점 인덱스
    private int indexOfLerpPoints = 0;      // 중간점 인덱스

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAlongTrack());
    }

    IEnumerator MoveAlongTrack()
    {
        while (true)
        {
            if (indexOfWayPoints1 == wayPoints.Count)
                indexOfWayPoints1 = 0;

            if (indexOfWayPoints2 == wayPoints.Count)
                indexOfWayPoints2 = 0;

            if (indexOfLerpPoints == lerpPoints.Count)
                indexOfLerpPoints = 0;

            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                Debug.Log(time);
                float t = time / duration;  // 선형보간 가중치
                target.position = BezierCurves(t, indexOfWayPoints1, indexOfWayPoints2, indexOfLerpPoints);
                yield return null;
            }

            indexOfWayPoints1++;
            indexOfWayPoints2++;
            indexOfLerpPoints++;
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
