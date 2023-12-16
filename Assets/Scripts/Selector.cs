using UnityEngine;

public class SkeletonPartVisibility : MonoBehaviour
{
    public GameObject skinObject;
    public GameObject musclesObject;
    public GameObject skeletonObject;

    private GameObject lastClickedObject;
    public bool isInIsolationMode;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);

                GameObject clickedObject = hit.collider.gameObject;

                if (lastClickedObject == clickedObject)
                {
                    // Clicked the same object again
                    if (isInIsolationMode)
                    {
                        // Toggle visibility for the entire layer of the last clicked object
                        ToggleLayerVisibility(skinObject, true);
                        ToggleLayerVisibility(musclesObject, true);
                        ToggleLayerVisibility(skeletonObject, true);

                        isInIsolationMode = false;
                    }
                    else
                    {
                        // Toggle visibility for the entire layer
                        ToggleLayerVisibility(skinObject, true);
                        ToggleLayerVisibility(musclesObject, true);
                        ToggleLayerVisibility(skeletonObject, true);
                    }
                }
                else
                {
                    // Clicked a different object, toggle visibility of the clicked layer
                    ToggleLayerVisibility(skinObject, false);
                    ToggleLayerVisibility(musclesObject, false);
                    ToggleLayerVisibility(skeletonObject, false);

                    // Then, toggle visibility for the specific clicked object
                    clickedObject.SetActive(true);

                    lastClickedObject = clickedObject;
                    isInIsolationMode = true;
                }
            }
        }
    }

    public void ToggleLayerVisibility(GameObject layerObject, bool isVisible)
    {
        foreach (Transform child in layerObject.transform)
        {
            child.gameObject.SetActive(isVisible);
        }
    }

    public void IsolatePointedObject(GameObject pointedObject)
    {
        ToggleLayerVisibility(skinObject, false);
        ToggleLayerVisibility(musclesObject, false);
        ToggleLayerVisibility(skeletonObject, false);

        pointedObject.SetActive(true);

        lastClickedObject = pointedObject;
        isInIsolationMode = true;
    }
}