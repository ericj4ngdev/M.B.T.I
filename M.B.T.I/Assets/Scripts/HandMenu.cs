using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandMenu : MonoBehaviour
{
    public GameObject handMenuCanvas;
    public InputActionProperty showButton;

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            // �̵� ��Ȱ��ȭ            
            Debug.Log("Menu");
            handMenuCanvas.SetActive(!handMenuCanvas.activeSelf);
        }
    }

    // x ������ ���� Ȱ��ȭ 
}
