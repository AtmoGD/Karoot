using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private AnimationCurve moveFasterMultiplier = AnimationCurve.Linear(0f, 1f, 1f, 2f);

    public float Multiplier { get; set; } = 1f;

    private float timeInGame = 0f;

    private void Update()
    {
        timeInGame += Time.deltaTime;

        Vector3 desiredPosition = transform.position + Vector3.up;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveFasterMultiplier.Evaluate(timeInGame) * Multiplier * Time.deltaTime);
    }
}
