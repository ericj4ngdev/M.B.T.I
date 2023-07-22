using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
public class TabletManager : MonoBehaviour
{
    private GameObject XRPlayer;
    private XRBaseInteractable interactable;
    private Vector3 originalScale;
    private CapsuleCollider capsuleCollider;
    
    public GameObject pocket;
    public GameObject menu;

    public List<GameObject> panels = new List<GameObject>();
    // public GameObject mapPanel;
    // public GameObject airBalloonPanel;
    // public GameObject stampPanel;
    // public GameObject settingPanel;
    // public GameObject endPanel;
    // public GameObject btnPanel;
    
    private void Start()
    {
        XRPlayer = FindObjectOfType<XROrigin>().gameObject;
        capsuleCollider = GetComponent<CapsuleCollider>();
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        originalScale = transform.localScale;           // 원래 스케일 저장
        grabbable.selectEntered.AddListener(ShowUI);
        grabbable.selectExited.AddListener(HideUI);
    }
    public void ShowUI(SelectEnterEventArgs arg)
    {
        // isGrab = !isGrab;
        menu.SetActive(true);
        capsuleCollider.isTrigger = true;
        transform.SetParent(null);
        transform.localScale = originalScale; // 원래 스케일로 설정
    }
    public void HideUI(SelectExitEventArgs arg)
    {
        // isGrab = !isGrab;
        menu.SetActive(false);
        capsuleCollider.isTrigger = false;
        transform.position = pocket.transform.position;
        transform.rotation = pocket.transform.rotation;
        transform.SetParent(pocket.transform);
    }
    void Update()
    {
        
    }

    // 패널 모두 비활성화하는 함수 
    private void setoffPanels()
    {
        foreach (var VARIABLE in panels)
        {
            VARIABLE.SetActive(false);
        }
    }
    
    public void PressBtn(int idx)
    {
        setoffPanels();
        panels[idx].SetActive(true);
    }
    
    public void TeleportToBuildingForward(GameObject SpawnSpot)
    {
        XRPlayer.transform.position = SpawnSpot.transform.position;
    }
}
