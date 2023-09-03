using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 도전과제 관리
// 현재 게임 상태 관리

public class KHUManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacle3X3;
    [SerializeField]
    private GameObject obstacle5X5;

    [SerializeField]
    private GameObject robot3X3;
    [SerializeField]
    private GameObject robot5X5;


    private bool isTutorial;

    public void SetTutorial()
    {
        isTutorial = true;
        obstacle3X3.SetActive(true);
        obstacle5X5.SetActive(false);
        robot3X3.SetActive(true);
        robot5X5.SetActive(false);
        // 도움말 UI 띄우기
    }

    public void setMainGame()
    {
        isTutorial = false;
        obstacle3X3.SetActive(false);
        obstacle5X5.SetActive(true);
        robot3X3.SetActive(false);
        robot5X5.SetActive(true);
        // 메인 UI 띄우기
    }

    public void Fail()
    {
        // fail UI 띄우기
        Debug.Log("다시시도해보세요.");

    }

    public void Success()
    {
        // success UI 띄우기
        // 트로피 애니메이션 추가
        Debug.Log("축하합니다.");
        if (!isTutorial)
            Debug.Log("도전과제 완료");
    }
}
