using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float targetX = 0f;
    [SerializeField] private float delay = 0.5f;

    private bool windActive = false;
    private float delayTimer = 0f;

    private void Update()
    {
        if (!windActive) return;

        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        Vector3 desiredPosition = transform.position;
        desiredPosition.x = targetX;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }

    public void SetWindActive()
    {
        windActive = true;
        delayTimer = delay;
        AudioManagement.AudioManager.Instance.Play("Wind");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.WindHit();
            AudioManagement.AudioManager.Instance.Play("GetHitWind");
        }
    }
}
