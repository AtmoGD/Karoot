using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Joystick joystick;
    [SerializeField] private bool IsMobile { get; set; } = false;

    private void Start()
    {
        IsMobile = Application.isMobilePlatform;

        if (!IsMobile)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        player.OnMove(new Vector2(joystick.Horizontal, joystick.Vertical));
    }
}
