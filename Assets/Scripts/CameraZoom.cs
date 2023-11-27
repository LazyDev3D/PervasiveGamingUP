using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWithCursor : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minFOV = 10f;
    public float maxFOV = 60f;

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the camera's field of view based on the scroll wheel input
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - scrollWheel * zoomSpeed, minFOV, maxFOV);

        // Get the cursor position in screen space
        Vector3 cursorScreenPos = Input.mousePosition;

        // Convert the cursor position to a ray from the camera
        Ray ray = Camera.main.ScreenPointToRay(cursorScreenPos);
        RaycastHit hit;

        // Check if the ray hits anything in the scene
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the vector from the hit point to the camera
            Vector3 toHitPoint = hit.point - Camera.main.transform.position;

            // Adjust the camera's position based on the scroll wheel input and cursor position
            Camera.main.transform.Translate(toHitPoint * scrollWheel * zoomSpeed, Space.World);
        }
    }
}
