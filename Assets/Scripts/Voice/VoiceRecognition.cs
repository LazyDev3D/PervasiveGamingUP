using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;
using System.Linq; // Add this using directive for LINQ

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> voiceCommands = new Dictionary<string, System.Action>();

    private bool isRotating = false;
    private Vector3 rotatingAxis = Vector3.zero;
    private float rotationSpeed = 30f;
    private float minRotationSpeed = 10f;
    private float maxRotationSpeed = 50f;
    private float rotationSpeedChangeAmount = 5f;


    private bool isZoomingIn = false;
    private bool isZoomingOut = false;
    private bool isZooming = false;
    public float zoomSpeed = 5f;

    public TextMeshProUGUI recognitionText; // Reference to your TMP Text element

    public Camera mainCamera;

    void Start()
    {
        // Set up voice commands
        voiceCommands.Add("rotate left", () => StartContinuousRotation(Vector3.up));
        voiceCommands.Add("rotate right", () => StartContinuousRotation(Vector3.down));
        voiceCommands.Add("rotate up", () => StartContinuousRotation(Vector3.left));
        voiceCommands.Add("rotate down", () => StartContinuousRotation(Vector3.right));
        voiceCommands.Add("stop", StopContinuousCommands);
        voiceCommands.Add("slower", DecreaseRotationSpeed);
        voiceCommands.Add("faster", IncreaseRotationSpeed);
        voiceCommands.Add("zoom in", StartZoomIn);
        voiceCommands.Add("zoom out", StartZoomOut);

        // Initialize KeywordRecognizer with the voice command dictionary
        keywordRecognizer = new KeywordRecognizer(voiceCommands.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        // Execute the action associated with the recognized command
        voiceCommands[speech.text]?.Invoke();

        // Update the recognition text on the UI
        recognitionText.text = "You said: " + speech.text;
    }


    void Update()
    {
        if (isRotating)
        {
            // Implement continuous rotation logic here
            // For example, rotate the camera continuously around the specified axis
            // You can use Time.deltaTime to make the rotation frame-rate independent
            RotateCamera(rotatingAxis);

        }

        if (isZooming)
        {
            // Implement continuous zoom logic here
            // For example, move the camera forward or backward
            // You can use Time.deltaTime to make the zoom frame-rate independent
            ZoomCamera();
        }

    }

    public void RotateCamera(Vector3 axis)
    {
        // Implement your camera rotation logic here
        // Rotate the camera continuously around the specified axis
        // You can use Time.deltaTime to make the rotation frame-rate independent
        transform.Rotate(axis * Time.deltaTime * rotationSpeed);
    }

    public void StartContinuousRotation(Vector3 axis)
    {
        isRotating = true;
        rotatingAxis = axis;
    }

    public void StopContinuousRotation()
    {
        isRotating = false;
    }

    private void DecreaseRotationSpeed()
    {
        rotationSpeed = Mathf.Max(rotationSpeed - rotationSpeedChangeAmount, minRotationSpeed);
        Debug.Log("Rotation speed decreased to: " + rotationSpeed);
    }

    private void IncreaseRotationSpeed()
    {
        rotationSpeed = Mathf.Min(rotationSpeed + rotationSpeedChangeAmount, maxRotationSpeed);
        Debug.Log("Rotation speed increased to: " + rotationSpeed);
    }

    public void StopContinuousCommands()
    {
        isRotating = false;
        StopContinuousZoom();
        recognitionText.text = "Continuous commands stopped.";
    }

    private void StartZoomIn()
    {
        isZoomingIn = true;
        isZoomingOut = false;
    }

    private void StartZoomOut()
    {
        isZoomingIn = false;
        isZoomingOut = true;
    }

    private void StopZoom()
    {
        isZoomingIn = false;
        isZoomingOut = false;
        recognitionText.text = "Zoom stopped.";
    }

    private void StopContinuousZoom()
    {
        isZooming = false;
        recognitionText.text = "Continuous zoom stopped.";
    }

    private void ZoomCamera()
    {
        float zoomDirection = 0f;

        if (isZoomingIn)
        {
            zoomDirection = 1f;
        }
        else if (isZoomingOut)
        {
            zoomDirection = -1f;
        }

        // Adjust this value based on your scene and how much you want to zoom
        float zoomSpeedFactor = 5f;

        // Calculate the new position
        Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward * zoomDirection * Time.deltaTime * zoomSpeed * zoomSpeedFactor;

        // Add a check to ensure you don't zoom out too far
        float minZoomDistance = 5f; // Adjust this value based on your scene
        float maxZoomDistance = 20f; // Adjust this value based on your scene

        float newZoomDistance = Vector3.Distance(mainCamera.transform.position, transform.position);

        newZoomDistance = Mathf.Clamp(newZoomDistance, minZoomDistance, maxZoomDistance);

        // Set the new position
        mainCamera.transform.position = transform.position + mainCamera.transform.forward * newZoomDistance;
    }
}