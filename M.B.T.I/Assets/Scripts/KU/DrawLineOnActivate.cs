using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class DrawLineOnActivate : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject drawingPoint;
    public InputActionProperty btnRTrigger;
    
    public LineRenderer drawingLr;
    public List<Vector3> points = new List<Vector3>();
    public bool isPressed;

    [SerializeField] private MeshRenderer drawingMeshRenderer;
    [SerializeField] private float width;
    private float minDistanceToErase = 0.1f; // 선을 지우기 위한 최소 거리
    private ColorMenuManager colorMenuManager;
    
    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        // grabbable.selectEntered.AddListener(EnableMenu);
        // grabbable.selectExited.AddListener(DisableMenu);
        grabbable.activated.AddListener(DrawLine);
        grabbable.deactivated.AddListener(EndLine);

        colorMenuManager = GetComponent<ColorMenuManager>();
        // 선의 속성을 바꾸기 위해 컴포넌트 접근
        drawingLr = linePrefab.GetComponent<LineRenderer>();
        drawingMeshRenderer = drawingPoint.GetComponent<MeshRenderer>();
    }

    // 메뉴가 활성화되어 있는 동안 계속 동기화가 진행.
    public void SetColor()
    {
        // Material lineMaterial = drawingLr.material;
        drawingLr.material.color = drawingMeshRenderer.material.color;
    }

    
    public void EnableMenu(SelectEnterEventArgs arg)
    {
        colorMenuManager.enabled = true;
        // 그리기 불가하게 하기? 
    }
    
    public void DisableMenu(SelectExitEventArgs arg)
    {
        colorMenuManager.menu.SetActive(false);
        colorMenuManager.enabled = false;
    }
    
    public void DrawLine(ActivateEventArgs arg)
    {
        // 검지로 Trigger(Activate)를 처음에 눌렀을 떄만  && 메뉴 비활성화시에만 그리기
        if (isPressed == false && colorMenuManager.menu.activeSelf == false)
        {
            GameObject go = Instantiate(linePrefab);
            // 소환한 linePrefab을 저장한 go는 LineRenderer 컴포넌트를 가진 Line이다. 
            // 이 Line 정보를 drawingLr에 저장한다. 
            drawingLr = go.GetComponent<LineRenderer>();
            // 색 정보를 갱신하고
            drawingLr.material.color = drawingMeshRenderer.material.color;
            
            points.Add(drawingPoint.transform.position);
            drawingLr.positionCount = 1;
            drawingLr.SetPosition(0, points[0]);
            isPressed = true;
        }
    }

    private void Update()
    {
        // 누르는 동안 그려진다. 
        if (isPressed && colorMenuManager.menu.activeSelf == false)
        {
            Vector3 pos = drawingPoint.transform.position;
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
