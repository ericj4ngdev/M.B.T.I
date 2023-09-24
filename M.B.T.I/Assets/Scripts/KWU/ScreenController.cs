using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Photon.Pun;
using Photon.Realtime;

public class ScreenController : MonoBehaviourPunCallbacks, IPunObservable
{
    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;
    private PhotonView PV;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // IPunObservable 인터페이스를 구현해야한다.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(index);
        }
        else
        {
            // Network player, receive data
            this.index = (int)stream.ReceiveNext();
        }
    }

    public void SetVideoClip(int idx)
    {
        index = idx;
        Debug.Log("click");
        PV.RequestOwnership();
        PV.RPC("ChangeVideoClip_RPC", RpcTarget.AllBuffered);                
    }

    // 누구든지 누를수 있게 하기 위해
    [PunRPC]
    private void ChangeVideoClip_RPC()
    {
        if (videoPlayer != null && index >= 0 && index < videoClips.Length)
        {
            // VideoPlayer의 clip 속성을 변경하여 새로운 비디오 클립으로 설정
            videoPlayer.clip = videoClips[index];
            Debug.Log("click _ RPC");
            videoPlayer.Play(); // 변경된 클립 재생
        }
    }
}
