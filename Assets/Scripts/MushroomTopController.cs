using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomTopController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
            player.JumpOnMushroom();
    }
}
