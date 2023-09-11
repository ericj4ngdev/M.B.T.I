﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace PCU
{
    public class PCUCharactor : MonoBehaviourPunCallbacks
    {
        public PhotonView pv;
        // Start is called before the first frame update
        void Start()
        {
            pv = GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetColor(ColorData colorData)
        {
            // RPC를 사용하여 다른 클라이언트에게 큐브 색상 변경을 동기화합니다.
            pv.RPC("SyncCubeColor", RpcTarget.AllBuffered, colorData);
        }

        [PunRPC]
        void SyncCubeColor(ColorData _custumType)
        {
            // 다른 클라이언트에서 호출되는 RPC로써 큐브의 색상을 동기화합니다.
            Color myColor = new Color(_custumType.r / 255f, 
                                      _custumType.g / 255f, 
                                      _custumType.b / 255f, 
                                      _custumType.a / 255f);
            GetComponent<Renderer>().material.color = myColor;
        }
    }
}