using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject flowerPrefab;
    public RectTransform canvasRectTransform;
    public Camera worldCamera;
    public Canvas flowerCanvas;
    public int spawnCount = 3; // 클릭마다 생성할 프리팹 개수

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 screenPos = Input.mousePosition;
            Vector2 canvasPos;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, worldCamera, out canvasPos))
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    GameObject flower = Instantiate(flowerPrefab, flowerCanvas.transform);
                    flower.GetComponent<RectTransform>().anchoredPosition = canvasPos;
                    flower.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    flower.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    flower.SetActive(true);
                }
            }
        }
    }
}