using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public Vector3 offset;          // Offset from the player
    public float smoothSpeed = 0.125f;  // Speed of camera smoothing
    public float rotationSpeed = 5.0f;  // Speed of camera rotation
    public float zoomSpeed = 4.0f;      // Speed of zooming in/out
    public float minZoom = 5.0f;        // Minimum zoom distance
    public float maxZoom = 15.0f;       // Maximum zoom distance

    private float currentZoom = 10.0f;  // Current zoom distance
    private float currentYaw = 0.0f;    // Current yaw rotation

    // Camera shake parameters
    public float shakeMagnitude = 0.1f;
    public float shakeRoughness = 0.1f;
    public float shakeFadeIn = 0.1f;
    public float shakeFadeOut = 0.1f;

    void Update()
    {
        // Zooming in/out with mouse scroll wheel
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Rotate camera around player with right mouse button
        if (Input.GetMouseButton(1))
        {
            currentYaw -= Input.GetAxis("Mouse X") * rotationSpeed;

            // Trigger camera shake
            CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRoughness, shakeFadeIn, shakeFadeOut);
        }
    }

    void LateUpdate()
    {
        // Desired position of the camera
        Vector3 desiredPosition = player.position - offset * currentZoom;
        // Smoothly interpolate between current position and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Look at the player
        transform.LookAt(player.position + Vector3.up * 1.5f);

        // Rotate the camera around the player
        transform.RotateAround(player.position, Vector3.up, currentYaw);
    }
}
