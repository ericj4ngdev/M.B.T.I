using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class TabletManager : MonoBehaviour
{
    private GameObject XRPlayer;
    private XRBaseInteractable interactable;
    private Vector3 originalScale;
    private CapsuleCollider capsuleCollider;
    
    public GameObject pocket;
    public GameObject menu;
    
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

    public void TeleportToBuildingForward(GameObject SpawnSpot)
    {
        XRPlayer.transform.position = SpawnSpot.transform.position;
    }
}
