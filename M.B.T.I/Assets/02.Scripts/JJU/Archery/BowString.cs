using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BowString : MonoBehaviour
{
    [SerializeField]
    private Transform endPoint1, endPoint2;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreateString(Vector3? midPoistion)
    {
        Vector3[] linePoints = new Vector3[midPoistion == null ? 2 : 3];
        linePoints[0] = endPoint1.localPosition;
        if (midPoistion != null)
        {
            // 로컬좌표계로 변환
            linePoints[1] = transform.InverseTransformPoint(midPoistion.Value);
        }
        linePoints[^1] = endPoint2.localPosition;
        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateString(null);
    }
}
