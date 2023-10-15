using System.Collections.Generic;
using Photon.Pun.Demo.SlotRacer.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawWithMouse : MonoBehaviour
{
    public GameObject linePrefab;

    public LineRenderer drawingLr;
    public LineRenderer eraseLr;
    public EdgeCollider2D col;
    public EdgeCollider2D eraseCol;
    public List<Vector2> points = new List<Vector2>();
    
    private float minDistanceToErase = 0.1f; // 선을 지우기 위한 최소 거리

    // Update is called once per frame
    void Update()
    {
        Draw();

        // 지우기 기능 호출
        Erase();
    }

    private void Draw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(linePrefab);
            drawingLr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            drawingLr.positionCount = 1;
            drawingLr.SetPosition(0, points[0]);
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 클릭을 하는 동안 position이 계속 갱신되는 것을 방지한다. 위치 차이가 0.1보다 커야 한다. 
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                points.Add(pos);
                drawingLr.positionCount++;
                drawingLr.SetPosition(drawingLr.positionCount-1, pos);
                col.points = points.ToArray();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
            drawingLr = null;
        }
    }
    private void Erase()
    {
        if (eraseLr != null)
        {
            // 우클릭시
            if (Input.GetMouseButtonDown(1))
            {
                print("GetMouseButtonDown");
                
                
                points.Clear();     // 한번 비우고
                // collider에 닿은 선의 point들을 points에 담음
                for (int i = 0; i < eraseLr.positionCount - 1; i++)
                {
                    points.Add(eraseLr.GetPosition(i));
                }
            }
            if (Input.GetMouseButton(1))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                int closestPointIndex = -1;
                // 순회하여 0.1보다 작은 점 지우기 
                for (int i = 0; i < points.Count; i++)
                {
                    float distance = Vector2.Distance(mousePos, points[i]);
                    if (distance < 0.1f)
                    {
                        closestPointIndex = i;
                        points.RemoveAt(closestPointIndex);
                        // eraseLr.positionCount--;
                        // eraseLr.SetPosition(closestPointIndex, mousePos);
                        
                        // if (closestPointIndex < eraseLr.positionCount)
                        // {
                        //     eraseLr.positionCount = closestPointIndex + 1;
                        // } 
                        
                        eraseCol.points = points.ToArray();
                    }
                }
                if (eraseLr.positionCount == 0)
                {
                    Destroy(eraseLr);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                points.Clear();
                eraseLr = null;
            }
        }
        
    }

    
    // 동기화시켜주는 함수 없나? 
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        LineRenderer lr = other.GetComponent<LineRenderer>();
        if (lr != null)
        {
            eraseLr = lr;
            eraseCol = eraseLr.GetComponent<EdgeCollider2D>();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        print("OnTriggerStay2D");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        eraseLr = null;
        eraseCol = null;
    }
    
}
