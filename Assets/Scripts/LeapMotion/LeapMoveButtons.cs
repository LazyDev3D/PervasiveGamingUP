using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMoveButtons : MonoBehaviour
{
    public VoiceRecognition VoiceRecognition; // Reference to the PanelMovement script
    private void Start()
    {
        // Ensure that the PanelMovement script is assigned
        if (panelMovement == null)
        {
            Debug.LogError("PanelMovement script not assigned to the ButtonInteraction script!");
        }
    }

    // Update is called once per frame
    public void OnButtonPress()
    {

    }
}
