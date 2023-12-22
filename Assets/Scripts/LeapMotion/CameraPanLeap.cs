using UnityEngine;
using Leap.Unity.Interaction;

public class LeapMotionSliderCameraPan : MonoBehaviour
{
    public float panSpeed = 0.1f;
    public InteractionSlider horizontalSlider;
    public InteractionSlider verticalSlider;

    private bool isPanning = false;
    private float lastHorizontalValue;
    private float lastVerticalValue;

    void Update()
    {
        // Handle camera panning with the InteractionSliders
        HandlePanning();
    }

    void HandlePanning()
    {
        if (horizontalSlider != null && verticalSlider != null)
        {
            float horizontalValue = horizontalSlider.HorizontalSliderValue;
            float verticalValue = verticalSlider.VerticalSliderValue;

            if (!isPanning)
            {
                isPanning = true;
                lastHorizontalValue = horizontalValue;
                lastVerticalValue = verticalValue;
            }

            // Calculate the differences in slider values
            float deltaHorizontal = horizontalValue - lastHorizontalValue;
            float deltaVertical = verticalValue - lastVerticalValue;

            // Pan the camera based on the slider movements
            PanCamera(deltaHorizontal, deltaVertical);

            // Update the last slider values
            lastHorizontalValue = horizontalValue;
            lastVerticalValue = verticalValue;
        }
        else
        {
            isPanning = false;
        }
    }

    void PanCamera(float deltaHorizontal, float deltaVertical)
    {
        // Convert the slider movements to pan directions in world space
        Vector3 panDirection = new Vector3(deltaHorizontal, deltaVertical, 0) * panSpeed;

        // Apply the pan direction to the camera's position
        transform.Translate(panDirection, Space.World);
    }
}