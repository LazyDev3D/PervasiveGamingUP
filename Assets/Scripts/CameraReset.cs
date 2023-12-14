using UnityEngine;
using UnityEngine.UI;


public class CameraReset : MonoBehaviour
{
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    public float resetDuration = 1.0f; // Adjust this value for the reset transition duration

    private bool isResetting = false;
    private float resetTimer = 0.0f;

    void Start()
    {
        // Store the original position and rotation of the camera
        originalCameraPosition = transform.position;
        originalCameraRotation = transform.rotation;
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

        // Smoothly interpolate the position and rotation towards their original values
        transform.position = Vector3.Lerp(transform.position, originalCameraPosition, t);
        transform.rotation = Quaternion.Slerp(transform.rotation, originalCameraRotation, t);

        // Check if the reset transition is complete
        if (t >= 1.0f)
        {
            isResetting = false;
        }
    }

    // Method to be called to start the reset transition
    public void ResetCamera()
    {
        StartReset();
    }
}