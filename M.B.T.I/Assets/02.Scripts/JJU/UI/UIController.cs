using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject signBoardUI;
    public void onSelectedSignBoard()
    {
        signBoardUI.SetActive(true);
    }
    public void onSelectedCloseBtn()
    {
        signBoardUI.SetActive(false);
    }
}
