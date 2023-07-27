using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToPlane : MonoBehaviour
{
    public Camera sourceCamera;
    public GameObject destinationPlane;

    void Start()
    {
        // 새로운 Render Texture 생성
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        sourceCamera.targetTexture = renderTexture;
        destinationPlane.GetComponent<Renderer>().material.mainTexture = renderTexture;
    }
}
