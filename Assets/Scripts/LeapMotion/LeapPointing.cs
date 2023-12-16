using UnityEngine;
using Leap;
using Leap.Unity;

public class LeapPointing : MonoBehaviour
{
    private LeapServiceProvider leapProvider;
    public GameObject lastPointedObject;

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
        Vector3 indexFingerTip = Vector3.zero;
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

                        // Adjust the hierarchy
                        indexFingerTip = indexFinger.TipPosition;

                        // Use Leap Motion hand tracking to raycast from the index finger tip
                        Debug.DrawLine(indexFingerTip, indexFingerTip + hand.Fingers[1].bones[(int)Bone.BoneType.TYPE_DISTAL].Direction * 10f, Color.red, 0.1f);

                        // Perform the ray cast
                        Ray ray = new Ray(indexFingerTip, hand.Fingers[1].bones[(int)Bone.BoneType.TYPE_DISTAL].Direction);
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
                // Handle the case where there are no hands
            }
        }
    }

    public GameObject GetLastPointedObject()
    {
        return lastPointedObject;
    }
}