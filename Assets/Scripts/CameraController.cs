using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float toleranceY = 2f;
    [SerializeField] private float upSpeed = 0.5f;

    private float minY;

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        float smoothedPositionY = Mathf.Lerp(transform.position.y, desiredPosition.y, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, smoothedPositionY, transform.position.z);

        if (transform.position.y < minY - toleranceY)
            transform.position = new Vector3(transform.position.x, minY - toleranceY, transform.position.z);

        if (transform.position.y > minY)
            minY = transform.position.y;

        minY += upSpeed * Time.deltaTime;
    }
}
