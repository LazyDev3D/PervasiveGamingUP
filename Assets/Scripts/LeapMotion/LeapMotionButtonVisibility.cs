using UnityEngine;
using UnityEngine.UI;
using Leap;
using Leap.Unity;
using Leap.Unity.Interaction;

public class LeapMotionVisibilityToggle : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    public UIElement[] uiElementsToToggle;
    public LeapProvider leapProvider;  // Reference to the Leap Motion provider

    void Update()
    {
        // Check for visible hands
        bool handsVisible = AreHandsVisible();

        // Toggle the visibility of game objects based on hands visibility
        ToggleObjectVisibility(objectsToToggle, handsVisible);

        // Toggle the visibility of UI elements based on hands visibility
        ToggleUIVisibility(uiElementsToToggle, handsVisible);
    }

    bool AreHandsVisible()
    {
        Frame frame = leapProvider.CurrentFrame;

        // Check if there are any valid hands with finger data in the frame
        foreach (Hand hand in frame.Hands)
        {
            if (hand.Fingers.Count > 0)
            {
                return true; // At least one hand with valid finger data is present
            }
        }

        return false; // No hands with valid finger data found
    }

    void ToggleObjectVisibility(GameObject[] objects, bool handsVisible)
    {
        // Set the visibility of game objects based on hands visibility
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(handsVisible);
            }
        }
    }

    void ToggleUIVisibility(UIElement[] uiElements, bool handsVisible)
    {
        // Set the visibility of UI elements based on hands visibility
        foreach (UIElement uiElement in uiElements)
        {
            if (uiElement != null)
            {
                uiElement.gameObject.SetActive(!handsVisible);
            }
        }
    }
}

[System.Serializable]
public class UIElement
{
    public GameObject gameObject;
    // Add any other properties or references related to UI elements if needed
}
