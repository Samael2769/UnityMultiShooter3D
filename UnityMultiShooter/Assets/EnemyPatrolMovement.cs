using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolMovement : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject Entity;
    [SerializeField] private Rigidbody rb;
    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Entity.transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Entity.transform.position, waypoints[currentWaypointIndex].position) < 0.5f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        Vector3 direction = waypoints[currentWaypointIndex].position - Entity.transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
    }
}
