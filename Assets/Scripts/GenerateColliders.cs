using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAddMeshColliders : MonoBehaviour
{
    void Start()
    {
        AddMeshCollidersRecursively(transform);
    }

    void AddMeshCollidersRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Check if the current child has a MeshFilter component
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();

            if (meshFilter != null)
            {
                // Add a MeshCollider to the current bone
                MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();

                // Set the mesh of the collider to the same mesh as the MeshFilter
                meshCollider.sharedMesh = meshFilter.sharedMesh;
            }

            // Recursively process child bones
            AddMeshCollidersRecursively(child);
        }
    }
}
