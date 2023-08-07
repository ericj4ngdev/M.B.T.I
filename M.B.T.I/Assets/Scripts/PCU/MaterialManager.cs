using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    public GameObject target;
    public Slider Smoothness_silder;
    public Slider Metallic_silder;
    public Image accessory_image;
    
    public List<Sprite> accessories_image = new List<Sprite>();
    
    [SerializeField]
    private int idx;
    [SerializeField]
    private List<GameObject> accessories = new List<GameObject>();
    private Renderer renderer;
    private Material m_targetMaterial;
    private Color m_color;
    
    private Color originalColor; // 원본 색상을 저장하는 변수
    private float originalSmoothness; // 원본 Smoothness 값을 저장하는 변수
    private float originalMetallic; // 원본 Metallic 값을 저장하는 변수
    
    private void Start()
    {
        renderer = target.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Renderer의 Material을 가져와서 원본 Material로 설정합니다.
            m_targetMaterial = renderer.material;
            // 초기 Material을 복사하여 m_material로 설정합니다.
            originalColor = m_targetMaterial.color;
            originalSmoothness = m_targetMaterial.GetFloat("_Smoothness");
            originalMetallic = m_targetMaterial.GetFloat("_Metallic");
        }

        // target 오브젝트의 자식 오브젝트들을 리스트에 추가합니다.
        foreach (Transform child in target.transform)
        {
            accessories.Add(child.gameObject);
        }

        accessory_image.sprite = accessories_image[0];
        idx = 0;
    }

    // 색깔을 조절합니다.
    public void SetColor(Material material)
    {
        m_targetMaterial.color = material.color;
    }
    
    // Smoothness 값을 조절합니다.
    public void SetSmoothness()
    {
        m_targetMaterial.SetFloat("_Smoothness", Smoothness_silder.value);
    }
    
    // Metallic값을 조절합니다.
    public void SetMetallic()
    {
        m_targetMaterial.SetFloat("_Metallic", Metallic_silder.value);
    }

    public void Revert()
    {
        if (m_targetMaterial != null && renderer != null)
        {
            m_targetMaterial.color = originalColor;
            Smoothness_silder.value = originalSmoothness;
            Metallic_silder.value = originalMetallic;
        }
        // 악세사리 초기화
        foreach (GameObject accessory in accessories)
        {
            accessory.SetActive(false);
        }
        idx = 0;
        accessory_image.sprite = accessories_image[idx]; 
    }

    public void SetAccessories(bool isRight)
    {
        foreach (GameObject accessory in accessories)
        {
            accessory.SetActive(false);
        }

        int nextIndex = idx + (isRight ? 1 : -1);
        if (nextIndex < 0)
        {
            nextIndex = accessories.Count - 1;
        }
        else if(nextIndex >= accessories.Count)
        {
            nextIndex = 0;
        }
        
        accessories[nextIndex].SetActive(true);
        accessory_image.sprite = accessories_image[nextIndex]; 
        idx = nextIndex;
    }
    
}
