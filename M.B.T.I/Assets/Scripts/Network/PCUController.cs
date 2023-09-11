using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace MyNamespace
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
            custom.r = (byte)material.color.r;
            custom.g = (byte)material.color.g;
            custom.b = (byte)material.color.b;
            custom.a = (byte)material.color.a;

            Debug.Log("(byte)material.color.r : " + (byte)material.color.r + "\n" + "custom.r: " + custom.r);
            Debug.Log("(byte)material.color.g : " + (byte)material.color.g + "\n" + "custom.g: " + custom.g);
            Debug.Log("(byte)material.color.b : " + (byte)material.color.b + "\n" + "custom.b: " + custom.b);
            Debug.Log("(byte)material.color.a : " + (byte)material.color.a + "\n" + "custom.a: " + custom.a);
            
            // Debug.Log("custom.r: " + custom.r);
            // Debug.Log("custom.g: " + custom.g);
            // Debug.Log("custom.b: " + custom.b);
            // Debug.Log("custom.a: " + custom.a);
            pcuCharactor.SetColor(custom);
            // RPC를 사용하여 다른 클라이언트에게 큐브 색상 변경을 동기화합니다.
            // pv.RPC("SyncCubeColor", RpcTarget.AllBuffered, custom);
        }

        /*[PunRPC]
        private void SyncCubeColor(ColorData _custumType)
        {
            // 다른 클라이언트에서 호출되는 RPC로써 큐브의 색상을 동기화합니다.
            Color myColor =new Color(_custumType.r / 255f, _custumType.g / 255f, _custumType.b / 255f, 1.0f);
            charactorRender.material.color = myColor;
        }*/
    }
}

