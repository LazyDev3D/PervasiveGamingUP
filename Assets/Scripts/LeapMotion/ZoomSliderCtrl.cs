using UnityEngine;
using Leap.Unity.Interaction;

public class ZoomSliderController : MonoBehaviour
{
    public InteractionSlider slider;
    public float minZoom = 1f;
    public float maxZoom = 5f;
    public float zoomSpeed = 5f;
    public Transform cameraPivot; // Reference to the CameraPivot

    void Start()
    {
        // Subscribe to the slider's OnValueChanged event
        slider.HorizontalSlideEvent += OnSliderValueChanged;

        // Set the initial Z position of the camera based on the minimum zoom level
        float initialZoomLevel = Mathf.Lerp(minZoom, maxZoom, slider.HorizontalSliderPercent);

        // Calculate the new position relative to the CameraPivot
        Vector3 initialLocalPosition = new Vector3(0f, 0f, -initialZoomLevel);

        // Set the initial local position of the camera within the CameraPivot
        Camera.main.transform.localPosition = initialLocalPosition;
    }

    void OnSliderValueChanged(float value)
    {
        // Map the slider value to the zoom range
        float zoomLevel = Mathf.Lerp(minZoom, maxZoom, value);

        // Calculate the new Z position based on the zoom level
        float newZPosition = -zoomLevel;

        // Get the current local position of the camera
        Vector3 currentLocalPosition = Camera.main.transform.localPosition;

        // Update only the Z position
        currentLocalPosition.z = newZPosition;

        // Set the new local position of the camera within the CameraPivot
        Camera.main.transform.localPosition = currentLocalPosition;
    }
}