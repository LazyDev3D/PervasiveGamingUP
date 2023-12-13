using UnityEngine;

public class LeapMoveButtons : MonoBehaviour
{
    public VoiceRecognition VoiceRecognition; // Reference to the PanelMovement script
    public string objectName; // You can set this in the Inspector or directly in the script
    public Vector3 direction;

    public void OnButtonPress()
    {
        SetDirectionBasedOnObjectName(); // Call this method to set the direction based on the object name
        VoiceRecognition.StartContinuousRotation(direction);
        Debug.Log(objectName);

        if (objectName == "leapHelp")
        {
            direction = Vector3.right;
        }
    }

    public void OnButtonRelease()
    {
        VoiceRecognition.StopContinuousCommands();
    }

    void SetDirectionBasedOnObjectName()
    {
        Debug.Log("Object Name: " + objectName); // Add this line for debugging

        // Check if the object name contains specific strings
        if (objectName == "ButtonUp")
        {
            direction = Vector3.right;
        }
        else if (objectName == "ButtonDown")
        {
            direction = Vector3.left;
        }
        else if (objectName.Contains("ButtonLeft"))
        {
            direction = Vector3.down;
        }
        else if (objectName.Contains("ButtonRight"))
        {
            direction = Vector3.up;
        }
        else
        {
            // Default case or handle other scenarios
            direction = Vector3.right;
        }

        Debug.Log("Direction set to: " + direction);
    }
}

