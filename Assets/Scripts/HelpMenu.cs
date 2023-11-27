using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelMovement : MonoBehaviour
{
    public Button moveButton;
    public RectTransform panelRectTransform;
    public float moveDuration = 1.5f;
    public float targetX = 100f; // Target x-coordinate when the panel is fully visible

    private bool isPanelVisible = false;

    void Start()
    {
        if (moveButton != null)
        {
            moveButton.onClick.AddListener(TogglePanelVisibility);
        }

        if (panelRectTransform == null)
        {
            Debug.LogError("Panel RectTransform not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Check for the 'H' key press
        if (Input.GetKeyDown(KeyCode.H))
        {
            TogglePanelVisibility();
        }
    }

    void TogglePanelVisibility()
    {
        float targetXPosition = isPanelVisible ? -800f : targetX;
        StartCoroutine(MovePanelCoroutine(targetXPosition));

        isPanelVisible = !isPanelVisible;
    }

    IEnumerator MovePanelCoroutine(float targetXPosition)
    {
        float elapsedTime = 0f;
        float startX = panelRectTransform.anchoredPosition.x;
        Vector2 targetPosition = new Vector2(targetXPosition, panelRectTransform.anchoredPosition.y);

        while (elapsedTime < moveDuration)
        {
            panelRectTransform.anchoredPosition = Vector2.Lerp(
                new Vector2(startX, panelRectTransform.anchoredPosition.y),
                targetPosition,
                elapsedTime / moveDuration
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelRectTransform.anchoredPosition = targetPosition;
    }
}