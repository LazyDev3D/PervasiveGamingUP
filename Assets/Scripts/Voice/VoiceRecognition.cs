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

    private bool isPanning = false;
    private Vector3 lastMousePosition;
    public float panSpeed = 0.1f;

    public TextMeshProUGUI recognitionText; // Reference to your TMP Text element

    public Camera mainCamera;

    void Start()
    {
        string[] keywords = { "What is this" };
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
        voiceCommands.Add("pan up", () => StartPan(Vector3.up));
        voiceCommands.Add("pan down", () => StartPan(Vector3.down));
        voiceCommands.Add("pan left", () => StartPan(Vector3.left));
        voiceCommands.Add("pan right", () => StartPan(Vector3.right));


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
        Debug.Log("Update method called");

        if (isRotating)
        {
            Debug.Log("Rotating");
            RotateCamera(rotatingAxis);
        }

        if (isZooming)
        {
            Debug.Log("Zooming");
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
        isZooming = true;
        isZoomingIn = true;
        isZoomingOut = false;
    }

    private void StartZoomOut()
    {
        isZooming = true;
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
            zoomDirection = 1f; // Inverted direction for zooming in
        }
        else if (isZoomingOut)
        {
            zoomDirection = -1f; // Inverted direction for zooming out
        }

        // Adjust this value based on how much you want to zoom
        float zoomSpeedFactor = 2f; // Adjust this value based on your scene

        // Calculate the new position
        Vector3 newPosition = mainCamera.transform.localPosition + mainCamera.transform.forward * zoomDirection * Time.deltaTime * zoomSpeed * zoomSpeedFactor;

        // Set the new position
        mainCamera.transform.localPosition = newPosition;
    }
}