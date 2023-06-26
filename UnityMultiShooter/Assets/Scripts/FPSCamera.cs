using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float verticalClampAngle = 90f;
    public float cameraOffset = 0.6f;
    private float cameraFrontOffset = 0.5f;

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial rotations based on current camera and player body rotations
        verticalRotation = transform.localRotation.eulerAngles.x;
        horizontalRotation = playerBody.localRotation.eulerAngles.y;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClampAngle, verticalClampAngle);

        Quaternion horizontalQuaternion = Quaternion.Euler(0f, horizontalRotation, 0f);
        Quaternion verticalQuaternion = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        playerBody.rotation = horizontalQuaternion;
        transform.localRotation = verticalQuaternion;

        // Set camera position on the player's head
        Vector3 desiredPosition = playerBody.position + playerBody.up * cameraOffset;
        desiredPosition += playerBody.forward * cameraFrontOffset;
        transform.position = desiredPosition;
    }
}