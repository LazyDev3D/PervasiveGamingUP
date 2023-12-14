using UnityEngine;
using Leap;
using Leap.Unity;

public class LeapPointing : MonoBehaviour
{
    private LeapServiceProvider leapProvider;
    private GameObject lastPointedObject;

    void Start()
    {
        // Attempt to get the LeapServiceProvider component on the same GameObject
        leapProvider = GetComponent<LeapServiceProvider>();

        // Check if the LeapServiceProvider is found
        if (leapProvider == null)
        {
            Debug.LogError("LeapServiceProvider not found on the same GameObject as LeapPointing.");
        }
    }

    void Update()
    {
        if (leapProvider != null)
        {
            // Get the current Leap Motion frame from the LeapServiceProvider
            Frame frame = leapProvider.CurrentFrame;

            // Check if the frame is valid and hands are present
            if (frame != null && frame.Hands.Count > 0)
            {
                // Iterate through each hand in the frame
                foreach (Hand hand in frame.Hands)
                {
                    // Check if the hand is valid and either left or right
                    if (hand != null && (hand.IsLeft || hand.IsRight))
                    {
                        // Assuming index finger is tracked on the hand
                        Finger indexFinger = hand.Fingers[(int)Finger.FingerType.TYPE_INDEX];

                        // Assuming the bone structure you provided, adjust the hierarchy
                        Vector3 indexFingerTip = indexFinger.TipPosition;

                        // Use Leap Motion hand tracking to raycast from the index finger tip
                        Ray ray = new Ray(indexFingerTip, Camera.main.transform.forward);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            lastPointedObject = hit.collider.gameObject;
                            Debug.Log("Pointed at: " + lastPointedObject.name);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("LeapServiceProvider is null. Make sure it's attached to the same GameObject as LeapPointing.");
            }
        }
    }

    public GameObject GetLastPointedObject()
    {
        return lastPointedObject;
    }
}