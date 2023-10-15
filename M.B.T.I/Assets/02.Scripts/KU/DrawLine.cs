using System.Collections.Generic;
using Photon.Pun.Demo.SlotRacer.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    [Header("Key")]
    public InputActionProperty btnRTrigger;

    public Transform drawingPoint;
    
    public LineRenderer drawingLr;
    public LineRenderer eraseLr;
    // public EdgeCollider2D col;
    public List<Vector3> points = new List<Vector3>();
    
    private float minDistanceToErase = 0.1f; // 선을 지우기 위한 최소 거리

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (btnRTrigger.action.WasPressedThisFrame())
        {
            GameObject go = Instantiate(linePrefab);
            drawingLr = go.GetComponent<LineRenderer>();
            // col = go.GetComponent<EdgeCollider2D>();
            points.Add(drawingPoint.position);
            drawingLr.positionCount = 1;
            drawingLr.SetPosition(0, points[0]);
        }
        if (btnRTrigger.action.IsPressed())
        {
            Vector3 pos = drawingPoint.position;
            // 클릭을 하는 동안 position이 계속 갱신되는 것을 방지한다. 위치 차이가 0.1보다 커야 한다. 
            if (Vector3.Distance(points[points.Count - 1], pos) > 0.01f)
            {
                points.Add(pos);
                drawingLr.positionCount++;
                drawingLr.SetPosition(drawingLr.positionCount-1, pos);
                // col.points = points.ToArray();
            }
        }

        if (btnRTrigger.action.WasReleasedThisFrame())
        {
            points.Clear();
            drawingLr = null;
        }
    }
}
