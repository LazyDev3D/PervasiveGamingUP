using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnatomyBodyMenu : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 10, 0);


    public GameObject skinObject;
    public GameObject muscleObject;
    public GameObject skeletonObject;
    public float skinVisibilityDelay = 5f;
    public float muscleVisibilityDelay = 10f;
    public float visibilityCycleDuration = 15f;

    private float timer = 0f;

    void Update()
    {
        // Rotate the object continuously
        transform.Rotate(rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        // Check the current stage of the visibility cycle
        if (timer < skinVisibilityDelay)
        {
            // Stage 1: All layers visible
            ToggleLayerVisibility(skinObject, true);
            ToggleLayerVisibility(muscleObject, true);
            ToggleLayerVisibility(skeletonObject, true);
        }
        else if (timer < muscleVisibilityDelay)
        {
            // Stage 2: Skin becomes invisible
            ToggleLayerVisibility(skinObject, false);
        }
        else if (timer < visibilityCycleDuration)
        {
            // Stage 3: Muscles become invisible
            ToggleLayerVisibility(muscleObject, false);
        }
        else
        {
            // Reset the timer for the next cycle
            timer = 0f;
        }
    }

    void ToggleLayerVisibility(GameObject layerObject, bool isVisible)
    {
        // Toggle visibility for the specified layer
        if (layerObject != null)
        {
            layerObject.SetActive(isVisible);
        }
    }
}
