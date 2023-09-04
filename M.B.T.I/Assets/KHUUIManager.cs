using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHUUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainUI, tutorial, blockArray3x3, blockArray5x5, success, fail;

    // Start is called before the first frame update
    void Start()
    {
        mainUI.SetActive(true);
    }

    private void backToMainUI()
    {
        mainUI.SetActive(true);
        success.SetActive(false);
    }

    
    public void setTutoUI()
    {
        mainUI.SetActive(false);
        blockArray3x3.SetActive(true);
    }

    public void setMainGameUI()
    {
        mainUI.SetActive(false);
        blockArray5x5.SetActive(true);
    }

    public void setSuccessUI()
    {
        blockArray3x3.SetActive(false);
        blockArray5x5.SetActive(false);
        success.SetActive(true);
        Invoke("backToMainUI", 2f);
    }

    public void setFailUI()
    {
        blockArray3x3.SetActive(false);
        blockArray5x5.SetActive(false);
        fail.SetActive(true);
    }
}
