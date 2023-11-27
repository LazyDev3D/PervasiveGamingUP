using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 10f;

    void Update()
    {
            ZoomCamera();
    }
    void ZoomCamera()
    {
        // Get input from the mouse scroll wheel
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the zoom distance based on the scroll wheel input
        float zoomDistance = Mathf.Clamp(transform.localPosition.z - scrollWheelInput * zoomSpeed, -maxZoomDistance, -minZoomDistance);

        // Zoom in/out along the forward axis
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zoomDistance);
    }
}
