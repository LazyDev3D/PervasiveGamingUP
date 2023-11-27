using UnityEngine;
using UnityEngine.UI;

public class MeshVisibilityToggle : MonoBehaviour
{
    public Button Button;
    public GameObject SkinMesh;
    public GameObject MuscleMesh;
    public GameObject SkeletonMesh;

    void Start()
    {
        // Add a listener to the skinButton
        if (Button != null)
        {
            Button.onClick.AddListener(ToggleSkinVisibility);
        }
    }

    void ToggleSkinVisibility()
    {
        // Toggle the visibility of the skin mesh
        if (SkinMesh != null)
        {
            SkinMesh.SetActive(!SkinMesh.activeSelf);
        }
    }

    void Update()
    {
        // Check for numeric key presses (1, 2, 3)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleLayerVisibility(SkinMesh, true);
            ToggleLayerVisibility(MuscleMesh, false);
            ToggleLayerVisibility(SkeletonMesh, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleLayerVisibility(SkinMesh, false);
            ToggleLayerVisibility(MuscleMesh, true);
            ToggleLayerVisibility(SkeletonMesh, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleLayerVisibility(SkinMesh, false);
            ToggleLayerVisibility(MuscleMesh, false);
            ToggleLayerVisibility(SkeletonMesh, true);
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
