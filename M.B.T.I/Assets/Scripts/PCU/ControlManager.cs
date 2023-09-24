using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using PCU;

public class ControlManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject target;
    public PCUCharactor pcuCharactor;
    public Animator animator;
    public int index;

    public SkinnedMeshRenderer renderer;
    public Material m_targetMaterial;
    public Color m_color;

    public PhotonView PV;

    public Color originalColor; // 원본 색상을 저장하는 변수
    public float originalMetallic; // 원본 Metallic 값을 저장하는 변수
    public List<RuntimeAnimatorController> animationControllers;

    private void Awake()
    {
        // 포톤 네트워크에 타입을 등록
        PhotonPeer.RegisterType(typeof(IndexData), 0, IndexData.Serialize, IndexData.Deserialize);
        PhotonPeer.RegisterType(typeof(ColorData), 0, ColorData.Serialize, ColorData.Deserialize);
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = target.GetComponent<PhotonView>();
        pcuCharactor = target.GetComponent<PCUCharactor>();
        animator = target.GetComponent<Animator>();
        renderer = target.GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            // Renderer의 Material을 가져와서 원본 Material로 설정합니다.
            m_targetMaterial = renderer.material;
            // 초기 Material을 복사하여 m_material로 설정합니다.
            originalColor = m_targetMaterial.color;
            originalMetallic = m_targetMaterial.GetFloat("_Metallic");
        }
    }
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

    // 색깔을 조절합니다.
    public void SetColor(Material material)
    {
        m_targetMaterial.color = material.color;
    }

    // 각 버튼에서 이 함수를 이벤트로 호출한다. 
    public void SetColorData(Material material)
    {
        ColorData custom = new ColorData();
        // 큐브의 색상을 변경합니다.
        // material 값은 0~1 사이 값(float)이다. 
        custom.r = (material.color.r * 255);
        custom.g = (material.color.g * 255);
        custom.b = (material.color.b * 255);
        custom.a = (material.color.a * 255);

        Debug.Log("material.color.r : " + material.color.r + "\n" + "custom.r: " + custom.r);
        Debug.Log("material.color.g : " + material.color.g + "\n" + "custom.g: " + custom.g);
        Debug.Log("material.color.b : " + material.color.b + "\n" + "custom.b: " + custom.b);
        Debug.Log("material.color.a : " + material.color.a + "\n" + "custom.a: " + custom.a);

        pcuCharactor.SetColor(custom);
    }

    // Metallic값을 조절합니다.
    public void SetMetallic()
    {
        // m_targetMaterial.SetFloat("_Metallic", Metallic_silder.value);
    }

    public void Revert()
    {
        if (m_targetMaterial != null && renderer != null)
        {
            m_targetMaterial.color = originalColor;
            m_targetMaterial.SetFloat("_Metallic", originalMetallic);
        }
    }

    public void SetAnimData(int idx)
    {
        
        target.transform.position = new Vector3(target.transform.position.x, 0.76f, target.transform.position.z);
        if (index >= 0)
        {
            index = idx;
            Debug.Log("click");
            PV.RequestOwnership();
            PV.RPC("ChangeVideoClip_RPC", RpcTarget.AllBuffered);
            // animator.runtimeAnimatorController = animationControllers[index];
            // animator.SetTrigger("PlayAnimation"); // "PlayAnimation"은 애니메이션을 실행시키는 트리거 이름입니다.
        }
        else
        {
            Debug.LogError("Invalid animation index");
        }
    }

    // 누구든지 누를수 있게 하기 위해
    [PunRPC]
    private void ChangeVideoClip_RPC()
    {
        if (target != null && index >= 0 && index < animationControllers.Count)
        {
            // VideoPlayer의 clip 속성을 변경하여 새로운 비디오 클립으로 설정
            animator.runtimeAnimatorController = animationControllers[index];
            Debug.Log("click _ RPC");
        }
    }
}
