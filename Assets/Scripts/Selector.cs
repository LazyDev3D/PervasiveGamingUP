using UnityEngine;

public class SkeletonPartVisibility : MonoBehaviour
{
    public LayerMask skeletonLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, skeletonLayer))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);

                ToggleSkeletonVisibility(false);
                hit.collider.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Clicked outside the skeleton");

                ToggleSkeletonVisibility(true);
            }
        }
    }
    void ToggleSkeletonVisibility(bool isVisible)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isVisible);
        }
    }
}
