using UnityEngine;
using UnityEngine.UI;

public class MeshVisibilityToggle : MonoBehaviour
{
    public Button SkinButton;
    public Button MuscleButton;
    public Button SkeletonButton;

    public GameObject SkinMesh;
    public GameObject MuscleMesh;
    public GameObject SkeletonMesh;

    void Start()
    {
        // Add listeners to the buttons
        if (SkinButton != null)
        {
            SkinButton.onClick.AddListener(ToggleSkinVisibility);
        }

        if (MuscleButton != null)
        {
            MuscleButton.onClick.AddListener(ToggleMuscleVisibility);
        }

        if (SkeletonButton != null)
        {
            SkeletonButton.onClick.AddListener(ToggleSkeletonVisibility);
        }
    }

    public void ToggleSkinVisibility()
    {
        ToggleLayerVisibility(SkinMesh, true);
        ToggleLayerVisibility(MuscleMesh, false);
        ToggleLayerVisibility(SkeletonMesh, false);
    }

    public void ToggleMuscleVisibility()
    {
        ToggleLayerVisibility(SkinMesh, false);
        ToggleLayerVisibility(MuscleMesh, true);
        ToggleLayerVisibility(SkeletonMesh, false);
    }

    public void ToggleSkeletonVisibility()
    {
        ToggleLayerVisibility(SkinMesh, false);
        ToggleLayerVisibility(MuscleMesh, false);
        ToggleLayerVisibility(SkeletonMesh, true);
    }

    void Update()
    {
        // Check for numeric key presses (1, 2, 3)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleSkinVisibility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleMuscleVisibility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleSkeletonVisibility();
        }
    }

    void ToggleLayerVisibility(GameObject layerMesh, bool isVisible)
    {
        // Toggle visibility for the specified layer
        if (layerMesh != null)
        {
            layerMesh.SetActive(isVisible);
        }
    }
}
