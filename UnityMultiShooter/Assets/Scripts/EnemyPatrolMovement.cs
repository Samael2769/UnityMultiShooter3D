using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolMovement : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject Entity;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    private int currentWaypointIndex = 0;
    private HandleCollision handleCollision;
    public int collideValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Entity.transform.forward * speed;
        Entity = transform.GetChild(1).gameObject;
        handleCollision = Entity.GetComponent<HandleCollision>();
        Animator animator = Entity.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!handleCollision.isColliding)
        {
            Debug.Log("Not colliding");
            moveToWaypoint();
        }
        else
        {
            Debug.Log("Colliding");
            // Check if moving to the right is possible
            if (CanMoveToRight() && handleCollision.collisionCount == 1 && collideValue == 0)
            {
                Debug.Log("Can move to right");
                MoveToRight();
            }
            // If moving to the right is not possible, check if moving to the left is possible
            else if (CanMoveToLeft() && collideValue >= 0 && collideValue <= 1)
            {
                Debug.Log("Can move to left");
                collideValue = 1;
                MoveToLeft();
            }
            // If moving to the left is not possible, move backwards
            else
            {
                Debug.Log("Can't move to left or right");
                MoveBackwards();
                collideValue = 2;
            }
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    void moveToWaypoint()
    {
        if (Vector3.Distance(Entity.transform.position, waypoints[currentWaypointIndex].position) < 0.5f)
        {
            currentWaypointIndex++;
            collideValue = 0;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        Vector3 direction = waypoints[currentWaypointIndex].position - Entity.transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
    }

    bool CanMoveToRight()
    {
        Vector3 right = transform.right;
        right.Normalize();

        Vector3 raycastOrigin = Entity.transform.position + right * (Entity.transform.localScale.x * 0.5f);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, right, out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Map")
            {
                Debug.Log("Hit on the right");
                return false;
            }
        }
        return true;
    }

    bool CanMoveToLeft()
    {
        Vector3 left = -transform.right;
        left.Normalize();

        Vector3 raycastOrigin = Entity.transform.position + left * (Entity.transform.localScale.x * 0.5f);
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, left, out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Map")
            {
                Debug.Log("Hit on the left");
                return false;
            }
        }
        return true;
    }

    void MoveToRight()
    {
        Vector3 right = transform.right;
        right.Normalize();
        rb.velocity = right * speed;
    }

    void MoveToLeft()
    {
        Vector3 left = -transform.right;
        left.Normalize();
        rb.velocity = left * speed;
    }

    void MoveBackwards()
    {
        Vector3 backwards = -transform.forward;
        backwards.Normalize();
        rb.velocity = backwards * speed;
    }
}
