﻿using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace PCU
{
    public class PCUController : MonoBehaviourPunCallbacks
    {
        public PCUCharactor pcuCharactor;         // 캐릭터 가져오기
        
        private void Awake()
        {
            // 포톤 네트워크에 타입을 등록
            PhotonPeer.RegisterType(typeof(ColorData), 0, ColorData.Serialize, ColorData.Deserialize);
        }

        // 각 버튼에서 이 함수를 이벤트로 호출한다. 
        public void SetColorData(Material material)
        {
            ColorData custom = new ColorData();
            
            // 캐릭터 색상 변경
            custom.r = (material.color.r * 255);        // material 값은 0~1 사이 값(float)이다. 
            custom.g = (material.color.g * 255);
            custom.b = (material.color.b * 255);
            custom.a = (material.color.a * 255);

            Debug.Log("material.color.r : " + material.color.r + "\n" + "custom.r: " + custom.r);
            Debug.Log("material.color.g : " + material.color.g + "\n" + "custom.g: " + custom.g);
            Debug.Log("material.color.b : " + material.color.b + "\n" + "custom.b: " + custom.b);
            Debug.Log("material.color.a : " + material.color.a + "\n" + "custom.a: " + custom.a);
            
            pcuCharactor.SetColor(custom);
        }

    }
}

