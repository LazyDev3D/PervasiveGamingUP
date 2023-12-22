using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDescriptionManager : MonoBehaviour
{
    // Dictionary to store mesh names and their corresponding descriptions
    private Dictionary<string, string> objectDescriptions = new Dictionary<string, string>();

    // Reference to the TextMeshPro component for displaying descriptions
    public TextMeshProUGUI infoContentText;

    // Add mesh names and descriptions to the dictionary in the Unity Editor
    public void Awake()
    {
        // Face Muscles
        objectDescriptions.Add("OrbicularisOris", "The orbicularis oris is a complex of muscles around the mouth responsible for various movements, including puckering and closing the lips.");
        objectDescriptions.Add("ZygomaticusMajor", "The zygomaticus major is a facial muscle that extends from the cheekbone to the corner of the mouth, aiding in smiling.");
        objectDescriptions.Add("Frontalis", "The frontalis is a muscle in the forehead region that contracts to raise the eyebrows and create forehead wrinkles.");

        // Wrist Muscles
        objectDescriptions.Add("FlexorCarpiRadialis", "The flexor carpi radialis is a forearm muscle that flexes and abducts the hand at the wrist.");
        objectDescriptions.Add("FlexorCarpiUlnaris", "The flexor carpi ulnaris is a forearm muscle responsible for flexion and adduction of the hand at the wrist.");
        objectDescriptions.Add("ExtensorCarpiRadialisBrevis", "The extensor carpi radialis brevis is a forearm muscle that extends and abducts the wrist.");
        objectDescriptions.Add("ExtensorCarpiUlnaris", "The extensor carpi ulnaris is a forearm muscle that extends and adducts the wrist.");

        // Upper Body Muscles
        objectDescriptions.Add("Breasts", "The breasts, also known as mammary glands, contain milk-producing glands and are part of the female reproductive system.");
        objectDescriptions.Add("PectoralisMajor", "The pectoralis major is a large, fan-shaped muscle on the chest that contributes to shoulder movement.");
        objectDescriptions.Add("PectoralisMinor", "The pectoralis minor is a thin, triangular muscle located beneath the pectoralis major, aiding in shoulder movement.");

        objectDescriptions.Add("AnteriorDeltoid", "The anterior deltoid is the front part of the deltoid muscle, contributing to shoulder flexion.");
        objectDescriptions.Add("LateralDeltoid", "The lateral deltoid is the side part of the deltoid muscle, assisting in shoulder abduction.");
        objectDescriptions.Add("PosteriorDeltoid", "The posterior deltoid is the rear part of the deltoid muscle, involved in shoulder extension.");
        objectDescriptions.Add("TricepsBrachii", "The triceps brachii is a three-headed muscle located on the back of the upper arm, responsible for elbow extension.");
        objectDescriptions.Add("Bicep", "The bicep is a two-headed muscle in the upper arm, contributing to elbow flexion.");

        objectDescriptions.Add("LatissimusDorsi", "The latissimus dorsi is a broad muscle of the back responsible for shoulder adduction, extension, and internal rotation.");

        objectDescriptions.Add("Trapezius", "The trapezius is a large muscle that extends down the back of the neck and upper spine, supporting shoulder movement.");

        objectDescriptions.Add("TeresMajor", "The teres major is a muscle of the upper limb that attaches to the scapula and assists in shoulder adduction and internal rotation.");

        objectDescriptions.Add("TeresMinor", "The teres minor is a small, thin muscle of the upper limb, aiding in shoulder external rotation.");

        // Abdominal Muscles
        objectDescriptions.Add("RectusAbdominis", "The rectus abdominis, commonly known as the 'six-pack,' is a paired muscle running vertically along the front of the abdomen, contributing to trunk flexion.");

        // Leg Muscles
        objectDescriptions.Add("QuadricepsFemoris", "The quadriceps femoris is a group of four muscles on the front of the thigh responsible for knee extension.");
        objectDescriptions.Add("Hamstrings", "The hamstrings are a group of muscles on the back of the thigh involved in knee flexion and hip extension.");
        objectDescriptions.Add("Gastrocnemius", "The gastrocnemius is the prominent muscle of the calf, contributing to ankle plantar flexion.");
        objectDescriptions.Add("Soleus", "The soleus is a powerful muscle in the calf that plays a key role in standing and walking.");
        objectDescriptions.Add("AdductorMagnus", "The adductor magnus is a large triangular muscle in the thigh, contributing to thigh adduction and hip extension.");
        objectDescriptions.Add("TensorFasciaeLatae", "The tensor fasciae latae is a muscle of the thigh that helps stabilize the hip and knee.");
        objectDescriptions.Add("Gracilis", "The gracilis is a long, thin muscle that runs down the inner thigh, contributing to thigh adduction.");
        objectDescriptions.Add("Sartorius", "The sartorius is the longest muscle in the human body, crossing from the hip to the knee and aiding in thigh flexion and rotation.");

        // Glutes
        objectDescriptions.Add("GluteusMaximus", "The gluteus maximus is the largest of the gluteal muscles and plays a major role in hip extension.");
        objectDescriptions.Add("GluteusMedius", "The gluteus medius is one of the gluteal muscles, contributing to hip abduction and stabilization of the pelvis.");
        objectDescriptions.Add("GluteusMinimus", "The gluteus minimus is one of the gluteal muscles, assisting in hip abduction and stabilization.");

    }

    // Method to show the description for a given mesh name
    public void ShowObjectDescription(string meshName)
    {
        Debug.Log("ShowObjectDescription called with meshName: " + meshName);

        if (objectDescriptions.ContainsKey(meshName))
        {
            infoContentText.text = objectDescriptions[meshName];
            Debug.Log("Description set to: " + objectDescriptions[meshName]);
        }
        else
        {
            infoContentText.text = string.Empty;
            Debug.Log("Mesh name not found in dictionary.");
        }
    }
}