using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the empty GameObject around its up axis (Y-axis) based on horizontal input
        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);

        // Rotate the empty GameObject around its right axis (X-axis) based on vertical input
        transform.Rotate(Vector3.right, rotationSpeed * verticalInput * Time.deltaTime);
    }
}