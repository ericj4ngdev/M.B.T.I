using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class MainMenuManager : MonoBehaviour
{
    // 플레이어 앞에 UI띄우기 위한 변수 생성
    private Transform head;
    private XROrigin xrOrigin;

    public float spawnDistance = 0.5f;
    public GameObject menu;
    public InputActionProperty showButton;

    // 플레이어 처음엔 못움직이게 하기
    private void Start()
    {
        if (xrOrigin == null) xrOrigin = FindObjectOfType<XROrigin>();
        head = xrOrigin.Camera.transform;
        // SetMovement(false);
    }

    void SetComponentEnabled<T>(bool isEnabled) where T : MonoBehaviour
    {
        MonoBehaviour componentToDisable = xrOrigin.GetComponent<T>();
        componentToDisable.enabled = isEnabled;
    }

    // 시작 버튼 이벤트
    public void SetMovement(bool canMove)
    {
        //SetComponentEnabled<ActionBasedContinuousMoveProvider>(canMove);
        // 컴포넌트 비활성화하면 Null오류가 떠서 속도를 0으로 만들어버리려 한다. 
        var move = xrOrigin.GetComponent<ActionBasedContinuousMoveProvider>();
        move.moveSpeed = canMove ? 2 : 0;
        var turn = xrOrigin.GetComponent<ActionBasedContinuousTurnProvider>();
        turn.turnSpeed = canMove ? 60 : 0;
        SetComponentEnabled<TeleportationProvider>(canMove);
    }

    void Update()
    {
        // 키보드용
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("Menu");
        //     menu.SetActive(!menu.activeSelf);
        // }

        ShowMenuOnHead();
    }

    void ShowMenuOnHead()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            // 메뉴 활성화
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);

            // 플레이어 앞에 띄우기
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    public void TeleportToBuildingForward(Transform SpawnSpot)
    {
        xrOrigin.transform.position = SpawnSpot.position;
        xrOrigin.transform.rotation = SpawnSpot.rotation;
    }
}
