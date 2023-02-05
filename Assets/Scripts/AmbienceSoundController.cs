using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSoundController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float changeAtHeight = 40f;

    private bool changed = false;

    private void Update()
    {
        if (changed) return;

        if (player.transform.position.y > changeAtHeight)
        {
            changed = true;
            AudioManagement.AudioManager.Instance.Play("AmbienceHigh");
            AudioManagement.AudioManager.Instance.Pause("AmbienceGround");
        }
    }
}
