using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class DrawLineOnActivate : MonoBehaviour
{
    public GameObject linePrefab;
    public Transform drawingPoint;
    public InputActionProperty btnRTrigger;
    
    public LineRenderer drawingLr;
    public List<Vector3> points = new List<Vector3>();
    public bool isPressed;
    private float minDistanceToErase = 0.1f; // 선을 지우기 위한 최소 거리
    
    
    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(DrawLine);
        grabbable.deactivated.AddListener(EndLine);
        
        // 선의 속성을 바꾸기 위해 컴포넌트 접근
        // drawingLr = linePrefab.GetComponent<LineRenderer>();
    }

    public void SetColor()
    {
        
    }
    public void DrawLine(ActivateEventArgs arg)
    {
        // 처음에 눌렀을 떄만 
        if (isPressed == false)
        {
            GameObject go = Instantiate(linePrefab);
            drawingLr = go.GetComponent<LineRenderer>();
            points.Add(drawingPoint.position);
            drawingLr.positionCount = 1;
            drawingLr.SetPosition(0, points[0]);
            isPressed = true;
        }
    }

    
    
    private void Update()
    {
        // 누르는 동안 그려진다. 
        if (isPressed)
        {
            Vector3 pos = drawingPoint.position;
            // 클릭을 하는 동안 position이 계속 갱신되는 것을 방지한다. 위치 차이가 0.1보다 커야 한다. 
            if (Vector3.Distance(points[points.Count - 1], pos) > 0.01f)
            {
                points.Add(pos);
                drawingLr.positionCount++;
                drawingLr.SetPosition(drawingLr.positionCount - 1, pos);
            }
        }
    }

    public void EndLine(DeactivateEventArgs arg)
    {
        points.Clear();
        drawingLr = null;
        isPressed = false;
    }
}
