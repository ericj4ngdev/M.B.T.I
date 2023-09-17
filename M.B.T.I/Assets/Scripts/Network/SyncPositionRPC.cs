using UnityEngine;
using Photon.Pun;

public class SyncPositionRPC : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPosition;
    private float lerpSpeed = 10f;

    private void Update()
    {
        if (!photonView.IsMine)
        {
            // 다른 플레이어의 Cube일 경우 보간하여 위치를 동기화합니다.
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * lerpSpeed);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 자신의 Cube 위치를 스트림에 씁니다.
            stream.SendNext(transform.position);
        }
        else
        {
            // 다른 플레이어의 Cube 위치를 스트림에서 읽어옵니다.
            networkPosition = (Vector3)stream.ReceiveNext();
        }
    }
    // 위치를 RPC로 동기화합니다.
    [PunRPC]
    void SyncPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    // 위치 변경 시 호출
    void ChangePosition(Vector3 newPosition)
    {
        // 로컬 플레이어에게만 큐브를 이동할 수 있도록 합니다.
        if (photonView.IsMine)
        {
            photonView.RPC("SyncPosition", RpcTarget.All, newPosition);
        }
    }

}
