using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace PCU
{
    public class PCUCharactor : MonoBehaviourPunCallbacks
    {
        public PhotonView pv;
        public Animator animator;
        public List<RuntimeAnimatorController> animationControllers;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(transform.position.x, 0.76f, transform.position.z);
            pv = GetComponent<PhotonView>();
            animator = GetComponent<Animator>();
        }

        public void SetAnim(int indexData)
        {
            // RPC를 사용하여 다른 클라이언트에게 큐브 색상 변경을 동기화합니다.
            pv.RequestOwnership();
            animator.SetInteger("Index", indexData);
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
            GetComponentInChildren<SkinnedMeshRenderer>().material.color = myColor;
            // GetComponent<Renderer>().material.color = myColor;
        }
    }
}