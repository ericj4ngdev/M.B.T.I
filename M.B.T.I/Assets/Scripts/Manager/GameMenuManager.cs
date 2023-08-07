using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    // 플레이어 앞에 UI띄우기 위한 변수 생성
    public Transform head;
    public float spawnDistance = 0.5f;
    private GameObject XRPlayer;

    public GameObject menu;
    public InputActionProperty showButton;

    private void Start()
    {
        XRPlayer = FindObjectOfType<XROrigin>().gameObject;
    }


    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            Debug.Log("Menu");
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

    public void TeleportToBuildingForward(GameObject SpawnSpot)
    {
        XRPlayer.transform.position = SpawnSpot.transform.position;
    }

}