using UnityEngine;

public class CameraResetScript : MonoBehaviour
{
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private float originalZoomDistance;

    public float resetDuration = 1.0f; // Adjust this value for the reset transition duration

    private bool isResetting = false;
    private float resetTimer = 0.0f;

    void Start()
    {
        // Store the original position, rotation, and zoom distance of the camera
        originalCameraPosition = transform.position;
        originalCameraRotation = transform.rotation;
        originalZoomDistance = transform.localPosition.z;
    }

    void Update()
    {
        // Check for the R key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Start the reset transition
            StartReset();
        }

        // Update the reset transition if it's in progress
        if (isResetting)
        {
            UpdateReset();
        }
    }

    // Make the function public to make it accessible from the Unity Editor
    public void StartReset()
    {
        isResetting = true;
        resetTimer = 0.0f;
    }

    void UpdateReset()
    {
        // Increment the timer
        resetTimer += Time.deltaTime;

        // Calculate the interpolation factor based on the timer and reset duration
        float t = Mathf.Clamp01(resetTimer / resetDuration);

        // Smoothly interpolate the position, rotation, and zoom towards their original values
        transform.position = Vector3.Lerp(transform.position, originalCameraPosition, t);
        transform.rotation = Quaternion.Slerp(transform.rotation, originalCameraRotation, t);

        float newZoomDistance = Mathf.Lerp(transform.localPosition.z, originalZoomDistance, t);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZoomDistance);

        // Check if the reset transition is complete
        if (t >= 1.0f)
        {
            isResetting = false;
        }
    }
}