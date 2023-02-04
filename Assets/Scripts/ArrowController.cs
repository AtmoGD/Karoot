using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform player;

    private void Start()
    {
        SetArrowActive(false);
    }

    private void Update()
    {
        Vector3 newPos = arrow.position;
        newPos.x = player.position.x;
        arrow.position = newPos;
    }

    public void SetArrowActive(bool active)
    {
        arrow.gameObject.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
            SetArrowActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
            SetArrowActive(false);
    }
}
