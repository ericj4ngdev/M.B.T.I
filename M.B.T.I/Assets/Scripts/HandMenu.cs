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
            // 이동 비활성화            
            Debug.Log("Menu");
            handMenuCanvas.SetActive(!handMenuCanvas.activeSelf);
        }
    }

    // x 누르면 지도 활성화 
}
