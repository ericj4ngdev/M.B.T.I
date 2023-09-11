using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipText : MonoBehaviour
{
    public TextMeshProUGUI tooltip;
    [TextArea]
    public string TextData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Text()
    {
        tooltip.text = TextData;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
