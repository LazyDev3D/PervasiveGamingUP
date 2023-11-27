using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float panSpeed = 0.1f;

    private bool isPanning = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        // Handle camera panning
        HandlePanning();
    }

    void HandlePanning()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isPanning = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            // Calculate the difference in mouse position
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;

            // Pan the camera based on the mouse movement
            PanCamera(deltaMouse);

            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }
    }

    void PanCamera(Vector3 deltaMouse)
    {
        // Convert the mouse movement to a pan direction in world space
        Vector3 panDirection = new Vector3(-deltaMouse.x, -deltaMouse.y, 0) * panSpeed;

        // Apply the pan direction to the camera's position
        transform.Translate(panDirection, Space.Self);
    }
}
