using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        rb.MovePosition(transform.position + move * speed * Time.deltaTime);
        anim.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 1.8f;
            anim.SetBool("IsSprinting", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8f;
            anim.SetBool("IsSprinting", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            anim.SetBool("IsJumping", false);
            canJump = true;
        }
    }
}
