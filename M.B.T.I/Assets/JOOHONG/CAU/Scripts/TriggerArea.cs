using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerArea : MonoBehaviour
{
    private XRBaseInteractable currentInteractable = null;
    private List<XRBaseInteractable> interactablesInsideTrigger = new List<XRBaseInteractable>();

    public bool isCheck = false;

    public GameObject particleCAU;
    public Transform particleCAU_list;
    //public float particleLifetime = 2.0f;

    private XRInteractionManager interactionManager; // XRInteractionManager를 참조하기 위한 변수 추가

    private void Start()
    {
        // XRInteractionManager 컴포넌트를 찾아 변수에 할당
        interactionManager = FindObjectOfType<XRInteractionManager>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        SetInteractiable(other);

        if (TryGetInteractable(other, out XRBaseInteractable interactable))
        {
            if (!interactablesInsideTrigger.Contains(interactable))
            {
                interactablesInsideTrigger.Add(interactable);
                Debug.Log("Object entered the trigger area: " + interactable.name);
                isCheck = true;
                CheckCollision(other.transform.position);

            }
        }

    }


    private void SetInteractiable(Collider other)
    {
        if (TryGetInteractable(other, out XRBaseInteractable interactable))
        {
            if (currentInteractable == null)
                currentInteractable = interactable;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        ClearInteractable(other);
    }

    private void ClearInteractable(Collider other)
    {
        if (TryGetInteractable(other, out XRBaseInteractable interactable))
        {
            if (currentInteractable == interactable)
                currentInteractable = null;

            if (interactablesInsideTrigger.Contains(interactable))
            {
                interactablesInsideTrigger.Remove(interactable);
                Debug.Log("Object exited the trigger area: " + interactable.name);
                isCheck = false;
                DestroyParticle();
            }
        }
    }
    
    private bool TryGetInteractable(Collider collider, out XRBaseInteractable interactable)
    {
        interactable = null;

        // interactionManager가 유효한 경우에만 호출
        if (interactionManager != null)
        {
            interactable = interactionManager.GetInteractableForCollider(collider);
        }

        return interactable != null;

    }
    /*

    public override void GetValidTargets(List<XRBaseInteractable> validTargets)
    {
        validTargets.Clear();
        validTargets.Add(currentInteractable);
    }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && currentInteractable == interactable && !interactable.isSelected;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return false;
    }
    */
    private void CheckCollision(Vector3 collisionPosition)
    {
        if (isCheck)
        {
            Debug.Log("확인");

            // 파티클 프리팹을 인스턴스화하여 생성할 위치로 설정
            GameObject particlePrefab = Instantiate(particleCAU, collisionPosition, Quaternion.identity);
            // 생성한 파티클을 원하는 부모 오브젝트에 추가
            particlePrefab.transform.SetParent(particleCAU_list);

            // 파티클을 사용한 후에 파괴하거나 더 이상 필요하지 않을 때 파괴
           // Destroy(particlePrefab, particleLifetime); 

        }
    }

    private void DestroyParticle()
    {
        // 파티클을 파괴
        foreach (Transform child in particleCAU_list)
        {
            Destroy(child.gameObject);
        }
    }

}
