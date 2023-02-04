using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private List<Vector3> waypoints = new List<Vector3>();
    [SerializeField] private float waitTime = 1f;

    private int currentWaypoint = 0;
    private float waitTimer = 0f;

    private void Start()
    {
        transform.position = waypoints[currentWaypoint];
    }

    private void Update()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        Vector3 target = waypoints[currentWaypoint];
        Vector3 direction = target - transform.position;
        direction.y = 0f;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
                currentWaypoint = 0;

            waitTimer = waitTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Count; i++)
        {
            Gizmos.DrawSphere(waypoints[i], 0.1f);
            if (i < waypoints.Count - 1)
                Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            else
                Gizmos.DrawLine(waypoints[i], waypoints[0]);
        }
    }
}
