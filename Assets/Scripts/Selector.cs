using TMPro;
using UnityEngine;

public class SkeletonPartVisibility : MonoBehaviour
{
    public GameObject skinObject;
    public GameObject musclesObject;
    public GameObject skeletonObject;

    private GameObject lastClickedObject;
    public bool isInIsolationMode;

    public GameObject infoPanel;
    public TextMeshProUGUI infoTitle;

    public ObjectDescriptionManager objectDescriptionManager;

    void Start()
    {
        // Ensure the initial state
        ToggleInfoPanelVisibility(false);
    }
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
                    if (isInIsolationMode)
                    {
                        ToggleLayerVisibility(skinObject, true);
                        ToggleLayerVisibility(musclesObject, true);
                        ToggleLayerVisibility(skeletonObject, true);

                        isInIsolationMode = false;
                        UpdateInfoPanelVisibility();
                    }
                    else
                    {
                        ToggleLayerVisibility(skinObject, true);
                        ToggleLayerVisibility(musclesObject, true);
                        ToggleLayerVisibility(skeletonObject, true);
                    }
                }
                else
                {
                    ToggleLayerVisibility(skinObject, false);
                    ToggleLayerVisibility(musclesObject, false);
                    ToggleLayerVisibility(skeletonObject, false);

                    clickedObject.SetActive(true);
                    lastClickedObject = clickedObject;
                    isInIsolationMode = true;

                    UpdateInfoPanel();
                    UpdateInfoPanelVisibility();
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

    public void ToggleInfoPanelVisibility(bool isVisible)
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(isVisible);
        }
    }

    public void UpdateInfoPanel()
    {
        if (infoPanel != null && infoTitle != null && lastClickedObject != null)
        {
            // Assuming lastClickedObject has a name to display
            infoTitle.text = lastClickedObject.name;
            string meshName = lastClickedObject.name;
            objectDescriptionManager.ShowObjectDescription(meshName);
        }
    }

    public void UpdateInfoPanelVisibility()
    {
        // Make the InfoPanel visible based on the isolation mode
        ToggleInfoPanelVisibility(isInIsolationMode);
    }

    public void HideInfoPanel()
    {
        // Assuming InfoPanel is a GameObject that needs to be hidden
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

}