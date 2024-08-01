using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class OverviewCameraController : MonoBehaviour
{
    public Transform target; // The target the camera will look at
    public float distance = 50f; // Distance from the target
    public float height = 30f; // Height above the target
    public float rotationSpeed = 10f; // Speed of rotation around the target

    private float currentAngle = 0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for the OverviewCameraController.");
            enabled = false;
            return;
        }
        // Set the initial position and rotation
        UpdateCameraPosition();
    }

    void Update()
    {
        // Rotate the camera around the target
        currentAngle += rotationSpeed * Time.deltaTime;
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Calculate the new position
        Vector3 offset = new Vector3(
            Mathf.Sin(currentAngle) * distance,
            height,
            Mathf.Cos(currentAngle) * distance
        );
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}

