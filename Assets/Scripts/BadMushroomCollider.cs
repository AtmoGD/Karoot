using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadMushroomCollider : MonoBehaviour
{
    [SerializeField] private float damageTimeout = 1f;
    private float lastDamageTime;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null && Time.time - lastDamageTime > damageTimeout)
        {
            lastDamageTime = Time.time;
            player.TakeDamage();
        }
    }
}
