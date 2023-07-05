using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        
    }

    
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        // 현재 로컬 플레이어에게 소유권 주장
        photonView.RequestOwnership();
        base.OnSelectEntered(interactor);
    }
}
