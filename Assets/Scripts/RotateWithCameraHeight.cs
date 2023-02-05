using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X,
    Y,
    Z
}

public class RotateWithCameraHeight : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Axis axis = Axis.Z;

    [SerializeField] private float rotateSpeed = 1f;

    private void Update()
    {
        Vector3 desiredRotation = transform.rotation.eulerAngles;
        switch (axis)
        {
            case Axis.X:
                desiredRotation.x = (cameraTransform.position.y % 360) * rotateSpeed * Time.deltaTime;
                break;
            case Axis.Y:
                desiredRotation.y = (cameraTransform.position.y % 360) * rotateSpeed * Time.deltaTime;
                break;
            case Axis.Z:
                desiredRotation.z = (cameraTransform.position.y % 360) * rotateSpeed * Time.deltaTime;
                break;
        }

        transform.rotation = Quaternion.Euler(desiredRotation);
    }
}
