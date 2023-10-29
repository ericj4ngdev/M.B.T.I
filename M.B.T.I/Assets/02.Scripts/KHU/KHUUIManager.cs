using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHUUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainUI, tutorial, blockArray3x3, blockArray5x5, success, fail;
    private GameObject currentUI;

    // Start is called before the first frame update
    void Start()
    {
        mainUI.SetActive(true);
        currentUI = mainUI;
    }

    public void backToMainUI()
    {
        mainUI.SetActive(true);
        currentUI.SetActive(false);
        currentUI = mainUI;
    }

    public void explainUI()
    {
        currentUI.SetActive(false);
        tutorial.SetActive(true);
        currentUI = tutorial;
    }
    
    public void setTutoUI()
    {
        currentUI.SetActive(false);
        blockArray3x3.SetActive(true);
        currentUI = blockArray3x3;
    }

    public void setMainGameUI()
    {
        currentUI.SetActive(false);
        blockArray5x5.SetActive(true);
        currentUI = blockArray5x5;
    }

    public void setSuccessUI()
    {
        currentUI.SetActive(false);
        success.SetActive(true);
        currentUI = success;
    }

    public void setFailUI()
    {
        currentUI.SetActive(false);
        fail.SetActive(true);
        currentUI = fail;
    }
}
