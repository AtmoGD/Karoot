using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    [SerializeField] private Wind wind;

    private bool windActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (windActive) return;


        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            windActive = true;
            wind.SetWindActive(true);
        }
    }
}
