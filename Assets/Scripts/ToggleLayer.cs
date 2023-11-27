using UnityEngine;
using UnityEngine.UI;

public class MeshVisibilityToggle : MonoBehaviour
{
    public Button Button;
    public GameObject Mesh;

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
        if (Mesh != null)
        {
            Mesh.SetActive(!Mesh.activeSelf);
        }
    }
}
