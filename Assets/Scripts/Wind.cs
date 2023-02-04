using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float targetX = 0f;

    private bool windActive = false;

    private void Update()
    {
        if (!windActive) return;

        Vector3 desiredPosition = transform.position;
        desiredPosition.x = targetX;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }

    public void SetWindActive(bool active)
    {
        windActive = active;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
            playerController.WindHit();
    }
}
