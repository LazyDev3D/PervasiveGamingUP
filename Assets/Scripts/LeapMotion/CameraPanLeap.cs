using UnityEngine;

public class CameraPanLeap : MonoBehaviour
{
    public float panSpeed = 0.1f;

    private bool isPanning = false;
    private Vector3 lastSliderPosition;

    void Update()
    {
        // Handle camera panning
        HandlePanning();
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button for interacting with the slider
        {
            isPanning = true;
            lastSliderPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            // Calculate the difference in slider position
            Vector3 deltaSlider = Input.mousePosition - lastSliderPosition;

            // Pan the camera based on the slider movement
            PanCamera(deltaSlider);

            // Update the last slider position
            lastSliderPosition = Input.mousePosition;
        }
    }

    void PanCamera(Vector3 deltaSlider)
    {
        // Convert the slider movement to a pan direction in world space
        Vector3 panDirection = new Vector3(deltaSlider.x, 0, deltaSlider.y) * panSpeed;

        // Apply the pan direction to the camera's position
        transform.Translate(panDirection, Space.Self);
    }
}
