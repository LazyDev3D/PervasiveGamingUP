using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float keyboardRotationSpeed = 100f;
    public float mouseRotationSpeed = 500f;

    void Update()
    {
        // Get input from arrow keys and WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            // Get mouse movement for rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y") *-1;

            // Rotate the empty GameObject around its up axis (Y-axis) based on mouse X movement
            transform.Rotate(Vector3.up, mouseRotationSpeed * mouseX * Time.deltaTime);

            // Rotate the empty GameObject around its right axis (X-axis) based on mouse Y movement
            transform.Rotate(Vector3.right, mouseRotationSpeed * mouseY * Time.deltaTime);
        }
        else
        {
            // Rotate the empty GameObject around its up axis (Y-axis) based on horizontal input
            transform.Rotate(Vector3.up, keyboardRotationSpeed * horizontalInput * Time.deltaTime);

            // Rotate the empty GameObject around its right axis (X-axis) based on vertical input
            transform.Rotate(Vector3.right, keyboardRotationSpeed * verticalInput * Time.deltaTime);
        }
    }
}
