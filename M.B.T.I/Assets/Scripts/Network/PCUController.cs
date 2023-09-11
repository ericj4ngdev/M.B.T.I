using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace PCU
{
    public class PCUController : MonoBehaviourPunCallbacks
    {
        public PCUCharactor pcuCharactor;         // 캐릭터 가져오기
        // private Renderer charactorRender;
        // public PhotonView pv;
        
        private void Awake()
        {
            // 포톤 네트워크에 타입을 등록
            PhotonPeer.RegisterType(typeof(ColorData), 0, ColorData.Serialize, ColorData.Deserialize);
            // charactorRender = pcuCharactor.GetComponent<Renderer>();
        }

        // 각 버튼에서 이 함수를 이벤트로 호출한다. 
        public void SetColorData(Material material)
        {
            ColorData custom = new ColorData();
            // 큐브의 색상을 변경합니다.
            // material 값은 0~1 사이 값(float)이다. 
            custom.r = (int)(material.color.r * 255);
            custom.g = (int)(material.color.g * 255);
            custom.b = (int)(material.color.b * 255);
            custom.a = (int)(material.color.a * 255);

            Debug.Log("material.color.r : " + material.color.r + "\n" + "custom.r: " + custom.r);
            Debug.Log("material.color.g : " + material.color.g + "\n" + "custom.g: " + custom.g);
            Debug.Log("material.color.b : " + material.color.b + "\n" + "custom.b: " + custom.b);
            Debug.Log("material.color.a : " + material.color.a + "\n" + "custom.a: " + custom.a);
            
            pcuCharactor.SetColor(custom);
        }

    }
}

