using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingArrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider myColider;

    [SerializeField]
    private Material[] mats = new Material[3];
    [SerializeField]
    Renderer excludedRenderer;

    public TrailRenderer trailRenderer;

    [SerializeField]
    private bool isStuck = false;

    [SerializeField]
    private GameObject target;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        if (!isStuck)
        {
            // 화살이 움직일 때마다 Trail Renderer 업데이트
            trailRenderer.emitting = true;
        }
        else
        {
            // 화살이 멈추면 Trail Renderer를 중지
            trailRenderer.emitting = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !isStuck)
        {
            Collider collider = collision.collider;

            StickToTarget(collision.contacts[0].point);

            Vector3 contactPoint = collision.contacts[0].point;

            // 충돌 지점 주변에 가장 가까운 서브메쉬를 찾기
            GameObject target1 = collision.gameObject;
            Renderer[] subMeshRenderers = target1.GetComponentsInChildren<Renderer>();
            Renderer[] closestRenderer = GetClosestRenderer(subMeshRenderers, contactPoint, 2);

            if (closestRenderer != null)
            {
                Debug.Log("체크 전");
                // 가장 가까운 서브메쉬의 Material 색상 가져오기
                Material hitMaterial = closestRenderer[1].material;
                Debug.Log(hitMaterial.name);
                CheckMaterial(hitMaterial);

            }
        }
    }

    private void CheckMaterial(Material material)
    {
        Debug.Log("체크");
        if (material == mats[0])
            Debug.Log("mat1");
        else if (material == mats[1])
            Debug.Log("mat2");
        else if (material == mats[2])
            Debug.Log("mat3");
        else
            Debug.Log("못찾음");
    }

    private Renderer[] GetClosestRenderer(Renderer[] renderers, Vector3 point, int count)
    {
        // 배열을 초기화합니다.
        Renderer[] closestRenderers = new Renderer[count];
        float[] closestDistances = new float[count];
        for (int i = 0; i < count; i++)
        {
            closestRenderers[i] = null;
            closestDistances[i] = float.MaxValue;
        }

        foreach (Renderer renderer in renderers)
        {
            float distance = Vector3.Distance(renderer.transform.position, point);

            for (int i = 0; i < count; i++)
            {
                if (distance < closestDistances[i])
                {
                    for (int j = count - 1; j > i; j--)
                    {
                        closestRenderers[j] = closestRenderers[j - 1];
                        closestDistances[j] = closestDistances[j - 1];
                    }
                    closestRenderers[i] = renderer;
                    closestDistances[i] = distance;
                    break;
                }
            }
        }

        return closestRenderers;
    }

    private void StickToTarget(Vector3 contactPoint)
    {
        isStuck = true;

        // 화살을 움직이지 않게 하기 위해 중력 비활성화
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 화살을 과녁에 고정
        transform.SetParent(target.transform);
        transform.position = contactPoint;
        
    }

}
