using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public PanelMovement panelMovement; // Reference to the PanelMovement script

    private void Start()
    {
        // Ensure that the PanelMovement script is assigned
        if (panelMovement == null)
        {
            Debug.LogError("PanelMovement script not assigned to the ButtonInteraction script!");
        }
    }

    // This method will be called when the button is pressed
    public void OnButtonPress()
    {
        // Call the TogglePanelVisibility method in the PanelMovement script
        panelMovement.TogglePanelVisibility();
    }
}
