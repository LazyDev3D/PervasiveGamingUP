using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;
using System.Linq; // Add this using directive for LINQ
using Leap.Unity.HandsModule;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> voiceCommands = new Dictionary<string, System.Action>();

    private bool isRotating = false;
    private Vector3 rotatingAxis = Vector3.zero;
    private float rotationSpeed = 30f;
    private float zoomSpeed = 1.0f; // Initial zoom speed
    private float minRotationSpeed = 10f;
    private float maxRotationSpeed = 50f;
    private float rotationSpeedChangeAmount = 5f;

    public PanelMovement HelpMenu;

    private bool isZoomingIn = false;
    private bool isZoomingOut = false;
    private bool isZooming = false;
    public float zoomSpeedFactor = 5f;

    private bool isPanning = false;
    private Vector3 panDirection = Vector3.zero;
    public float panSpeed = .7f;

    public TextMeshProUGUI recognitionText; // Reference to your TMP Text element

    public Camera mainCamera;

    public CameraReset cameraReset;
    public CameraResetScript CameraResetScript;

    public LeapPointing LeapPointing;
    public SkeletonPartVisibility skeletonPartVisibility;
    private ObjectDescriptionManager objectDescriptionManager;


    void Start()
    {
        string[] keywords = { "What is this" };
        // Set up voice commands
        voiceCommands.Add("rotate left", () => StartContinuousRotation(Vector3.up));
        voiceCommands.Add("rotate right", () => StartContinuousRotation(Vector3.down));
        voiceCommands.Add("rotate up", () => StartContinuousRotation(Vector3.left));
        voiceCommands.Add("rotate down", () => StartContinuousRotation(Vector3.right));
        voiceCommands.Add("stop", StopContinuousCommands);
        voiceCommands.Add("slower", DecreaseSpeed);
        voiceCommands.Add("faster", IncreaseSpeed);
        voiceCommands.Add("zoom in", StartZoomIn);
        voiceCommands.Add("zoom out", StartZoomOut);
        voiceCommands.Add("help", help);
        voiceCommands.Add("pan up", () => StartPan(Vector3.up));
        voiceCommands.Add("pan down", () => StartPan(Vector3.down));
        voiceCommands.Add("pan left", () => StartPan(Vector3.left));
        voiceCommands.Add("pan right", () => StartPan(Vector3.right));
        voiceCommands.Add("reset", ResetCamera);
        voiceCommands.Add("reset camera", ResetCamera);
        voiceCommands.Add("What's this", IdentifyLastPointedObject);
        voiceCommands.Add("What is this", IdentifyLastPointedObject);
        voiceCommands.Add("What is that", IdentifyLastPointedObject);
        voiceCommands.Add("What's that", IdentifyLastPointedObject);
        voiceCommands.Add("show all", ShowAll);



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
        if (isPanning)
        {
            PanCamera(panDirection);
        }

        if (isRotating)
        {
            RotateCamera(rotatingAxis);
        }

        if (isZooming)
        {
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

    private void DecreaseSpeed()
    {
        rotationSpeed = Mathf.Max(rotationSpeed - rotationSpeedChangeAmount, minRotationSpeed);
        zoomSpeed /= 1.5f;
        Debug.Log("Rotation speed decreased to: " + rotationSpeed);
    }

    private void IncreaseSpeed()
    {
        rotationSpeed = Mathf.Min(rotationSpeed + rotationSpeedChangeAmount, maxRotationSpeed);
        zoomSpeed *= 1.5f;
        Debug.Log("Rotation speed increased to: " + rotationSpeed);
    }

    public void StopContinuousCommands()
    {
        isRotating = false;
        StopContinuousZoom();
        isPanning = false;
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

        // Calculate the new position
        Vector3 newPosition = mainCamera.transform.localPosition + mainCamera.transform.forward * zoomDirection * Time.deltaTime * zoomSpeed * zoomSpeedFactor;

        // Set the new position
        mainCamera.transform.localPosition = newPosition;
    }

    private void StartPan(Vector3 direction)
    {
        isPanning = true;
        panDirection = direction;
    }

    private void StopPan()
    {
        isPanning = false;
        recognitionText.text = "Panning stopped.";
    }

    void PanCamera(Vector3 direction)
    {
        // Adjust pan speed based on your preferences
        float adjustedPanSpeed = panSpeed * Time.deltaTime;

        // Apply the pan direction to the camera's position
        transform.Translate(direction * adjustedPanSpeed, Space.World);
    }

    private void ResetCamera()
    {
        // Call the ResetCamera method in the CameraReset script
        cameraReset.ResetCamera();
        CameraResetScript.StartReset();
    }

    private void IdentifyLastPointedObject()
    {

        if (LeapPointing != null && skeletonPartVisibility != null)
        {
            GameObject lastPointedObject = LeapPointing.lastPointedObject;

            if (lastPointedObject != null)
            {
                // Assuming that the layer of the last pointed object corresponds to the layer you want to toggle
                int layerToToggle = lastPointedObject.layer;

                // Call the ToggleLayerVisibility function from the SkeletonPartVisibility script
                skeletonPartVisibility.ToggleLayerVisibility(lastPointedObject, !lastPointedObject.activeSelf);

                // Toggle isolation mode in SkeletonPartVisibility
                skeletonPartVisibility.IsolatePointedObject(lastPointedObject);
                skeletonPartVisibility.UpdateInfoPanel();
                skeletonPartVisibility.UpdateInfoPanelVisibility();
                string meshName = lastPointedObject.name;
                if (objectDescriptionManager != null)
                {
                    objectDescriptionManager.ShowObjectDescription(meshName);
                }
            }
            else
            {
                Debug.Log("No object pointed.");
            }
        }
        else
        {
            Debug.LogError("LeapPointingScript or SkeletonPartVisibility script not found.");
        }
    }

    private void ShowAll()
    {
        if (skeletonPartVisibility != null)
        {
            // Toggle visibility for all layers
            skeletonPartVisibility.ToggleLayerVisibility(skeletonPartVisibility.skinObject, true);
            skeletonPartVisibility.ToggleLayerVisibility(skeletonPartVisibility.musclesObject, true);
            skeletonPartVisibility.ToggleLayerVisibility(skeletonPartVisibility.skeletonObject, true);
            skeletonPartVisibility.HideInfoPanel();

            GameObject lastPointedObject = LeapPointing.lastPointedObject;

            lastPointedObject = null;
            skeletonPartVisibility.isInIsolationMode = false;

            Debug.Log("Show all layers.");
        }
        else
        {
            Debug.LogError("SkeletonPartVisibility script not found.");
        }
    }

    private void help()
    {
        HelpMenu.TogglePanelVisibility();
    }
}