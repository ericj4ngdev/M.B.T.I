using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckCollider : MonoBehaviour
{
    public string targetTag = "Left Hand"; // 검사할 대상의 태그
    public bool isCheck = false; // 초기 값은 false로 설정

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 targetTag를 가진 오브젝트인지 확인
        if (collision.gameObject.CompareTag(targetTag))
        {
            isCheck = true; // 충돌한 오브젝트가 태그를 가지고 있으면 isCheck를 true로 설정
        }
    }
}
