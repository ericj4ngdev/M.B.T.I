using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float radius = 0.1f;
    void Update()
    {
        // 현재 마우스의 스크린 좌표를 월드 좌표로 변환하여 targetPosition으로 설정
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0f; // 2D 게임이므로 z 축은 0으로 설정
     
        // 현재 위치에서 목표 위치로 부드럽게 이동
        transform.position = targetPosition;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    // }
}