using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HelpLeap : MonoBehaviour
{

    public PanelMovement HelpMenu;
    
    public void OnButtonPress()
    {
        HelpMenu.TogglePanelVisibility();
    }
}
